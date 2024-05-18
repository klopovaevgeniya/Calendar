using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Calendar.Model
{
    public class DayInfo
    {
        public List<bool> Checks { get; set; } = new List<bool>();

        public static void Serialize(Dictionary<DateTime, DayInfo> dayData, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(dayData, options);
            File.WriteAllText(filePath, jsonData);
        }

        public static Dictionary<DateTime, DayInfo> Deserialize(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Dictionary<DateTime, DayInfo>>(jsonData) ?? new Dictionary<DateTime, DayInfo>();
            }
            return new Dictionary<DateTime, DayInfo>();
        }
    }
}
