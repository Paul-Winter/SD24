using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using PCResourceManager.Models;
namespace PCResourceManager.Services
{
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(UserSettings))]
    [JsonSerializable(typeof(List<ProtectedItem>))]
    [JsonSerializable(typeof(ProtectedItem))]
    public partial class SettingsJsonContext : JsonSerializerContext
    {

    }
}