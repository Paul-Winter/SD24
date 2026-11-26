using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PCResourceManager.Models;
using PCResourceManager.Services;
namespace PCResourceManager.Forms
{
    public class SettingsForm : Form
    {
        private readonly SettingsRepository _repo;
        private readonly ProtectionService _protection;
        private UserSettings _settings;
        private readonly ListBox _protectedList = new() { Dock = DockStyle.Fill };
        private readonly Button _addFolderBtn = new() { Text = "Добавить папку", Height = 30 };
        private readonly Button _removeFolderBtn = new() { Text = "Удалить из списка", Height = 30 };
        public SettingsForm(SettingsRepository repo, ProtectionService protection)
        {
            _repo = repo;
            _protection = protection;
            Text = "Белый список";
            Width = 500; Height = 450;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            _settings = _repo.Load();
            var topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 100, Padding = new Padding(15), FlowDirection = FlowDirection.TopDown };
            var middlePanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            var titleLabel = new Label { Text = "Защищенные объекты (белый список):", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            var listContainer = new Panel { Dock = DockStyle.Fill };
            listContainer.Controls.Add(_protectedList);
            var rightButtons = new FlowLayoutPanel { Dock = DockStyle.Right, Width = 130, FlowDirection = FlowDirection.TopDown, Padding = new Padding(5, 0, 0, 0) };
            rightButtons.Controls.Add(_addFolderBtn);
            rightButtons.Controls.Add(_removeFolderBtn);
            middlePanel.Controls.Add(listContainer);
            middlePanel.Controls.Add(rightButtons);
            middlePanel.Controls.Add(titleLabel);
            var bottomPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 60, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(10) };
            var saveBtn = new Button { Text = "Сохранить", Width = 120, Height = 35, BackColor = Color.LightGreen, DialogResult = DialogResult.None };
            var cancelBtn = new Button { Text = "Отмена", Width = 100, Height = 35, DialogResult = DialogResult.Cancel };
            bottomPanel.Controls.Add(saveBtn);
            bottomPanel.Controls.Add(cancelBtn);
            Controls.Add(middlePanel);
            Controls.Add(topPanel);
            Controls.Add(bottomPanel);
            CancelButton = cancelBtn;
            _addFolderBtn.Click += (_, _) => AddProtectedFolder();
            _removeFolderBtn.Click += (_, _) => RemoveProtectedFolder();
            saveBtn.Click += (_, _) => SaveSettings();
            RefreshProtectedList();
        }
        private void RefreshProtectedList()
        {
            _protectedList.Items.Clear();
            foreach (var item in _protection.All)
            {
                string prefix = item.IsFolder ? "[Папка] " : "[Файл]  ";
                _protectedList.Items.Add(prefix + item.Path);
            }
        }
        private void AddProtectedFolder()
        {
            using var dialog = new FolderBrowserDialog();
            dialog.Description = "Выберите папку для защиты от удаления";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    _protection.Protect(dialog.SelectedPath, isFolder: true);
                    RefreshProtectedList();
                }
            }
        }
        private void RemoveProtectedFolder()
        {
            if (_protectedList.SelectedIndex == -1) return;
            string selectedText = _protectedList.SelectedItem.ToString();
            string path = selectedText.Replace("[Папка] ", "").Replace("[Файл]  ", "");
            _protection.Unprotect(path);
            RefreshProtectedList();
        }
        private void SaveSettings()
        {
            try
            {
                _repo.Save(_settings);
                MessageBox.Show("Настройки успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}