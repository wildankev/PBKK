using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using StudentRegistrationApp.Data;
using StudentRegistrationApp.Models;

namespace StudentRegistrationApp.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentRepository _repository;

        public ObservableCollection<Student> Students { get; } = new();
        public ObservableCollection<ProgramStudy> Programs { get; } = new();
        public ObservableCollection<string> GenderList { get; } = new() { "Laki-laki", "Perempuan" };

        private int _studentId;
        private string _nim = string.Empty;
        private string _fullName = string.Empty;
        private string _email = string.Empty;
        private string _gender = "Laki-laki";
        private DateTime? _birthDate = new DateTime(2004, 5, 15);
        private string _phoneNumber = string.Empty;
        private string _address = string.Empty;
        private ProgramStudy? _selectedProgram;

        private string _searchKeyword = string.Empty;
        private ProgramStudy? _filterProgram;
        private Student? _selectedStudent;
        private string _statusMessage = "Aplikasi siap.";

        public event PropertyChangedEventHandler? PropertyChanged;

        public StudentViewModel()
        {
            _repository = new StudentRepository();

            SaveCommand = new RelayCommand(ExecuteSave, CanSave);
            ResetFormCommand = new RelayCommand(ExecuteResetForm);
            DeleteCommand = new RelayCommand(ExecuteDelete);
            SearchCommand = new RelayCommand(ExecuteSearch);
            RefreshCommand = new RelayCommand(ExecuteRefresh);

            LoadPrograms();
            LoadStudents();
        }

        #region Form Properties

        public int StudentId
        {
            get => _studentId;
            set
            {
                if (_studentId != value)
                {
                    _studentId = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEditMode));
                    OnPropertyChanged(nameof(FormTitle));
                    OnPropertyChanged(nameof(SaveButtonText));
                }
            }
        }

        public string NIM
        {
            get => _nim;
            set
            {
                if (_nim != value)
                {
                    _nim = value;
                    OnPropertyChanged();
                    // Auto-fill student email format if empty or matching default
                    if (!IsEditMode && (string.IsNullOrWhiteSpace(_email) || _email.EndsWith("@student.its.ac.id")))
                    {
                        Email = string.IsNullOrWhiteSpace(value) ? "" : $"{value.Trim()}@student.its.ac.id";
                    }
                }
            }
        }

        public string FullName
        {
            get => _fullName;
            set { if (_fullName != value) { _fullName = value; OnPropertyChanged(); } }
        }

        public string Email
        {
            get => _email;
            set { if (_email != value) { _email = value; OnPropertyChanged(); } }
        }

        public string Gender
        {
            get => _gender;
            set { if (_gender != value) { _gender = value; OnPropertyChanged(); } }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set { if (_birthDate != value) { _birthDate = value; OnPropertyChanged(); } }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { if (_phoneNumber != value) { _phoneNumber = value; OnPropertyChanged(); } }
        }

        public string Address
        {
            get => _address;
            set { if (_address != value) { _address = value; OnPropertyChanged(); } }
        }

        public ProgramStudy? SelectedProgram
        {
            get => _selectedProgram;
            set { if (_selectedProgram != value) { _selectedProgram = value; OnPropertyChanged(); } }
        }

        public bool IsEditMode => StudentId > 0;
        public string FormTitle => IsEditMode ? "Ubah Data Mahasiswa" : "Pendaftaran Mahasiswa Baru";
        public string SaveButtonText => IsEditMode ? "Simpan Perubahan" : "Daftarkan Mahasiswa";

        #endregion

        #region Search & Selection

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (_searchKeyword != value)
                {
                    _searchKeyword = value;
                    OnPropertyChanged();
                    ExecuteSearch();
                }
            }
        }

        public ProgramStudy? FilterProgram
        {
            get => _filterProgram;
            set
            {
                if (_filterProgram != value)
                {
                    _filterProgram = value;
                    OnPropertyChanged();
                    ExecuteSearch();
                }
            }
        }

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    OnPropertyChanged();
                    if (value != null)
                    {
                        PopulateForm(value);
                    }
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { if (_statusMessage != value) { _statusMessage = value; OnPropertyChanged(); } }
        }

        #endregion

        #region Statistics & Badges

        public int TotalStudents => Students.Count;
        public int TotalInformatika => Students.Count(s => s.ProgramName.Contains("Informatika", StringComparison.OrdinalIgnoreCase) || s.ProgramId == 1);
        public int TotalSistemInformasi => Students.Count(s => s.ProgramName.Contains("Sistem Informasi", StringComparison.OrdinalIgnoreCase) || s.ProgramId == 2);

        public string DatabaseStatus => _repository.IsConnectedToDatabase
            ? "Terhubung ke SQL Server LocalDB"
            : "Mode Offline (In-Memory Fallback)";

        public bool IsDatabaseOnline => _repository.IsConnectedToDatabase;

        #endregion

        #region Commands

        public ICommand SaveCommand { get; }
        public ICommand ResetFormCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(NIM) &&
                   !string.IsNullOrWhiteSpace(FullName) &&
                   SelectedProgram != null;
        }

        private void ExecuteSave()
        {
            if (string.IsNullOrWhiteSpace(NIM) || string.IsNullOrWhiteSpace(FullName))
            {
                MessageBox.Show("NIM dan Nama Lengkap wajib diisi!", "Validasi Formulir", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedProgram == null)
            {
                MessageBox.Show("Silakan pilih Program Studi!", "Validasi Formulir", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ensure email follows [NRP]@student.its.ac.id if empty
            string finalEmail = string.IsNullOrWhiteSpace(Email)
                ? $"{NIM.Trim()}@student.its.ac.id"
                : Email.Trim();

            var student = new Student
            {
                StudentId = this.StudentId,
                NIM = this.NIM.Trim(),
                FullName = this.FullName.Trim(),
                Email = finalEmail,
                Gender = string.IsNullOrWhiteSpace(this.Gender) ? "Laki-laki" : this.Gender,
                BirthDate = this.BirthDate,
                PhoneNumber = this.PhoneNumber?.Trim() ?? "",
                Address = this.Address?.Trim() ?? "",
                ProgramId = SelectedProgram.ProgramId,
                ProgramName = SelectedProgram.ProgramName
            };

            bool success;
            string error;

            if (IsEditMode)
            {
                success = _repository.UpdateStudent(student, out error);
                if (success)
                {
                    StatusMessage = $"Berhasil memperbarui data mahasiswa {student.FullName} ({student.NIM}).";
                    MessageBox.Show("Data mahasiswa berhasil diperbarui!", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Gagal memperbarui data: {error}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                success = _repository.InsertStudent(student, out error);
                if (success)
                {
                    StatusMessage = $"Berhasil mendaftarkan mahasiswa {student.FullName} ({student.NIM}).";
                    MessageBox.Show("Pendaftaran mahasiswa baru berhasil disimpan ke database!", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Gagal mendaftarkan mahasiswa: {error}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            ExecuteResetForm();
            LoadStudents();
        }

        private void ExecuteResetForm()
        {
            StudentId = 0;
            NIM = string.Empty;
            FullName = string.Empty;
            Email = string.Empty;
            Gender = "Laki-laki";
            BirthDate = new DateTime(2004, 5, 15);
            PhoneNumber = string.Empty;
            Address = string.Empty;
            SelectedProgram = Programs.FirstOrDefault();
            _selectedStudent = null;
            OnPropertyChanged(nameof(SelectedStudent));
            StatusMessage = "Formulir siap untuk pendaftaran baru.";
        }

        private void ExecuteDelete(object? parameter)
        {
            Student? target = parameter as Student ?? SelectedStudent;
            if (target == null)
            {
                MessageBox.Show("Pilih data mahasiswa yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus data mahasiswa:\n\nNIM: {target.NIM}\nNama: {target.FullName}\nProgram Studi: {target.ProgramName}?",
                "Konfirmasi Hapus Data",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                if (_repository.DeleteStudent(target.StudentId, out string error))
                {
                    StatusMessage = $"Data mahasiswa {target.FullName} ({target.NIM}) berhasil dihapus.";
                    if (SelectedStudent?.StudentId == target.StudentId)
                    {
                        ExecuteResetForm();
                    }
                    LoadStudents();
                }
                else
                {
                    MessageBox.Show($"Gagal menghapus data: {error}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExecuteSearch()
        {
            int? progId = FilterProgram?.ProgramId;
            var list = _repository.GetAllStudents(SearchKeyword, progId);

            Students.Clear();
            foreach (var item in list)
            {
                Students.Add(item);
            }

            NotifyStatistics();
        }

        private void ExecuteRefresh()
        {
            SearchKeyword = string.Empty;
            FilterProgram = null;
            LoadPrograms();
            LoadStudents();
            StatusMessage = "Data berhasil disinkronkan.";
        }

        #endregion

        #region Helpers

        private void LoadPrograms()
        {
            Programs.Clear();
            var list = _repository.GetPrograms();
            foreach (var p in list)
            {
                Programs.Add(p);
            }

            if (SelectedProgram == null && Programs.Count > 0)
            {
                SelectedProgram = Programs.First();
            }
        }

        private void LoadStudents()
        {
            int? progId = FilterProgram?.ProgramId;
            var list = _repository.GetAllStudents(SearchKeyword, progId);

            Students.Clear();
            foreach (var s in list)
            {
                Students.Add(s);
            }

            NotifyStatistics();
        }

        private void PopulateForm(Student student)
        {
            StudentId = student.StudentId;
            NIM = student.NIM;
            FullName = student.FullName;
            Email = student.Email;
            Gender = student.Gender;
            BirthDate = student.BirthDate;
            PhoneNumber = student.PhoneNumber;
            Address = student.Address;
            SelectedProgram = Programs.FirstOrDefault(p => p.ProgramId == student.ProgramId) ?? Programs.FirstOrDefault();
            StatusMessage = $"Mengedit data mahasiswa: {student.FullName}";
        }

        private void NotifyStatistics()
        {
            OnPropertyChanged(nameof(TotalStudents));
            OnPropertyChanged(nameof(TotalInformatika));
            OnPropertyChanged(nameof(TotalSistemInformasi));
            OnPropertyChanged(nameof(DatabaseStatus));
            OnPropertyChanged(nameof(IsDatabaseOnline));
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
