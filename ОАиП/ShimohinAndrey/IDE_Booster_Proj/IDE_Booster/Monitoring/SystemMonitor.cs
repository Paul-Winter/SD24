using System;
using System.Collections.Generic;
using System.Text;

namespace IDE_Booster;

public class SystemMonitor
{
    public double CpuLoad { get; private set; }
    public double RamUsedMb { get; private set; }
    public double RamTotalMb { get; private set; }

    public int ProcessCount { get; private set; }

    public double IdeRamUsageMb { get; private set; }
    public double IdeCpuUsage { get; private set; }

}