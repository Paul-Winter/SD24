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
        private readonly ICleanupService _cleanupService;
        private ScanResult? _lastResult;
        private CancellationTokenSource? _cts;
        private readonly TextBox _pathBox = new() { Width = 300, Text = @"C:\Users" };
        private readonly Button _scanBtn = new() { Text = "Сканировать", Width = 100 };
        private readonly Button _cancelBtn = new() { Text = "Отмена", Width = 80, Enabled = false };
        private readonly Button _cleanupBtn = new() { Text = "Перейти к очистке", Width = 150, Enabled = false };
        private readonly Button _topFoldersBtn = new() { Text = "Топ папок", Width = 100, Enabled = false };
        private readonly ComboBox _filterBox = new() { Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly TextBox _searchBox = new() { Width = 150, PlaceholderText = "Поиск..." };
        private readonly SplitContainer _splitContainer = new() { Dock = DockStyle.Fill };
        private readonly TreeView _treeView = new() { Dock = DockStyle.Fill };
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
            Width = 1100; Height = 700;
            StartPosition = FormStartPosition.CenterParent;
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(5) };
            top.Controls.Add(new Label { Text = "Путь:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            top.Controls.Add(_pathBox);
            top.Controls.Add(_scanBtn);
            top.Controls.Add(_cancelBtn);
            _filterBox.Items.AddRange(new object[] {
                "Все файлы",
                "Старые (не менялись > 1 года)",
                "Свежие (за последний месяц)"
            });
            _filterBox.SelectedIndex = 0;
            top.Controls.Add(new Label { Text = "Фильтр:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            top.Controls.Add(_filterBox);
            top.Controls.Add(_searchBox);
            top.Controls.Add(_cleanupBtn);
            top.Controls.Add(_topFoldersBtn);
            _splitContainer.SplitterDistance = 350;
            _splitContainer.Panel1.Controls.Add(_treeView);
            _splitContainer.Panel2.Controls.Add(_list);
            _treeView.HideSelection = false;
            _list.Columns.Add("Название", 220);
            _list.Columns.Add("Размер", 90);
            _list.Columns.Add("Дата изменения", 100);
            _list.Columns.Add("Статус", 110);
            _list.ContextMenuStrip = _contextMenu;
            Controls.Add(_splitContainer);
            Controls.Add(_summary);
            Controls.Add(_progress);
            Controls.Add(top);
            _scanBtn.Click += async (_, _) => await RunScan();
            _cancelBtn.Click += (_, _) => _cts?.Cancel();
            _searchBox.TextChanged += (_, _) => RenderFiles();
            _filterBox.SelectedIndexChanged += (_, _) => RenderFiles();
            _list.MouseClick += List_MouseClick;
            _list.ColumnClick += List_ColumnClick;
            _cleanupBtn.Click += (_, _) => OpenCleanup();
            _topFoldersBtn.Click += (_, _) => ShowTopFolders();
            _treeView.AfterSelect += (_, _) => RenderFiles();
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
                new ToolStripSeparator(), protectItem, unprotectItem
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
            _topFoldersBtn.Enabled = false;
            _progress.Style = ProgressBarStyle.Marquee;
            var progress = new Progress<int>(n => _summary.Text = $"Сканирование... Найдено: {n}");
            try
            {
                _lastResult = await _scanner.ScanAsync(_pathBox.Text, progress, _cts.Token);
                BuildTree(_lastResult);
                _cleanupBtn.Enabled = true;
                _topFoldersBtn.Enabled = true;
            }
            catch (OperationCanceledException) { _summary.Text = "Отменено."; }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
            finally
            {
                _scanBtn.Enabled = true;
                _cancelBtn.Enabled = false;
                _progress.Style = ProgressBarStyle.Blocks;
                _cts.Dispose();
                _cts = null;
            }
        }
        private void BuildTree(ScanResult result)
        {
            _treeView.BeginUpdate();
            _treeView.Nodes.Clear();
            var rootNode = new TreeNode(result.RootPath) { Tag = result.RootPath };
            _treeView.Nodes.Add(rootNode);
            var folders = result.Items
                .Select(f => Path.GetDirectoryName(f.FullPath))
                .Where(f => f != null)
                .Distinct()
                .OrderBy(f => f)
                .ToList();
            var nodeDict = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase)
            {
                { result.RootPath, rootNode }
            };
            foreach (var folder in folders)
            {
                if (folder == null) continue;
                if (nodeDict.ContainsKey(folder)) continue;
                var parts = folder.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
                string currentPath = "";
                TreeNode? parentNode = null;
                for (int i = 0; i < parts.Length; i++)
                {
                    currentPath = i == 0 ? parts[0] + Path.DirectorySeparatorChar : Path.Combine(currentPath, parts[i]);
                    if (!nodeDict.TryGetValue(currentPath, out var node))
                    {
                        node = new TreeNode(parts[i]) { Tag = currentPath };
                        if (parentNode != null)
                            parentNode.Nodes.Add(node);
                        else if (currentPath.StartsWith(result.RootPath, StringComparison.OrdinalIgnoreCase))
                            rootNode.Nodes.Add(node);
                        nodeDict[currentPath] = node;
                    }
                    parentNode = node;
                }
            }
            rootNode.Expand();
            _treeView.EndUpdate();
            _treeView.SelectedNode = rootNode;
        }
        private void RenderFiles()
        {
            if (_lastResult == null) return;
            string selectedFolder = _treeView.SelectedNode?.Tag as string ?? _lastResult.RootPath;
            var items = _lastResult.Items.Where(f =>
                Path.GetDirectoryName(f.FullPath)?.StartsWith(selectedFolder, StringComparison.OrdinalIgnoreCase) == true);
            if (_filterBox.SelectedIndex == 1)
                items = items.Where(f => f.LastModified < DateTime.Now.AddYears(-1));
            else if (_filterBox.SelectedIndex == 2)
                items = items.Where(f => f.LastModified > DateTime.Now.AddMonths(-1));
            var query = _searchBox.Text;
            if (!string.IsNullOrWhiteSpace(query))
                items = items.Where(i => i.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                                      || i.FullPath.Contains(query, StringComparison.OrdinalIgnoreCase));
            _list.BeginUpdate();
            try
            {
                _list.Items.Clear();
                foreach (var f in items.OrderByDescending(f => f.SizeBytes).Take(3000))
                {
                    var it = new ListViewItem(f.Name) { Tag = f };
                    it.SubItems.Add(f.SizeDisplay());
                    it.SubItems.Add(f.LastModified.ToShortDateString());
                    it.SubItems.Add(CategoryRu(f.Category));
                    if (f.IsProtected || f.Category == FileCategory.System)
                        it.ForeColor = Color.Gray;
                    _list.Items.Add(it);
                }
            }
            finally { _list.EndUpdate(); }
            _summary.Text = $"Выбрана папка: {selectedFolder}. Файлов: {_list.Items.Count}. Объём: {items.Sum(f => f.SizeBytes) / 1024 / 1024} МБ";
        }
        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _list.SelectedItems.Count > 0)
                _contextMenu.Show(_list, e.Location);
        }
        private void ChangeCategory(FileCategory newCategory)
        {
            if (_lastResult == null) return;
            foreach (ListViewItem item in _list.SelectedItems)
            {
                if (item.Tag is FileInfoModel file && !file.IsSystem && !file.IsProtected)
                {
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
        }
        private void UnprotectSelected()
        {
            if (_lastResult == null) return;
            foreach (ListViewItem item in _list.SelectedItems)
            {
                if (item.Tag is FileInfoModel file && file.IsProtected)
                {
                    file.IsProtected = false;
                    file.Category = FileCategory.NeedsReview;
                    item.SubItems[3].Text = "На проверку";
                    item.ForeColor = Color.Black;
                    _protection.Unprotect(file.FullPath);
                }
            }
        }
        private void OpenCleanup()
        {
            if (_lastResult == null) return;
            var toDelete = _lastResult.Items.Where(f => f.Category == FileCategory.CanDelete && !f.IsProtected).ToList();
            if (toDelete.Count == 0) { MessageBox.Show("Нет файлов, помеченных как 'Можно удалить'.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            var cleanupForm = new CleanupForm(_cleanupService, toDelete);
            cleanupForm.ShowDialog(this);
        }
        private void ShowTopFolders()
        {
            if (_lastResult == null) return;
            var topFolders = _lastResult.Items.GroupBy(f => Path.GetDirectoryName(f.FullPath)).Where(g => g.Key != null)
                .Select(g => new { Path = g.Key!, TotalSize = g.Sum(f => f.SizeBytes), FileCount = g.Count() })
                .OrderByDescending(x => x.TotalSize).Take(20).ToList();
            if (topFolders.Count == 0) { MessageBox.Show("Нет данных.", "Топ папок", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Самые тяжелые папки в отсканированной директории:\n");
            foreach (var folder in topFolders)
            {
                double sizeMb = folder.TotalSize / 1024.0 / 1024.0;
                string sizeStr = sizeMb > 1024 ? $"{sizeMb / 1024.0:0.00} ГБ" : $"{sizeMb:0.00} МБ";
                sb.AppendLine($"{sizeStr,-10} | Файлов: {folder.FileCount,-5} | {folder.Path}");
            }
            MessageBox.Show(sb.ToString(), "Топ-20 папок", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public ListViewItemComparer(int column) => _col = column;
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null) return 0;
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            if (_col == 1 && itemX.Tag is FileInfoModel fX && itemY.Tag is FileInfoModel fY)
                return fX.SizeBytes.CompareTo(fY.SizeBytes);
            if (_col == 2 && itemX.Tag is FileInfoModel dX && itemY.Tag is FileInfoModel dY)
                return dX.LastModified.CompareTo(dY.LastModified);
            return string.Compare(itemX.SubItems[_col].Text, itemY.SubItems[_col].Text, StringComparison.Ordinal);
        }
    }
}
