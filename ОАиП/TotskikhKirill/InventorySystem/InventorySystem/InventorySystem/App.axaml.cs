using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using InventorySystem.Data;
using InventorySystem.ViewModels;
using InventorySystem.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventorySystem
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var collection = new ServiceCollection();

            collection.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=InventorySystemDB;Username=postgres;Password=0000"));

            collection.AddTransient<MainWindowViewModel>();

            var services = collection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainViewModel = services.GetRequiredService<MainWindowViewModel>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}