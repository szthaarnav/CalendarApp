using System.Windows;
using CalenderApp.Repositories;
using CalenderApp.Services;
using CalenderApp.ViewModels;

namespace CalenderApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 1. Create the database connection
            var repository = new JsonCalendarRepository();

            // 2. Pass it to the business logic layer
            var service = new CalendarService(repository);

            // 3. Pass the service to the UI logic layer
            var viewModel = new MainViewModel(service);

            // 4. Tell the window to use this ViewModel for all data binding
            this.DataContext = viewModel;
        }
    }
}