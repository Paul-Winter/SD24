using System;
using System.IO;
using System.Windows.Forms;
using PCResourceManager.Forms;
using PCResourceManager.Services;
namespace PCResourceManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string dataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            var repo = new SettingsRepository(dataFolder);
            var protection = new ProtectionService(repo);
            var monitor = new ResourceMonitor();
            var scanner = new DiskScanner(protection);
            //var cleanup = new CleanupService(protection);
            //var pm = new ProcessManager();
            Application.Run(new MainForm(monitor));
        }
    }
}