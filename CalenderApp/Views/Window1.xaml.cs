using System.Windows;

namespace CalenderApp.Views
{
    public partial class EntryWindow : Window
    {
        public EntryWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Tells the main window that the user successfully completed the form
            this.DialogResult = true;
            this.Close();
        }
    }
}