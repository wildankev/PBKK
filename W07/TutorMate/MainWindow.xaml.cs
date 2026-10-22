using System.Windows;
using TutorMate.Models;
using TutorMate.Repositories;
using TutorMate.Views;

namespace TutorMate
{
    public partial class MainWindow : Window
    {
        private User _currentUser;
        private TutorRepository _tutorRepo;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _tutorRepo = new TutorRepository();

            TxtInfoUser.Text = $"{_currentUser.Nama} ({_currentUser.Role})";

            if (_currentUser.Role == "Peserta")
            {
                BtnFormTambahMK.Visibility = Visibility.Collapsed;
                BtnFormDaftarTutor.Visibility = Visibility.Visible;
            }
            else if (_currentUser.Role == "Mentor")
            {
                BtnFormDaftarTutor.Visibility = Visibility.Collapsed;
                BtnFormTambahMK.Visibility = Visibility.Visible;
            }

            LoadData();
        }

        private void LoadData()
        {
            DgTutor.ItemsSource = _tutorRepo.GetAllTutors();
            DgMatkul.ItemsSource = _tutorRepo.GetAllSubjects();
            CmbSubjectList.ItemsSource = _tutorRepo.GetAllSubjects();
        }

        private void MenuDaftarTutor_Click(object sender, RoutedEventArgs e)
        {
            SembunyikanSemuaPanel();
            PanelDaftarTutor.Visibility = Visibility.Visible;
            TxtJudulHalaman.Text = "List Tutor";
            LoadData();
        }

        private void MenuKatalogMK_Click(object sender, RoutedEventArgs e)
        {
            SembunyikanSemuaPanel();
            PanelKatalogMK.Visibility = Visibility.Visible;
            TxtJudulHalaman.Text = "Katalog Mata Kuliah";
            LoadData();
        }

        private void MenuFormDaftarTutor_Click(object sender, RoutedEventArgs e)
        {
            SembunyikanSemuaPanel();
            PanelFormDaftarTutor.Visibility = Visibility.Visible;
            TxtJudulHalaman.Text = "Form Pendaftaran Tutor";
            LoadData();
        }

        private void MenuFormTambahMK_Click(object sender, RoutedEventArgs e)
        {
            SembunyikanSemuaPanel();
            PanelFormTambahMK.Visibility = Visibility.Visible;
            TxtJudulHalaman.Text = "Tambah Mata Kuliah Baru";
        }

        private void BtnSimpanTutor_Click(object sender, RoutedEventArgs e)
        {
            if (CmbSubjectList.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih mata kuliah terlebih dahulu!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _tutorRepo.RegisterTutor(new TutorApplication
            {
                TutorId = _currentUser.UserId,
                SubjectId = (int)CmbSubjectList.SelectedValue,
                NilaiHuruf = "-",
                HariJadwal = TxtHariJadwal.Text,
                TopikKeahlian = "-"
            });

            MessageBox.Show("Berhasil Mendaftar!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            MenuDaftarTutor_Click(sender, e);
        }

        private void BtnSimpanMK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtKodeMK.Text) || string.IsNullOrWhiteSpace(TxtNamaMK.Text))
            {
                MessageBox.Show("Kode dan Nama Mata Kuliah wajib diisi!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _tutorRepo.InsertSubject(new Subject
            {
                KodeMK = TxtKodeMK.Text,
                NamaMataKuliah = TxtNamaMK.Text,
                Deskripsi = TxtDeskripsiMK.Text
            });

            MessageBox.Show("Berhasil Menambahkan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            MenuKatalogMK_Click(sender, e);
        }

        private void SembunyikanSemuaPanel()
        {
            PanelDaftarTutor.Visibility = Visibility.Collapsed;
            PanelKatalogMK.Visibility = Visibility.Collapsed;
            PanelFormDaftarTutor.Visibility = Visibility.Collapsed;
            PanelFormTambahMK.Visibility = Visibility.Collapsed;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}