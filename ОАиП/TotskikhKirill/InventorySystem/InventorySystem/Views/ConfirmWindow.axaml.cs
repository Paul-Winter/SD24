using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace InventorySystem.Views
{
    public partial class ConfirmWindow : Window
    {
        public bool IsConfirmed { get; private set; } = false;

        public ConfirmWindow()
        {
            AvaloniaXamlLoader.Load(this);

            this.FindControl<Button>("YesButton").Click += OnYesClick;
            this.FindControl<Button>("NoButton").Click += OnNoClick;
        }

        public ConfirmWindow(string message) : this()
        {
            var textBlock = this.FindControl<TextBlock>("MessageText");
            if (textBlock != null) textBlock.Text = message;
        }

        private void OnYesClick(object? sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            Close();
        }

        private void OnNoClick(object? sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            Close();
        }
    }
}