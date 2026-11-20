using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Type { get; set; } = string.Empty;

        public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    }
}
