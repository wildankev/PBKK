using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TutorMate.Commands;
using TutorMate.Models;
using TutorMate.Repositories;

namespace TutorMate.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly UserRepository _repository;
        private ObservableCollection<User> _users;
        private User _selectedUser;

        public MainViewModel()
        {
            _repository = new UserRepository();
            SelectedUser = new User();
            LoadData();

            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete, CanModify);
            ClearCommand = new RelayCommand(ClearForm);
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set { _users = value; OnPropertyChanged(); }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value ?? new User();
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        private void LoadData()
        {
            var data = _repository.GetAll();
            Users = new ObservableCollection<User>(data);
        }

        private void Save(object obj)
        {
            if (string.IsNullOrWhiteSpace(SelectedUser.NIM) || string.IsNullOrWhiteSpace(SelectedUser.Nama))
            {
                MessageBox.Show("NIM dan Nama wajib diisi!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedUser.UserId == 0)
                _repository.Insert(SelectedUser);
            else
                _repository.Update(SelectedUser);

            LoadData();
            ClearForm(null);
            MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Delete(object obj)
        {
            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _repository.Delete(SelectedUser.UserId);
                LoadData();
                ClearForm(null);
            }
        }

        private void ClearForm(object obj)
        {
            SelectedUser = new User();
        }

        private bool CanModify(object obj) => SelectedUser != null && SelectedUser.UserId > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}