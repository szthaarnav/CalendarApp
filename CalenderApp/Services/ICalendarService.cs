using System;
using System.Collections.Generic;
using CalenderApp.Models;

namespace CalenderApp.Services
{
    public interface ICalendarService
    {
        IEnumerable<CalendarEntry> GetAllEntries();

        // This is our new filtering requirement
        IEnumerable<CalendarEntry> GetEntriesByDateRange(DateTime start, DateTime end);

        void AddEntry(CalendarEntry entry);
        void UpdateEntry(CalendarEntry entry);
        void DeleteEntry(Guid id);
    }
}