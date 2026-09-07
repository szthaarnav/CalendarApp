using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CalenderApp.Models;

namespace CalenderApp.Repositories
{
    public class JsonCalendarRepository : ICalendarRepository
    {
        // This file will be created in the same folder as your .exe file
        private readonly string _filePath = "calendar_data.json";

        public IEnumerable<CalendarEntry> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<CalendarEntry>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<CalendarEntry>>(json) ?? new List<CalendarEntry>();
        }

        public void Add(CalendarEntry entry)
        {
            var entries = GetAll().ToList();
            entries.Add(entry);
            SaveAll(entries);
        }

        public void Update(CalendarEntry entry)
        {
            var entries = GetAll().ToList();
            var index = entries.FindIndex(e => e.Id == entry.Id);
            if (index != -1)
            {
                entries[index] = entry;
                SaveAll(entries);
            }
        }

        public void Delete(Guid id)
        {
            var entries = GetAll().ToList();
            var entryToRemove = entries.FirstOrDefault(e => e.Id == id);
            if (entryToRemove != null)
            {
                entries.Remove(entryToRemove);
                SaveAll(entries);
            }
        }

        // A private helper method to handle the actual writing to the file
        private void SaveAll(IEnumerable<CalendarEntry> entries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(_filePath, json);
        }
    }
}