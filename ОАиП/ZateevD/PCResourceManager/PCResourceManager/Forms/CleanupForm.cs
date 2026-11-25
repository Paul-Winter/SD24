using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PCResourceManager.Models;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public class CleanupForm : Form
    {
        private readonly ICleanupService _cleanup;
        private readonly List<FileInfoModel> _selectedFiles;
        private readonly ListView _list = new() { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true };
        private readonly Label _infoLabel = new() { Dock = DockStyle.Top, Height = 60, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        public CleanupForm(ICleanupService cleanup, List<FileInfoModel> selectedFiles)
        {
            _cleanup = cleanup;
            _selectedFiles = selectedFiles;
            Text = "Очистка диска";
            Width = 800; Height = 500;
            StartPosition = FormStartPosition.CenterParent;
            long totalBytes = _cleanup.Preview(_selectedFiles);
            _infoLabel.Text = $"Выбрано объектов: {_selectedFiles.Count}\n" +
                              $"Потенциально освободится: {totalBytes / 1024.0 / 1024.0:0.00} МБ\n" +
                              "Внимание! Системные и защищённые файлы не будут удалены.";
            _infoLabel.Padding = new Padding(10);
            _list.Columns.Add("Путь к файлу", 500);
            _list.Columns.Add("Размер", 100);
            _list.Columns.Add("Защита", 100);
            foreach (var f in _selectedFiles)
            {
                var it = new ListViewItem(f.FullPath);
                it.SubItems.Add(f.SizeDisplay());
                it.SubItems.Add(f.IsProtected ? "Да" : "Нет");
                if (f.IsProtected) it.ForeColor = Color.Gray;
                _list.Items.Add(it);
            }
            var bottomPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };
            var deleteBtn = new Button { Text = "Удалить безвозвратно", Width = 180, Height = 35, BackColor = Color.LightCoral };
            var recycleBtn = new Button { Text = "В Корзину", Width = 120, Height = 35, BackColor = Color.LightBlue };
            var cancelBtn = new Button { Text = "Отмена", Width = 100, Height = 35 };
            bottomPanel.Controls.AddRange(new Control[] { deleteBtn, recycleBtn, cancelBtn });
            deleteBtn.Click += (_, _) => RunCleanup(false);
            recycleBtn.Click += (_, _) => RunCleanup(true);
            cancelBtn.Click += (_, _) => Close();
            Controls.Add(_list);
            Controls.Add(_infoLabel);
            Controls.Add(bottomPanel);
        }
        private void RunCleanup(bool toRecycle)
        {
            string actionName = toRecycle ? "переместить в Корзину" : "УДАЛИТЬ БЕЗВОЗВРАТНО";
            var confirm = MessageBox.Show(
                $"Вы действительно хотите {actionName} {_selectedFiles.Count} объектов?\n\nЭто действие нельзя отменить.",
                "Подтверждение очистки",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            var report = _cleanup.Delete(_selectedFiles, toRecycle);
            MessageBox.Show(
                $"Успешно обработано: {report.DeletedCount}\n" +
                $"Освобождено: {report.FreedBytes / 1024.0 / 1024.0:0.00} МБ\n" +
                $"Ошибок: {report.Errors.Count}",
                "Отчёт об очистке",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
        }
    }
}