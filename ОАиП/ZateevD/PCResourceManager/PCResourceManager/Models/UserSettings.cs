using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models
{
    public class UserSettings
    {
        public int RefreshIntervalSec { get; set; } = 5;
        public bool AutoScanEnabled { get; set; }
        public bool NotificationsEnabled { get; set; } = true;
        public string Language { get; set; } = "ru";
        public DateTime LastScan { get; set; }
        public List<string> Exclusions { get; set; } = [];
    }
}