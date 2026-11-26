using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public partial class MainForm : Form
    {
        private readonly SettingsRepository _repo;
        private readonly IDiskScanner _scanner;
        private readonly ProtectionService _protection;
        private readonly ICleanupService _cleanup;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 5000 };
        private readonly ListView _diskList = new() { Dock = DockStyle.Fill, View = View.Details };
        private readonly Button _openDiskBtn = new() { Text = "Анализ дисков", Width = 150, Height = 40 };
        private readonly Button _openSettingsBtn = new() { Text = "Белый список", Width = 150, Height = 40 };
        public MainForm(SettingsRepository repo, IDiskScanner scanner, ProtectionService protection, ICleanupService cleanup)
        {
            _repo = repo;
            _scanner = scanner;
            _protection = protection;
            _cleanup = cleanup;
            Text = "PC Resource Manager";
            Width = 900; Height = 500;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            _timer.Tick += (_, _) => RefreshDisks();
            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(10) };
            _openDiskBtn.BackColor = Color.LightSteelBlue;
            _openSettingsBtn.BackColor = Color.LightSteelBlue;
            buttonPanel.Controls.Add(_openDiskBtn);
            buttonPanel.Controls.Add(_openSettingsBtn);
            var listContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            _diskList.Columns.Add("Диск", 100);
            _diskList.Columns.Add("Всего, ГБ", 120);
            _diskList.Columns.Add("Свободно, ГБ", 120);
            _diskList.Columns.Add("Занято, %", 100);
            listContainer.Controls.Add(_diskList);
            Controls.Add(listContainer);
            Controls.Add(buttonPanel);
            Load += (_, _) => { RefreshDisks(); _timer.Start(); };
            FormClosed += (_, _) => _timer.Stop();
            _openDiskBtn.Click += (_, _) => OpenDiskForm();
            _openSettingsBtn.Click += (_, _) => OpenSettingsForm();
        }
        private void RefreshDisks()
        {
            try
            {
                _diskList.Items.Clear();
                foreach (var d in DriveInfo.GetDrives().Where(d => d.IsReady))
                {
                    double totalGb = d.TotalSize / 1024.0 / 1024.0 / 1024.0;
                    double freeGb = d.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
                    double usedPct = totalGb > 0 ? (1 - freeGb / totalGb) * 100 : 0;
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add(totalGb.ToString("0.0"));
                    item.SubItems.Add(freeGb.ToString("0.0"));
                    item.SubItems.Add(usedPct.ToString("0.0") + " %");
                    if (usedPct > 90) item.ForeColor = Color.Red;
                    _diskList.Items.Add(item);
                }
            }
            catch { }
        }
        private void OpenDiskForm()
        {
            var diskForm = new DiskForm(_scanner, _protection, _cleanup);
            diskForm.ShowDialog(this);
        }
        private void OpenSettingsForm()
        {
            var settingsForm = new SettingsForm(_repo, _protection);
            settingsForm.ShowDialog(this);
        }
    }
}
