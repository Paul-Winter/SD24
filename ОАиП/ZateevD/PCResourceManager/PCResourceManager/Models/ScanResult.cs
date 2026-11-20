using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models
{
    public class ScanResult
    {
        public string RootPath { get; set; } = "";
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public long TotalSizeBytes { get; set; }
        public List<FileInfoModel> Items { get; set; } = new();
    }
}