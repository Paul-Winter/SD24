using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using InventorySystem.ViewModels;

namespace InventorySystem.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var deleteBtn = this.FindControl<Button>("DeleteRoomButton");
            if (deleteBtn != null) deleteBtn.Click += OnDeleteRoomClick;
        }

        private void OnRoomDoubleTapped(object? sender, TappedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm && vm.SelectedRoom != null)
            {
                vm.OpenRoomEquipmentCommand.Execute(null);
                if (this.FindControl<TabControl>("MainTabControl") is TabControl tabControl)
                    tabControl.SelectedIndex = 1;
            }
        }

        private async void OnDeleteRoomClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm && vm.SelectedRoom != null)
            {
                var confirmWindow = new ConfirmWindow($"Удалить аудиторию {vm.SelectedRoom.Number}?");
                var result = await confirmWindow.ShowDialog<bool>(this);

                if (result)
                {
                    vm.DeleteRoomCommand.Execute(null);
                }
            }
        }
    }
}