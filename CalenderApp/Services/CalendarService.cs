using System;
using System.Collections.Generic;
using System.Linq;
using CalenderApp.Models;
using CalenderApp.Repositories;

namespace CalenderApp.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _repository;

        // Dependency Injection: The service requires a repository to work.
        public CalendarService(ICalendarRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CalendarEntry> GetAllEntries()
        {
            // We use LINQ to automatically sort events by date
            return _repository.GetAll().OrderBy(e => e.EventDate);
        }

        public IEnumerable<CalendarEntry> GetEntriesByDateRange(DateTime start, DateTime end)
        {
            // LINQ filtering based on our date range
            return _repository.GetAll()
                .Where(e => e.EventDate >= start && e.EventDate <= end)
                .OrderBy(e => e.EventDate);
        }

        public void AddEntry(CalendarEntry entry)
        {
            ValidateEntry(entry); // Enforce business rules first
            _repository.Add(entry);
        }

        public void UpdateEntry(CalendarEntry entry)
        {
            ValidateEntry(entry); // Enforce business rules first
            _repository.Update(entry);
        }

        public void DeleteEntry(Guid id)
        {
            _repository.Delete(id);
        }

        // Business Logic: Prevent bad data from entering the repository
        private void ValidateEntry(CalendarEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                throw new ArgumentException("Event title cannot be empty.");
            }
            if (entry.EventDate == DateTime.MinValue)
            {
                throw new ArgumentException("A valid event date is required.");
            }
        }
    }
}