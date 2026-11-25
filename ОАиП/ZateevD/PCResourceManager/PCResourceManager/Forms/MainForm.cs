using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public class MainForm : Form
    {
        private readonly ProcessManager _processManager;
        private readonly IResourceMonitor _monitor;
        private readonly IDiskScanner _scanner;
        private readonly ProtectionService _protection;
        private readonly ICleanupService _cleanup;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 2000 };
        private readonly Button _openProcessBtn = new() { Text = "Процессы", Width = 150, Height = 40 };
        private readonly Label _cpuLabel = new() { AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
        private readonly Label _ramLabel = new() { AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
        private readonly ListView _diskList = new() { Dock = DockStyle.Fill, View = View.Details };
        private readonly Button _openDiskBtn = new() { Text = "Анализ дисков", Width = 150, Height = 40 };
        public MainForm(IResourceMonitor monitor, IDiskScanner scanner, ProtectionService protection, ICleanupService cleanup, ProcessManager processManager)
        {
            _monitor = monitor;
            _scanner = scanner;
            _protection = protection;
            _cleanup = cleanup;
            _processManager = processManager;
            Text = "PC Resource Manager";
            Width = 900; Height = 600;
            StartPosition = FormStartPosition.CenterScreen;
            var topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(10) };
            topPanel.Controls.Add(_cpuLabel);
            topPanel.Controls.Add(new Label { Width = 30 });
            topPanel.Controls.Add(_ramLabel);
            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10) };
            buttonPanel.Controls.Add(_openDiskBtn);
            buttonPanel.Controls.Add(_openProcessBtn);
            _diskList.Columns.Add("Диск", 80);
            _diskList.Columns.Add("Всего, ГБ", 100);
            _diskList.Columns.Add("Свободно, ГБ", 110);
            _diskList.Columns.Add("Занято, %", 100);
            Controls.Add(_diskList);
            Controls.Add(buttonPanel);
            Controls.Add(topPanel);
            _timer.Tick += (_, _) => RefreshDashboard();
            Load += (_, _) => { RefreshDashboard(); _timer.Start(); };
            FormClosed += (_, _) => { _timer.Stop(); (_monitor as IDisposable)?.Dispose(); };
            _openDiskBtn.Click += (_, _) => OpenDiskForm();
            _openProcessBtn.Click += (_, _) => OpenProcessForm();
        }
        private void RefreshDashboard()
        {
            try
            {
                _cpuLabel.Text = $"CPU: {_monitor.GetCpuUsage():0.0}%";
                (double used, double total) = _monitor.GetRamUsage();
                _ramLabel.Text = $"RAM: {used:0.0} / {total:0.0} ГБ";
                _diskList.Items.Clear();
                foreach (var d in _monitor.GetDisks().Where(d => d.IsReady))
                {
                    double totalGb = d.TotalSize / 1024.0 / 1024.0 / 1024.0;
                    double freeGb = d.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
                    double usedPct = totalGb > 0 ? (1 - freeGb / totalGb) * 100 : 0;
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add(totalGb.ToString("0.0"));
                    item.SubItems.Add(freeGb.ToString("0.0"));
                    item.SubItems.Add(usedPct.ToString("0.0"));
                    _diskList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                _cpuLabel.Text = $"Ошибка: {ex.Message}";
            }
        }
        private void OpenDiskForm()
        {
            var diskForm = new DiskForm(_scanner, _protection, _cleanup);
            diskForm.ShowDialog(this);
        }
        private void OpenProcessForm()
        {
            var processForm = new ProcessForm(_processManager);
            processForm.ShowDialog(this);
        }
    }
}
