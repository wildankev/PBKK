using System.Windows;
using System.Windows.Controls;

namespace StudentRegistrationApp;

public partial class MainWindow : Window
{
    private int editingIndex = -1;

    public MainWindow()
    {
        InitializeComponent();
        SeedInitialData();
    }

    private void SeedInitialData()
    {
        // Data awal mahasiswa untuk demo tampilan
        lstMahasiswa.Items.Add("5025241265 | A. Wildan Kevin Assyauqi | Teknik Informatika | Laki-laki");
    }

    private void BtnSimpan_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        string nrp = txtNrp.Text.Trim();
        string nama = txtNama.Text.Trim();
        string prodi = ((ComboBoxItem)cmbProdi.SelectedItem).Content.ToString() ?? "";
        string jenisKelamin = rbLaki.IsChecked == true ? "Laki-laki" : "Perempuan";

        string dataMahasiswa = $"{nrp} | {nama} | {prodi} | {jenisKelamin}";

        if (editingIndex == -1)
        {
            // Mode Tambah Baru (Create)
            lstMahasiswa.Items.Add(dataMahasiswa);
            MessageBox.Show("Data mahasiswa berhasil disimpan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            // Mode Perbarui (Update)
            lstMahasiswa.Items[editingIndex] = dataMahasiswa;
            editingIndex = -1;
            btnSimpan.Content = "Simpan Data";
            MessageBox.Show("Data mahasiswa berhasil diperbarui!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        ClearForm();
    }

    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void BtnItemUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext != null)
        {
            var item = btn.DataContext;
            editingIndex = lstMahasiswa.Items.IndexOf(item);

            if (editingIndex != -1)
            {
                string rawData = item.ToString() ?? "";
                string[] parts = rawData.Split('|');

                if (parts.Length >= 4)
                {
                    txtNrp.Text = parts[0].Trim();
                    txtNama.Text = parts[1].Trim();

                    string prodi = parts[2].Trim();
                    foreach (ComboBoxItem cbItem in cmbProdi.Items)
                    {
                        if (cbItem.Content.ToString() == prodi)
                        {
                            cmbProdi.SelectedItem = cbItem;
                            break;
                        }
                    }

                    string jk = parts[3].Trim();
                    rbLaki.IsChecked = (jk == "Laki-laki");
                    rbPerempuan.IsChecked = (jk == "Perempuan");

                    btnSimpan.Content = "Update Data";
                }
            }
        }
    }

    private void BtnItemDelete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext != null)
        {
            var item = btn.DataContext;
            var confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data mahasiswa ini?", "Konfirmasi Hapus", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                lstMahasiswa.Items.Remove(item);
                ClearForm();
                MessageBox.Show("Data mahasiswa berhasil dihapus.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    private void ClearForm()
    {
        txtNrp.Clear();
        txtNama.Clear();
        cmbProdi.SelectedIndex = -1;
        rbLaki.IsChecked = false;
        rbPerempuan.IsChecked = false;
        editingIndex = -1;
        btnSimpan.Content = "Simpan Data";
        txtNrp.Focus();
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtNrp.Text))
        {
            MessageBox.Show("NRP mahasiswa wajib diisi!", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
            txtNrp.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtNama.Text))
        {
            MessageBox.Show("Nama lengkap mahasiswa wajib diisi!", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
            txtNama.Focus();
            return false;
        }

        if (cmbProdi.SelectedItem == null)
        {
            MessageBox.Show("Silakan pilih program studi terlebih dahulu!", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
            cmbProdi.Focus();
            return false;
        }

        if (rbLaki.IsChecked != true && rbPerempuan.IsChecked != true)
        {
            MessageBox.Show("Silakan pilih jenis kelamin!", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }
}
