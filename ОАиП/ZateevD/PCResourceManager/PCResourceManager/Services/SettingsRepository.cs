using PCResourceManager.Models;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
namespace PCResourceManager.Services
{
    public class SettingsRepository
    {
        private readonly string _settingsPath;
        private readonly string _protectedPath;
        public SettingsRepository(string dataFolder)
        {
            Directory.CreateDirectory(dataFolder);
            _settingsPath = Path.Combine(dataFolder, "settings.json");
            _protectedPath = Path.Combine(dataFolder, "protected.json");
        }
        public UserSettings Load()
        {
            try
            {
                if (!File.Exists(_settingsPath)) return new UserSettings();
                var json = File.ReadAllText(_settingsPath);
                return JsonSerializer.Deserialize(json, SettingsJsonContext.Default.UserSettings)
                       ?? new UserSettings();
            }
            catch { return new UserSettings(); }
        }
        public void Save(UserSettings s) =>
            File.WriteAllText(_settingsPath,
                JsonSerializer.Serialize(s, SettingsJsonContext.Default.UserSettings));
        public List<ProtectedItem> LoadProtected()
        {
            try
            {
                if (!File.Exists(_protectedPath)) return [];
                var json = File.ReadAllText(_protectedPath);
                return JsonSerializer.Deserialize(json, SettingsJsonContext.Default.ListProtectedItem) ?? [];
            }
            catch { return []; }
        }
        public void SaveProtected(List<ProtectedItem> items) =>
            File.WriteAllText(_protectedPath,
                JsonSerializer.Serialize(items, SettingsJsonContext.Default.ListProtectedItem));
    }
}
