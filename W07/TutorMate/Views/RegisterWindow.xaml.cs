using System.Windows;
using TutorMate.Models;
using TutorMate.Repositories;

namespace TutorMate.Views
{
    public partial class RegisterWindow : Window
    {
        private UserRepository _userRepo;

        public RegisterWindow()
        {
            InitializeComponent();
            _userRepo = new UserRepository();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIM.Text) || string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Semua kolom wajib diisi!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _userRepo.Insert(new User
            {
                NIM = txtNIM.Text,
                Nama = txtNama.Text,
                Password = txtPassword.Password,
                Role = cmbRole.Text
            });

            MessageBox.Show("Registrasi Berhasil! Silakan Login.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void TextBlock_BackToLogin_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}