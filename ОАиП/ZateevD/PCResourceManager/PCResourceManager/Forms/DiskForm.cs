using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
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
        private readonly TextBox _pathBox = new() { Width = 400, Text = @"C:\Users" };
        private readonly Button _scanBtn = new() { Text = "Сканировать" };
        private readonly Button _cancelBtn = new() { Text = "Отмена", Enabled = false };
        private readonly TextBox _searchBox = new() { Width = 200, PlaceholderText = "Поиск..." };
        private readonly ListView _list = new() { Dock = DockStyle.Fill, View = View.Details };
        private readonly ProgressBar _progress = new() { Dock = DockStyle.Bottom, Height = 20 };
        private readonly Label _summary = new() { Dock = DockStyle.Bottom, Height = 20 };
        public DiskForm(IDiskScanner scanner, ProtectionService protection)
        {
            _scanner = scanner;
            _protection = protection;
            Text = "Анализ дискового пространства";
            Width = 1100; Height = 650;
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new(5) };
            top.Controls.AddRange([_pathBox, _scanBtn, _cancelBtn, _searchBox]);
            _list.Columns.Add("Название", 220);
            _list.Columns.Add("Размер", 100);
            _list.Columns.Add("Расположение", 350);
            _list.Columns.Add("Статус", 120);
            Controls.Add(_list);
            Controls.Add(_summary);
            Controls.Add(_progress);
            Controls.Add(top);
            _scanBtn.Click += async (_, _) => await RunScan();
            _cancelBtn.Click += (_, _) => _cts?.Cancel();
            _searchBox.TextChanged += (_, _) => Render(_lastResult, _searchBox.Text);
        }
        private async Task RunScan()
        {
            _cts = new CancellationTokenSource();
            _scanBtn.Enabled = false;
            _cancelBtn.Enabled = true;
            _progress.Style = ProgressBarStyle.Marquee;
            var progress = new Progress<int>(n => _summary.Text = $"Найдено: {n}...");
            try
            {
                _lastResult = await _scanner.ScanAsync(_pathBox.Text, progress, _cts.Token);
                Render(_lastResult, _searchBox.Text);
            }
            catch (OperationCanceledException) { _summary.Text = "Отменено"; }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка"); }
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
                    _list.Items.Add(it);
                }
            }
            finally { _list.EndUpdate(); }
            _summary.Text = $"Найдено: {items.Count}, объём: {result.TotalSizeBytes / 1024 / 1024} МБ";
        }
        private static string CategoryRu(FileCategory c) => c switch
        {
            FileCategory.CanDelete => "Можно удалить",
            FileCategory.DoNotDelete => "Не удалять",
            FileCategory.System => "Системные",
            _ => "На проверку"
        };
    }
}
