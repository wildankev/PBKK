using System.Windows;
using StudentRegistrationApp.ViewModels;

namespace StudentRegistrationApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StudentViewModel();
        }
    }
}
