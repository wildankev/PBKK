using System.Windows;
using TutorMate.Models;
using TutorMate.Repositories;

namespace TutorMate.Views
{
    public partial class LoginWindow : Window
    {
        private UserRepository _userRepo;

        public LoginWindow()
        {
            InitializeComponent();
            _userRepo = new UserRepository();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            User loggedInUser = _userRepo.Authenticate(txtNIM.Text, txtPassword.Password);
            if (loggedInUser != null)
            {
                MainWindow dashboard = new MainWindow(loggedInUser);
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("NIM atau Password salah!", "Login Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBlock_Register_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RegisterWindow reg = new RegisterWindow();
            reg.Show();
            this.Close();
        }
    }
}