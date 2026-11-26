using IDE_Booster;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDEBooster;

public class OptimizationManager
{
    private Dictionary<int, System.Diagnostics.ProcessPriorityClass>
        _originalPriorities = new();

    public PerformanceMode CurrentMode { get; set; }
    public bool IsActive { get; set; }

}