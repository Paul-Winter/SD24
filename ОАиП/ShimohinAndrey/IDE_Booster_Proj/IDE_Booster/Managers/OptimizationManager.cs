using IDE_Booster;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDE_Booster;

public class OptimizationManager
{
    private Dictionary<int, System.Diagnostics.ProcessPriorityClass>
        _originalPriorities = new();

    public PerformanceMode CurrentMode { get; set; }
    public bool IsActive { get; set; }

}