namespace PCResourceManager.Models
{
    public class ProcessInfoModel
    {
        public int Pid { get; set; }
        public string Name { get; set; } = "";
        public long WorkingSetBytes { get; set; }
        public double CpuPercent { get; set; }
        public bool IsSystemProcess { get; set; }
    }
}