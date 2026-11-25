using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PCResourceManager.Models;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public class ProcessForm : Form
    {
        private readonly ProcessManager _pm;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 3000 };
        private readonly ListView _list = new() { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, MultiSelect = false };
        private readonly Button _killBtn = new() { Text = "Завершить процесс", Width = 150, Height = 35, BackColor = Color.LightCoral };
        private readonly Button _refreshBtn = new() { Text = "Обновить", Width = 100, Height = 35 };
        public ProcessForm(ProcessManager pm)
        {
            _pm = pm;
            Text = "Управление процессами";
            Width = 800; Height = 600;
            StartPosition = FormStartPosition.CenterParent;
            _list.Columns.Add("Имя процесса", 200);
            _list.Columns.Add("PID", 80);
            _list.Columns.Add("Память (МБ)", 120);
            _list.Columns.Add("Тип", 100);
            var bottomPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };
            bottomPanel.Controls.AddRange(new Control[] { _killBtn, _refreshBtn });
            Controls.Add(_list);
            Controls.Add(bottomPanel);
            _refreshBtn.Click += (_, _) => RefreshList();
            _killBtn.Click += (_, _) => KillSelected();
            Load += (_, _) => { RefreshList(); _timer.Start(); };
            FormClosed += (_, _) => _timer.Stop();
            _timer.Tick += (_, _) => RefreshList();
        }
        private void RefreshList()
        {
            int? selectedPid = (_list.SelectedItems.Count > 0 && _list.SelectedItems[0].Tag is ProcessInfoModel m)
                ? m.Pid
                : null;
            int topItemIndex = 0;
            if (_list.Items.Count > 0 && _list.TopItem != null)
            {
                topItemIndex = _list.TopItem.Index;
            }
            _list.BeginUpdate();
            try
            {
                _list.Items.Clear();
                var processes = _pm.GetProcesses();
                foreach (var p in processes)
                {
                    var item = new ListViewItem(p.Name) { Tag = p };
                    item.SubItems.Add(p.Pid.ToString());
                    item.SubItems.Add((p.WorkingSetBytes / 1024.0 / 1024.0).ToString("0.0"));
                    item.SubItems.Add(p.IsSystemProcess ? "Системный" : "Пользовательский");
                    if (p.IsSystemProcess) item.ForeColor = Color.Gray;
                    _list.Items.Add(item);
                    if (p.Pid == selectedPid) item.Selected = true;
                }
                if (_list.Items.Count > 0 && topItemIndex > 0)
                {
                    int safeIndex = Math.Min(topItemIndex, _list.Items.Count - 1);
                    _list.Items[safeIndex].EnsureVisible();
                    _list.TopItem = _list.Items[safeIndex];
                }
            }
            finally
            {
                _list.EndUpdate();
            }
        }
        private void KillSelected()
        {
            if (_list.SelectedItems.Count == 0) return;
            var selectedProcess = (ProcessInfoModel)_list.SelectedItems[0].Tag;
            if (selectedProcess.IsSystemProcess)
            {
                MessageBox.Show("Этот процесс является системным. Его завершение заблокировано во избежание сбоя Windows.",
                    "Запрещено", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            var confirm = MessageBox.Show(
                $"Вы уверены, что хотите завершить процесс «{selectedProcess.Name}» (PID: {selectedProcess.Pid})?\n\nНесохраненные данные в этой программе будут потеряны.",
                "Подтверждение завершения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                if (_pm.KillProcess(selectedProcess.Pid))
                {
                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Не удалось завершить процесс. Возможно, он уже закрыт или у вас нет прав.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}