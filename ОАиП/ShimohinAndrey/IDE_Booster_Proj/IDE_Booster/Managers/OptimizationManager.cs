
using System.Diagnostics;

namespace IDE_Booster;

public class OptimizationManager
{
    private readonly ProcessManager _processManager;

    private readonly Dictionary<int, ProcessPriorityClass>
        _originalPriorities = new();

    public bool IsActive { get; private set; }

    public OptimizationManager(ProcessManager processManager)
    {
        _processManager = processManager;
    }

    public bool Start(string processName)
    {
        if (IsActive)
            return false;

        Process[] processes =
            _processManager.FindProcessesByName(processName);

        if (processes.Length == 0)
            return false;

        bool success = false;

        foreach (Process process in processes)
        {
            try
            {
                int processId = process.Id;

                ProcessPriorityClass originalPriority =
                    _processManager.GetPriority(process);

                _processManager.ChangePriority(
                    process,
                    ProcessPriorityClass.AboveNormal
                );

                _originalPriorities[processId] = originalPriority;
                success = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                process.Dispose();
            }
        }

        IsActive = success;

        return success;
    }

    public void Stop()
    {
        foreach (var item in _originalPriorities)
        {
            try
            {
                using Process process =
                    Process.GetProcessById(item.Key);

                _processManager.ChangePriority(
                    process,
                    item.Value
                );
            }
            catch (Exception)
            {
            }
        }

        _originalPriorities.Clear();
        IsActive = false;
    }
}
