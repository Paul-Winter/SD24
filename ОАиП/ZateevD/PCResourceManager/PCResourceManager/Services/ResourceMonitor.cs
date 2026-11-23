using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PCResourceManager.Models;
using System.Text;
using System.Management;
namespace PCResourceManager.Services
{
    public interface IResourceMonitor
    {
        double GetCpuUsage();
        (double usedGb, double totalGb) GetRamUsage();
        List<DriveInfo> GetDisks();
    }
    public class ResourceMonitor : IResourceMonitor, IDisposable
    {
        private readonly PerformanceCounter? _cpuCounter;
        public ResourceMonitor()
        {
            try
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _cpuCounter.NextValue();
            }
            catch
            {
                _cpuCounter = null;
            }
        }
        public double GetCpuUsage()
        {
            try { return _cpuCounter?.NextValue() ?? 0; }
            catch { return 0; }
        }
        public (double usedGb, double totalGb) GetRamUsage()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    "SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
                foreach (var obj in searcher.Get())
                {
                    double totalKb = Convert.ToDouble(obj["TotalVisibleMemorySize"]);
                    double freeKb = Convert.ToDouble(obj["FreePhysicalMemory"]);
                    double totalGb = totalKb / 1024.0 / 1024.0;
                    double usedGb = (totalKb - freeKb) / 1024.0 / 1024.0;
                    return (usedGb, totalGb);
                }
            }
            catch { }
            return (0, 0);
        }
        public List<DriveInfo> GetDisks()
        {
            return DriveInfo.GetDrives().ToList();
        }
        public void Dispose()
        {
            _cpuCounter?.Dispose();
        }
    }
}
