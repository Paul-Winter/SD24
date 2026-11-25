using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCResourceManager.Models;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public partial class DiskForm : Form
    {
        private readonly IDiskScanner _scanner;
        private readonly ProtectionService _protection;
        private ScanResult? _lastResult;
        private CancellationTokenSource? _cts;
        private readonly ICleanupService _cleanupService;
        private readonly TextBox _pathBox = new() { Width = 350, Text = @"C:\Users" };
        private readonly Button _scanBtn = new() { Text = "Сканировать", Width = 100 };
        private readonly Button _cancelBtn = new() { Text = "Отмена", Width = 80, Enabled = false };
        private readonly Button _cleanupBtn = new() { Text = "Перейти к очистке", Width = 150, Enabled = false };
        private readonly TextBox _searchBox = new() { Width = 200, PlaceholderText = "Поиск..." };
        private readonly ListView _list = new() { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, MultiSelect = true };
        private readonly ProgressBar _progress = new() { Dock = DockStyle.Bottom, Height = 20, Style = ProgressBarStyle.Blocks };
        private readonly Label _summary = new() { Dock = DockStyle.Bottom, Height = 25, TextAlign = ContentAlignment.MiddleLeft };
        private readonly ContextMenuStrip _contextMenu = new();
        public DiskForm(IDiskScanner scanner, ProtectionService protection, ICleanupService cleanupService)
        {
            _scanner = scanner;
            _protection = protection;
            _cleanupService = cleanupService;
            Text = "Анализ дискового пространства";
            Width = 1000; Height = 600;
            StartPosition = FormStartPosition.CenterParent;
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(5) };
            top.Controls.Add(new Label { Text = "Путь:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            top.Controls.Add(_pathBox);
            top.Controls.Add(_scanBtn);
            top.Controls.Add(_cancelBtn);
            top.Controls.Add(new Label { Text = " | ", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            top.Controls.Add(_searchBox);
            top.Controls.Add(_cleanupBtn);
            _list.Columns.Add("Название", 250);
            _list.Columns.Add("Размер", 100);
            _list.Columns.Add("Расположение", 400);
            _list.Columns.Add("Статус", 120);
            _list.ContextMenuStrip = _contextMenu;
            Controls.Add(_list);
            Controls.Add(_summary);
            Controls.Add(_progress);
            Controls.Add(top);
            _scanBtn.Click += async (_, _) => await RunScan();
            _cancelBtn.Click += (_, _) => _cts?.Cancel();
            _searchBox.TextChanged += (_, _) => Render(_lastResult, _searchBox.Text);
            _list.MouseClick += List_MouseClick;
            _list.ColumnClick += List_ColumnClick;
            _cleanupBtn.Click += (_, _) => OpenCleanup();
            BuildContextMenu();
        }
        private void BuildContextMenu()
        {
            var deleteItem = new ToolStripMenuItem("Можно удалить");
            deleteItem.Click += (_, _) => ChangeCategory(FileCategory.CanDelete);
            var doNotDeleteItem = new ToolStripMenuItem("Не удалять");
            doNotDeleteItem.Click += (_, _) => ChangeCategory(FileCategory.DoNotDelete);
            var reviewItem = new ToolStripMenuItem("На проверку");
            reviewItem.Click += (_, _) => ChangeCategory(FileCategory.NeedsReview);
            var protectItem = new ToolStripMenuItem("Защитить от удаления");
            protectItem.Click += (_, _) => ProtectSelected();
            var unprotectItem = new ToolStripMenuItem("Снять защиту");
            unprotectItem.Click += (_, _) => UnprotectSelected();
            _contextMenu.Items.AddRange(new ToolStripItem[] {
                deleteItem, doNotDeleteItem, reviewItem,
                new ToolStripSeparator(),
                protectItem,
                unprotectItem
            });
        }
        private async Task RunScan()
        {
            if (string.IsNullOrWhiteSpace(_pathBox.Text) || !Directory.Exists(_pathBox.Text))
            {
                MessageBox.Show("Указанная папка не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _cts = new CancellationTokenSource();
            _scanBtn.Enabled = false;
            _cancelBtn.Enabled = true;
            _cleanupBtn.Enabled = false;
            _progress.Style = ProgressBarStyle.Marquee;
            var progress = new Progress<int>(n => _summary.Text = $"Сканирование... Найдено: {n}");
            try
            {
                _lastResult = await _scanner.ScanAsync(_pathBox.Text, progress, _cts.Token);
                Render(_lastResult, _searchBox.Text);
                _cleanupBtn.Enabled = true;
            }
            catch (OperationCanceledException)
            {
                _summary.Text = "Сканирование отменено.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _scanBtn.Enabled = true;
                _cancelBtn.Enabled = false;
                _progress.Style = ProgressBarStyle.Blocks;
                _cts.Dispose();
                _cts = null;
            }
        }
        private void Render(ScanResult? result, string? query)
        {
            if (result == null) return;
            var items = string.IsNullOrWhiteSpace(query) ? result.Items : _scanner.Search(result, query);
            _list.BeginUpdate();
            try
            {
                _list.Items.Clear();
                foreach (var f in items.OrderByDescending(f => f.SizeBytes).Take(2000))
                {
                    var it = new ListViewItem(f.Name) { Tag = f };
                    it.SubItems.Add(f.SizeDisplay());
                    it.SubItems.Add(Path.GetDirectoryName(f.FullPath) ?? "");
                    it.SubItems.Add(CategoryRu(f.Category));
                    if (f.IsProtected || f.Category == FileCategory.System)
                        it.ForeColor = Color.Gray;
                    _list.Items.Add(it);
                }
            }
            finally { _list.EndUpdate(); }
            _summary.Text = $"Найдено: {items.Count}, общий объём: {result.TotalSizeBytes / 1024 / 1024} МБ";
        }
        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _list.SelectedItems.Count > 0)
            {
                _contextMenu.Show(_list, e.Location);
            }
        }
        private void ChangeCategory(FileCategory newCategory)
        {
            if (_lastResult == null) return;
            foreach (ListViewItem item in _list.SelectedItems)
            {
                if (item.Tag is FileInfoModel file)
                {
                    if (file.IsSystem || file.IsProtected) continue;
                    file.Category = newCategory;
                    item.SubItems[3].Text = CategoryRu(newCategory);
                }
            }
        }
        private void ProtectSelected()
        {
            if (_lastResult == null) return;
            foreach (ListViewItem item in _list.SelectedItems)
            {
                if (item.Tag is FileInfoModel file)
                {
                    file.IsProtected = true;
                    file.Category = FileCategory.DoNotDelete;
                    item.SubItems[3].Text = "Не удалять";
                    item.ForeColor = Color.Gray;
                    _protection.Protect(file.FullPath, isFolder: false);
                }
            }
            MessageBox.Show("Выбранные файлы защищены и внесены в белый список.", "Защита");
        }
        private void UnprotectSelected()
        {
            if (_lastResult == null) return;
            bool anyChanged = false;
            foreach (ListViewItem item in _list.SelectedItems)
            {
                if (item.Tag is FileInfoModel file)
                {
                    if (!file.IsProtected) continue;
                    file.IsProtected = false;
                    file.Category = FileCategory.NeedsReview;
                    item.SubItems[3].Text = CategoryRu(file.Category);
                    item.ForeColor = Color.Black;
                    _protection.Unprotect(file.FullPath);
                    anyChanged = true;
                }
            }
            if (anyChanged)
                MessageBox.Show("Защита снята. Файлы снова можно удалять.", "Защита");
        }
        private void OpenCleanup()
        {
            if (_lastResult == null) return;
            var toDelete = _lastResult.Items
                .Where(f => f.Category == FileCategory.CanDelete && !f.IsProtected)
                .ToList();
            if (toDelete.Count == 0)
            {
                MessageBox.Show("Нет файлов, помеченных как 'Можно удалить'.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var cleanupForm = new CleanupForm(_cleanupService, toDelete);
            if (cleanupForm.ShowDialog(this) == DialogResult.OK)
            {

            }
        }
        private void List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _list.ListViewItemSorter = new ListViewItemComparer(e.Column);
            _list.Sort();
        }
        private static string CategoryRu(FileCategory c) => c switch
        {
            FileCategory.CanDelete => "Можно удалить",
            FileCategory.DoNotDelete => "Не удалять",
            FileCategory.System => "Системные",
            _ => "На проверку"
        };
    }
    public class ListViewItemComparer : System.Collections.IComparer
    {
        private readonly int _col;
        private readonly SortOrder _order;
        public ListViewItemComparer(int column)
        {
            _col = column;
            _order = SortOrder.Ascending;
        }
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null) return 0;
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            if (_col == 1 && itemX.Tag is FileInfoModel fX && itemY.Tag is FileInfoModel fY)
            {
                return fX.SizeBytes.CompareTo(fY.SizeBytes) * (_order == SortOrder.Ascending ? 1 : -1);
            }
            return string.Compare(itemX.SubItems[_col].Text, itemY.SubItems[_col].Text, StringComparison.Ordinal) * (_order == SortOrder.Ascending ? 1 : -1);
        }
    }
}
