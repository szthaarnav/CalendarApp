using System;
using CalenderApp.Models;

namespace CalenderApp.ViewModels
{
    public class EntryViewModel : ViewModelBase
    {
        public CalendarEntry CurrentEntry { get; private set; }

        // If an entry is passed in, we are editing. If null, we are creating a new one.
        public EntryViewModel(CalendarEntry entry = null)
        {
            CurrentEntry = entry ?? new CalendarEntry { EventDate = DateTime.Today };
        }
    }
}