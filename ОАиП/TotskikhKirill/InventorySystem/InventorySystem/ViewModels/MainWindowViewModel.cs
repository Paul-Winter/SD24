using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventorySystem.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Room> Rooms { get; } = new();

        public ObservableCollection<Equipment> SelectedRoomEquipment { get; } = new();

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged();
                if (DeleteRoomCommand is RelayCommand relayCmd)
                {
                    relayCmd.RaiseCanExecuteChanged();
                }
            }
        }

        private string _currentRoomTitle = "Оборудование";
        public string CurrentRoomTitle
        {
            get => _currentRoomTitle;
            set { _currentRoomTitle = value; OnPropertyChanged(); }
        }

        private string _newRoomNumber = string.Empty;
        public string NewRoomNumber
        {
            get => _newRoomNumber;
            set { _newRoomNumber = value; OnPropertyChanged(); }
        }

        private string _newRoomCapacity = string.Empty;
        public string NewRoomCapacity
        {
            get => _newRoomCapacity;
            set { _newRoomCapacity = value; OnPropertyChanged(); }
        }

        public ICommand SaveRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public ICommand OpenRoomEquipmentCommand { get; }

        public MainWindowViewModel(AppDbContext context)
        {
            _context = context;
            SaveRoomCommand = new RelayCommand(async _ => await SaveRoomAsync());
            DeleteRoomCommand = new RelayCommand(async _ => await DeleteRoomAsync(), _ => SelectedRoom != null);

            OpenRoomEquipmentCommand = new RelayCommand(async _ => await LoadEquipmentForRoomAsync());

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
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }

        private async Task LoadEquipmentForRoomAsync()
        {
            if (SelectedRoom == null) return;

            try
            {
                var equipment = await _context.Equipment
                    .Where(e => e.RoomId == SelectedRoom.Id)
                    .ToListAsync();

                SelectedRoomEquipment.Clear();
                foreach (var eq in equipment)
                {
                    SelectedRoomEquipment.Add(eq);
                }

                CurrentRoomTitle = $"Оборудование: Аудитория {SelectedRoom.Number}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки оборудования: {ex.Message}");
            }
        }

        private async Task SaveRoomAsync()
        {
            if (string.IsNullOrWhiteSpace(NewRoomNumber)) return;
            if (!int.TryParse(NewRoomCapacity, out int capacity) || capacity <= 0) return;

            try
            {
                var newRoom = new Room { Number = NewRoomNumber, Capacity = capacity };
                _context.Rooms.Add(newRoom);
                await _context.SaveChangesAsync();

                NewRoomNumber = string.Empty;
                NewRoomCapacity = string.Empty;
                LoadRooms();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        private async Task DeleteRoomAsync()
        {
            if (SelectedRoom == null) return;

            try
            {
                var hasEquipment = await _context.Equipment.AnyAsync(e => e.RoomId == SelectedRoom.Id);
                if (hasEquipment) return;

                _context.Rooms.Remove(SelectedRoom);
                await _context.SaveChangesAsync();
                SelectedRoom = null;
                LoadRooms();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка удаления: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}