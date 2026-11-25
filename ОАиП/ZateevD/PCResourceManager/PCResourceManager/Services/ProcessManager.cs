using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PCResourceManager.Models;
namespace PCResourceManager.Services
{
    public class ProcessManager
    {
        private static readonly HashSet<string> SystemNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "System", "Idle", "csrss", "wininit", "services", "lsass", "smss", "winlogon", "svchost", "explorer"
        };
        public List<ProcessInfoModel> GetProcesses()
        {
            var result = new List<ProcessInfoModel>();
            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    result.Add(new ProcessInfoModel
                    {
                        Pid = p.Id,
                        Name = p.ProcessName,
                        WorkingSetBytes = p.WorkingSet64,
                        IsSystemProcess = SystemNames.Contains(p.ProcessName)
                    });
                }
                catch
                {
                }
                finally
                {
                    p.Dispose();
                }
            }
            return result.OrderByDescending(p => p.WorkingSetBytes).ToList();
        }
        public bool KillProcess(int pid)
        {
            try
            {
                using var p = Process.GetProcessById(pid);
                if (SystemNames.Contains(p.ProcessName))
                    return false;
                p.Kill();
                p.WaitForExit(2000);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}