using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models
{
    public class UserSettings
    {
        public bool AutoScanEnabled { get; set; }
        public string Language { get; set; } = "ru";
        public DateTime LastScan { get; set; }
        public List<string> Exclusions { get; set; } = [];
    }
}
