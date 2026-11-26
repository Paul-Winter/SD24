using IDE_Booster;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDEBooster;

public class SettingsManager
{
    public string LastIdePath { get; set; } = "";

    public PerformanceMode LastMode { get; set; }
        = PerformanceMode.Standard;

    public List<string> SecondaryProcesses { get; set; } = new();

}
