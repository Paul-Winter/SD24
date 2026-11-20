using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}