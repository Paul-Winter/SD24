using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using PCResourceManager.Models;
namespace PCResourceManager.Services
{
    public class CleanupReport
    {
        public int DeletedCount { get; set; }
        public long FreedBytes { get; set; }
        public List<string> Errors { get; set; } = new();
    }
    public interface ICleanupService
    {
        long Preview(IEnumerable<FileInfoModel> items);
        CleanupReport Delete(IEnumerable<FileInfoModel> items, bool toRecycle);
    }
    public class CleanupService : ICleanupService
    {
        private readonly ProtectionService _protection;
        public CleanupService(ProtectionService protection)
        {
            _protection = protection;
        }
        public long Preview(IEnumerable<FileInfoModel> items)
        {
            return items.Where(_protection.CanDelete).Sum(i => i.SizeBytes);
        }
        public CleanupReport Delete(IEnumerable<FileInfoModel> items, bool toRecycle)
        {
            var report = new CleanupReport();
            foreach (var item in items)
            {
                if (!_protection.CanDelete(item))
                {
                    report.Errors.Add($"Пропущен (защищён/системный): {item.FullPath}");
                    continue;
                }
                try
                {
                    if (toRecycle)
                    {
                        FileSystem.DeleteFile(
                            item.FullPath,
                            UIOption.OnlyErrorDialogs,
                            RecycleOption.SendToRecycleBin);
                    }
                    else
                    {
                        File.Delete(item.FullPath);
                    }
                    report.DeletedCount++;
                    report.FreedBytes += item.SizeBytes;
                }
                catch (Exception ex)
                {
                    report.Errors.Add($"{item.FullPath}: {ex.Message}");
                }
            }
            return report;
        }
    }
}
