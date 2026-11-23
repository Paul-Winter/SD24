using PCResourceManager.Services;
namespace PCResourceManager
{
    public partial class MainForm : Form
    {
        private readonly IResourceMonitor _monitor;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 2000 };
        private readonly Label _cpuLabel = new() { AutoSize = true };
        private readonly Label _ramLabel = new() { AutoSize = true };
        private readonly ListView _diskList = new() { Dock = DockStyle.Fill, View = View.Details };
        public MainForm(IResourceMonitor monitor)
        {
            _monitor = monitor;
            Text = "PC Resource Manager";
            Width = 900; Height = 600;
            var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, Padding = new(10) };
            panel.Controls.Add(_cpuLabel);
            panel.Controls.Add(_ramLabel);
            _diskList.Columns.Add("Диск", 80);
            _diskList.Columns.Add("Всего, ГБ", 100);
            _diskList.Columns.Add("Свободно, ГБ", 110);
            _diskList.Columns.Add("Занято, %", 100);
            Controls.Add(_diskList);
            Controls.Add(panel);
            _timer.Tick += (_, _) => RefreshDashboard();
            Load += (_, _) => { RefreshDashboard(); _timer.Start(); };
            FormClosed += (_, _) => { _timer.Stop(); (_monitor as IDisposable)?.Dispose(); };
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
    }
}
