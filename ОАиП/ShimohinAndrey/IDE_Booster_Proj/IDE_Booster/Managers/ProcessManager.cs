
using System.Diagnostics;

namespace IDE_Booster;

public class ProcessManager
{
    public Process[] FindProcessesByName(string processName)
    {
        return Process.GetProcessesByName(processName);
    }

    public ProcessPriorityClass GetPriority(Process process)
    {
        return process.PriorityClass;
    }

    public void ChangePriority(
        Process process,
        ProcessPriorityClass priority)
    {
        process.PriorityClass = priority;
    }
}
