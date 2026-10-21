using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using StudentManager.Data;
using StudentManager.Models;

namespace StudentManager.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentRepository _repository = new();

        public ObservableCollection<Student> Students { get; } = new();

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    OnPropertyChanged();

                    if (_selectedStudent != null)
                    {
                        CurrentStudent = _selectedStudent.Clone();
                        StatusMessage = $"Mengedit data: {_selectedStudent.Nama}";
                    }
                }
            }
        }

        private Student _currentStudent = new();
        public Student CurrentStudent
        {
            get => _currentStudent;
            set
            {
                _currentStudent = value;
                OnPropertyChanged();
            }
        }

        private string _searchKeyword = string.Empty;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
            }
        }

        private string _statusMessage = "Aplikasi siap digunakan.";
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public List<string> JurusanList { get; } = new()
        {
            "Teknik Informatika",
            "Sistem Informasi",
            "Sains Data",
            "Teknik Komputer",
            "Rekayasa Perangkat Lunak"
        };

        public List<string> GenderList { get; } = new()
        {
            "Laki-laki",
            "Perempuan"
        };

        // Statistik
        public int TotalStudents => Students.Count;
        public int TotalInformatika => Students.Count(s => s.Jurusan == "Teknik Informatika");
        public int TotalSistemInformasi => Students.Count(s => s.Jurusan == "Sistem Informasi");
        public string DatabaseStatus => _repository.IsDatabaseConnected ? "SQL LocalDB: Aktif" : "Storage: Memory (Demo)";

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand LoadAllCommand { get; }

        public StudentViewModel()
        {
            SaveCommand = new RelayCommand(_ => ExecuteSave(), _ => CanExecuteSave());
            ResetCommand = new RelayCommand(_ => ExecuteReset());
            DeleteCommand = new RelayCommand(param => ExecuteDelete(param as Student ?? SelectedStudent));
            SearchCommand = new RelayCommand(_ => ExecuteSearch());
            LoadAllCommand = new RelayCommand(_ => ExecuteLoadAll());

            LoadData();
        }

        private void LoadData(string keyword = "")
        {
            var data = _repository.GetAll(keyword);
            Students.Clear();
            foreach (var s in data)
            {
                Students.Add(s);
            }
            RefreshStatistics();
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrWhiteSpace(CurrentStudent.NIM) &&
                   !string.IsNullOrWhiteSpace(CurrentStudent.Nama) &&
                   !string.IsNullOrWhiteSpace(CurrentStudent.Jurusan) &&
                   !string.IsNullOrWhiteSpace(CurrentStudent.Gender);
        }

        private void ExecuteSave()
        {
            try
            {
                bool isNew = CurrentStudent.Id == 0;
                _repository.Save(CurrentStudent);
                LoadData(SearchKeyword);

                StatusMessage = isNew
                    ? $"Data mahasiswa '{CurrentStudent.Nama}' berhasil disimpan!"
                    : $"Data mahasiswa '{CurrentStudent.Nama}' berhasil diperbarui!";

                MessageBox.Show(StatusMessage, "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                ExecuteReset();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                MessageBox.Show(ex.Message, "Gagal Menyimpan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteReset()
        {
            CurrentStudent = new Student();
            SelectedStudent = null;
            StatusMessage = "Formulir siap untuk data baru.";
        }

        private void ExecuteDelete(Student? target)
        {
            if (target == null) return;

            var confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus mahasiswa: {target.Nama} ({target.NIM})?",
                "Konfirmasi Hapus",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.Delete(target.Id);
                    LoadData(SearchKeyword);
                    ExecuteReset();
                    StatusMessage = $"Mahasiswa '{target.Nama}' berhasil dihapus.";
                    MessageBox.Show(StatusMessage, "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error: {ex.Message}";
                    MessageBox.Show(ex.Message, "Gagal Menghapus", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExecuteSearch()
        {
            LoadData(SearchKeyword);
            StatusMessage = $"Hasil pencarian untuk '{SearchKeyword}': ditemukan {Students.Count} mahasiswa.";
        }

        private void ExecuteLoadAll()
        {
            SearchKeyword = string.Empty;
            LoadData();
            StatusMessage = "Memuat seluruh data mahasiswa.";
        }

        private void RefreshStatistics()
        {
            OnPropertyChanged(nameof(TotalStudents));
            OnPropertyChanged(nameof(TotalInformatika));
            OnPropertyChanged(nameof(TotalSistemInformasi));
            OnPropertyChanged(nameof(DatabaseStatus));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
