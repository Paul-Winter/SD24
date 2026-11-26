using System;
using System.IO;
using System.Windows.Forms;
using PCResourceManager.Forms;
using PCResourceManager.Services;
namespace PCResourceManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            string dataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            var repo = new SettingsRepository(dataFolder);
            var protection = new ProtectionService(repo);
            var scanner = new DiskScanner(protection);
            var cleanup = new CleanupService(protection);
            Application.Run(new MainForm(repo, scanner, protection, cleanup));
        }
    }
}
