using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string InventoryNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; } = "Working";

        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
