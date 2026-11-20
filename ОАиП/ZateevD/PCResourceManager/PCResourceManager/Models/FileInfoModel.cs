using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models
{
    public class FileInfoModel
    {
        public required string FullPath { get; init; }
        public required string Name { get; init; }
        public long SizeBytes { get; set; }
        public DateTime LastModified { get; set; }
        public string FileType { get; set; } = "";
        public FileCategory Category { get; set; } = FileCategory.NeedsReview;
        public bool IsProtected { get; set; }
        public bool IsSystem { get; set; }
        public string SizeDisplay()
        {
            string[] units = ["Б", "КБ", "МБ", "ГБ", "ТБ"];
            double size = SizeBytes;
            int i = 0;
            while (size >= 1024 && i < units.Length - 1) { size /= 1024; i++; }
            return $"{size:0.##} {units[i]}";
        }
    }
}