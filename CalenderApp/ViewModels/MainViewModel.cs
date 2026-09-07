using System.Collections.ObjectModel;
using System.Windows.Input;
using CalenderApp.Models;
using CalenderApp.Services;

namespace CalenderApp.ViewModels
{
    // Inheriting from ViewModelBase gives us the OnPropertyChanged functionality
    public class MainViewModel : ViewModelBase
    {
        private readonly ICalendarService _service;
        private ObservableCollection<CalendarEntry> _entries;
        private CalendarEntry _selectedEntry;

        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddMonths(1);

        // ObservableCollection automatically tells the UI to redraw when items are added/removed
        public ObservableCollection<CalendarEntry> Entries
        {
            get => _entries;
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }

        // Tracks which event the user has clicked on in the DataGrid/List
        public CalendarEntry SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                _selectedEntry = value;
                OnPropertyChanged();
                // Tell the UI to check if buttons (like Delete) should be enabled or disabled
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(ICalendarService service)
        {
            _service = service;

            // Map the commands to the methods below
            AddCommand = new RelayCommand(_ => ExecuteAdd());
            EditCommand = new RelayCommand(_ => ExecuteEdit(), CanEdit);
            LoadCommand = new RelayCommand(_ => LoadData());
            FilterCommand = new RelayCommand(_ => ExecuteFilter());
            ClearFilterCommand = new RelayCommand(_ => LoadData()); // Re-loads all data
            DeleteCommand = new RelayCommand(ExecuteDelete, CanDelete);

            // Load data immediately when the app starts
            LoadData();
        }

        private void LoadData()
        {
            var data = _service.GetAllEntries();
            Entries = new ObservableCollection<CalendarEntry>(data);
        }

        private void ExecuteAdd()
        {
            // 1. Create the new ViewModel
            var entryViewModel = new EntryViewModel();

            // 2. Create the Window and assign the ViewModel
            var window = new Views.EntryWindow
            {
                DataContext = entryViewModel
            };

            // 3. Show the window as a blocking dialog. If the user clicks Save, it returns true.
            if (window.ShowDialog() == true)
            {
                // 4. Pass the new data to the Service layer to validate and save
                _service.AddEntry(entryViewModel.CurrentEntry);

                // 5. Refresh the UI grid
                LoadData();
            }
        }

        // The Edit button will be disabled if no row is selected
        private bool CanEdit(object parameter)
        {
            return SelectedEntry != null;
        }

        private void ExecuteEdit()
        {
            // 1. Clone the selected entry so we don't modify the grid directly until Save is clicked
            var entryToEdit = new CalendarEntry
            {
                Id = SelectedEntry.Id,
                Title = SelectedEntry.Title,
                Description = SelectedEntry.Description,
                EventDate = SelectedEntry.EventDate,
                CreatedAt = SelectedEntry.CreatedAt
            };

            // 2. Pass the cloned data into the EntryViewModel
            var entryViewModel = new EntryViewModel(entryToEdit);

            var window = new Views.EntryWindow
            {
                DataContext = entryViewModel
            };

            // 3. If they click Save, send the updated data to the Service layer
            if (window.ShowDialog() == true)
            {
                _service.UpdateEntry(entryViewModel.CurrentEntry);
                LoadData();
            }
        }

        private void ExecuteFilter()
        {
            var filteredData = _service.GetEntriesByDateRange(StartDate, EndDate);
            Entries = new ObservableCollection<CalendarEntry>(filteredData);
        }

        // The Delete button will be grayed out/disabled if this returns false
        private bool CanDelete(object parameter)
        {
            return SelectedEntry != null;
        }

        private void ExecuteDelete(object parameter)
        {
            _service.DeleteEntry(SelectedEntry.Id);
            LoadData(); // Refresh the visual list after deletion
        }
    }
}