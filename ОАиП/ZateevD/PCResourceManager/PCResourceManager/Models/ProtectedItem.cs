using System;
using System.Collections.Generic;
using System.Text;
namespace PCResourceManager.Models
{
    public class ProtectedItem
    {
        public string Path { get; set; } = "";
        public bool IsFolder { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}