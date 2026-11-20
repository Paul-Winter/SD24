using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Room> Rooms { get; } = new();

        public MainWindowViewModel(AppDbContext context)
        {
            _context = context;
            LoadRooms();
        }

        private async void LoadRooms()
        {
            try
            {
                var roomsFromDb = await _context.Rooms.ToListAsync();

                Rooms.Clear();
                foreach (var room in roomsFromDb)
                {
                    Rooms.Add(room);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}