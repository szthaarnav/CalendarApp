using System;
using System.Collections.Generic;
using CalenderApp.Models;

namespace CalenderApp.Repositories
{
    public interface ICalendarRepository
    {
        IEnumerable<CalendarEntry> GetAll();
        void Add(CalendarEntry entry);
        void Update(CalendarEntry entry);
        void Delete(Guid id);
    }
}