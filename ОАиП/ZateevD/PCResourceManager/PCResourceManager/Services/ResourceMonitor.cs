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

    }
}