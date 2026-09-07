using System;

namespace CalenderApp.Models
{
    public class CalendarEntry
    {
        // A globally unique identifier (Primary Key) for finding/deleting entries
        public Guid Id { get; set; } = Guid.NewGuid();

        // The name of the event
        public string Title { get; set; } = string.Empty;

        // Extra details about the event
        public string Description { get; set; } = string.Empty;

        // The scheduled date and time of the event
        public DateTime EventDate { get; set; }

        // A background timestamp to track when the user created this record
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}