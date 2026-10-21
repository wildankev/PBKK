using System;
using System.Linq;
using SistemAkademik.Models;
using SistemAkademik.Services;
using SistemAkademik.Utils;

namespace SistemAkademik
{
    internal class Program
    {
        private static readonly AkademikService _service = new();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                ConsoleHelper.Clear();
                ConsoleHelper.PrintHeader("SISTEM MANAJEMEN AKADEMIK & KALKULATOR IPK (.NET 8)");
                Console.WriteLine("Praktikum PBKK W02 - .NET Framework & C# Console Application");
                Console.WriteLine("Pengembang: M. Wildan Kevin A. (5025241265) - Kelas PBKK C");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine(" [1] Tampilkan Semua Mahasiswa & Transkrip");
                Console.WriteLine(" [2] Tambah Mahasiswa Baru");
                Console.WriteLine(" [3] Cari Mahasiswa (NRP / Nama / Kelas)");
                Console.WriteLine(" [4] Kalkulator IPK & Kelola Nilai Mata Kuliah");
                Console.WriteLine(" [5] Ubah Data Profil Mahasiswa");
                Console.WriteLine(" [6] Hapus Data Mahasiswa");
                Console.WriteLine(" [7] Ringkasan Statistik Kelas");
                Console.WriteLine(" [0] Keluar Aplikasi");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.Write("Pilih menu (0-7): ");

                var line = Console.ReadLine();
                if (line == null) break;
                var pilihan = line.Trim('\uFEFF', ' ', '\r', '\n', '\t');
                if (string.IsNullOrEmpty(pilihan)) continue;

                switch (pilihan)
                {
                    case "1":
                        MenuTampilkanSemua();
                        break;
                    case "2":
                        MenuTambahMahasiswa();
                        break;
                    case "3":
                        MenuCariMahasiswa();
                        break;
                    case "4":
                        MenuKalkulatorIpk();
                        break;
                    case "5":
                        MenuUbahMahasiswa();
                        break;
                    case "6":
                        MenuHapusMahasiswa();
                        break;
                    case "7":
                        MenuStatistik();
                        break;
                    case "0":
                        Console.WriteLine("\nTerima kasih telah menggunakan sistem ini. Sampai jumpa!");
                        return;
                    default:
                        ConsoleHelper.PrintError($"Pilihan menu '{pilihan}' (length={pilihan.Length}) tidak valid. Silakan pilih 0-7.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }

        private static void MenuTampilkanSemua()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("DAFTAR SELURUH MAHASISWA & PRESTASI AKADEMIK");

            var list = _service.GetAll();
            if (!list.Any())
            {
                ConsoleHelper.PrintWarning("Belum ada data mahasiswa yang terdaftar.");
                ConsoleHelper.Pause();
                return;
            }

            PrintTabelMahasiswa(list);
            ConsoleHelper.Pause();
        }

        private static void PrintTabelMahasiswa(System.Collections.Generic.IEnumerable<Mahasiswa> list)
        {
            Console.WriteLine("+----+--------------+----------------------------+--------+------+-----+------+--------------------------+");
            Console.WriteLine("| No | NRP          | Nama Mahasiswa             | Kelas  | Angk | SKS | IPK  | Predikat Kelulusan       |");
            Console.WriteLine("+----+--------------+----------------------------+--------+------+-----+------+--------------------------+");

            int no = 1;
            foreach (var m in list)
            {
                var nama = m.Nama.Length > 26 ? m.Nama.Substring(0, 23) + "..." : m.Nama;
                Console.WriteLine($"| {no,2} | {m.Nrp,-12} | {nama,-26} | {m.Kelas,-6} | {m.Angkatan,-4} | {m.TotalSks,3} | {m.Ipk,4:F2} | {m.Predikat,-24} |");
                no++;
            }
            Console.WriteLine("+----+--------------+----------------------------+--------+------+-----+------+--------------------------+");
        }

        private static void MenuTambahMahasiswa()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("TAMBAH MAHASISWA BARU");

            var nrp = ConsoleHelper.ReadMandatoryString("Masukkan NRP Mahasiswa (contoh: 5025241001): ");
            var nama = ConsoleHelper.ReadMandatoryString("Masukkan Nama Lengkap: ");
            var kelas = ConsoleHelper.ReadOptionalString("Masukkan Kelas", "PBKK-C");
            var angkatan = ConsoleHelper.ReadOptionalString("Masukkan Angkatan", "2024");

            var baru = new Mahasiswa
            {
                Nrp = nrp,
                Nama = nama,
                Kelas = kelas,
                Angkatan = angkatan
            };

            if (_service.Add(baru, out var error))
            {
                ConsoleHelper.PrintSuccess($"Mahasiswa '{nama}' ({nrp}) berhasil ditambahkan ke database!");
            }
            else
            {
                ConsoleHelper.PrintError(error);
            }

            ConsoleHelper.Pause();
        }

        private static void MenuCariMahasiswa()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("PENCARIAN MAHASISWA");

            var kataKunci = ConsoleHelper.ReadMandatoryString("Ketik kata kunci (NRP / Nama / Kelas): ");
            var hasil = _service.Search(kataKunci);

            if (!hasil.Any())
            {
                ConsoleHelper.PrintWarning($"Tidak ditemukan mahasiswa dengan kata kunci '{kataKunci}'.");
            }
            else
            {
                ConsoleHelper.PrintSuccess($"Ditemukan {hasil.Count} mahasiswa yang cocok:");
                Console.WriteLine();
                PrintTabelMahasiswa(hasil);
            }

            ConsoleHelper.Pause();
        }

        private static void MenuKalkulatorIpk()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("KALKULATOR AKADEMIK & DETAIL TRANSKRIP NILAI");

            var nrp = ConsoleHelper.ReadMandatoryString("Masukkan NRP Mahasiswa: ");
            var mhs = _service.GetByNrp(nrp);

            if (mhs == null)
            {
                ConsoleHelper.PrintError($"Mahasiswa dengan NRP '{nrp}' tidak ditemukan.");
                ConsoleHelper.Pause();
                return;
            }

            while (true)
            {
                ConsoleHelper.Clear();
                ConsoleHelper.PrintHeader($"TRANSKRIP & KALKULATOR IPK: {mhs.Nama} ({mhs.Nrp})");
                Console.WriteLine($"Kelas       : {mhs.Kelas}");
                Console.WriteLine($"Angkatan    : {mhs.Angkatan}");
                Console.WriteLine($"Total SKS   : {mhs.TotalSks} SKS");
                Console.WriteLine($"IPK Saat Ini: {mhs.Ipk:F2} / 4.00");
                Console.WriteLine($"Predikat    : {mhs.Predikat}");
                Console.WriteLine($"Batas Beban : Maksimal {mhs.MaxSksBerikutnya} SKS untuk semester depan");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("Daftar Mata Kuliah yang Diambil:");
                
                if (!mhs.DaftarNilai.Any())
                {
                    Console.WriteLine("  (Belum ada mata kuliah yang diinputkan)");
                }
                else
                {
                    Console.WriteLine("+----+--------+------------------------------------+-----+-------+-------+------------+");
                    Console.WriteLine("| No | Kode   | Mata Kuliah                        | SKS | Nilai | Bobot | SKS x Bobot|");
                    Console.WriteLine("+----+--------+------------------------------------+-----+-------+-------+------------+");
                    int i = 1;
                    foreach (var mk in mhs.DaftarNilai)
                    {
                        var namaMk = mk.Nama.Length > 34 ? mk.Nama.Substring(0, 31) + "..." : mk.Nama;
                        Console.WriteLine($"| {i,2} | {mk.Kode,-6} | {namaMk,-34} | {mk.Sks,3} | {mk.NilaiHuruf,-5} | {mk.Bobot,5:F1} | {mk.TotalBobotSks,10:F1} |");
                        i++;
                    }
                    Console.WriteLine("+----+--------+------------------------------------+-----+-------+-------+------------+");
                }

                Console.WriteLine();
                Console.WriteLine("[1] Tambah Mata Kuliah & Hitung Ulang IPK");
                Console.WriteLine("[2] Hapus Mata Kuliah");
                Console.WriteLine("[0] Kembali ke Menu Utama");
                Console.Write("Pilihan (0-2): ");

                var subPilihan = Console.ReadLine()?.Trim();
                if (subPilihan == "0") break;

                if (subPilihan == "1")
                {
                    var kode = ConsoleHelper.ReadMandatoryString("Kode MK (contoh: IF4405): ").ToUpper();
                    var namaMk = ConsoleHelper.ReadMandatoryString("Nama Mata Kuliah: ");
                    var sks = ConsoleHelper.ReadInt("Jumlah SKS (1-6): ", 1, 6);
                    
                    string nilaiHuruf;
                    while (true)
                    {
                        Console.Write("Nilai Huruf (A, AB, B, BC, C, D, E): ");
                        nilaiHuruf = Console.ReadLine()?.Trim().ToUpper() ?? "A";
                        if (new[] { "A", "AB", "B", "BC", "C", "D", "E" }.Contains(nilaiHuruf)) break;
                        ConsoleHelper.PrintError("Nilai tidak valid! Masukkan salah satu dari: A, AB, B, BC, C, D, E.");
                    }

                    mhs.DaftarNilai.Add(new MataKuliah
                    {
                        Kode = kode,
                        Nama = namaMk,
                        Sks = sks,
                        NilaiHuruf = nilaiHuruf
                    });

                    ConsoleHelper.PrintSuccess($"Mata kuliah '{namaMk}' berhasil ditambahkan. IPK baru: {mhs.Ipk:F2}");
                    ConsoleHelper.Pause();
                }
                else if (subPilihan == "2")
                {
                    if (!mhs.DaftarNilai.Any())
                    {
                        ConsoleHelper.PrintWarning("Tidak ada mata kuliah untuk dihapus.");
                        ConsoleHelper.Pause();
                        continue;
                    }

                    var noHapus = ConsoleHelper.ReadInt($"Pilih nomor mata kuliah yang ingin dihapus (1-{mhs.DaftarNilai.Count}): ", 1, mhs.DaftarNilai.Count);
                    var removed = mhs.DaftarNilai[noHapus - 1];
                    mhs.DaftarNilai.RemoveAt(noHapus - 1);

                    ConsoleHelper.PrintSuccess($"Mata kuliah '{removed.Nama}' berhasil dihapus. IPK baru: {mhs.Ipk:F2}");
                    ConsoleHelper.Pause();
                }
            }
        }

        private static void MenuUbahMahasiswa()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("UBAH DATA PROFIL MAHASISWA");

            var nrp = ConsoleHelper.ReadMandatoryString("Masukkan NRP Mahasiswa yang akan diubah: ");
            var mhs = _service.GetByNrp(nrp);

            if (mhs == null)
            {
                ConsoleHelper.PrintError($"Mahasiswa dengan NRP '{nrp}' tidak ditemukan.");
                ConsoleHelper.Pause();
                return;
            }

            Console.WriteLine($"Data Saat Ini: Nama: {mhs.Nama} | Kelas: {mhs.Kelas} | Angkatan: {mhs.Angkatan}");
            Console.WriteLine("Kosongkan dan tekan Enter jika tidak ingin mengubah field tertentu.");

            Console.Write($"Nama Baru [{mhs.Nama}]: ");
            var namaBaru = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(namaBaru)) namaBaru = mhs.Nama;

            Console.Write($"Kelas Baru [{mhs.Kelas}]: ");
            var kelasBaru = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(kelasBaru)) kelasBaru = mhs.Kelas;

            Console.Write($"Angkatan Baru [{mhs.Angkatan}]: ");
            var angkatanBaru = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(angkatanBaru)) angkatanBaru = mhs.Angkatan;

            if (_service.Update(nrp, namaBaru, kelasBaru, angkatanBaru, out var error))
            {
                ConsoleHelper.PrintSuccess("Data mahasiswa berhasil diperbarui!");
            }
            else
            {
                ConsoleHelper.PrintError(error);
            }

            ConsoleHelper.Pause();
        }

        private static void MenuHapusMahasiswa()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("HAPUS DATA MAHASISWA");

            var nrp = ConsoleHelper.ReadMandatoryString("Masukkan NRP Mahasiswa yang akan dihapus: ");
            var mhs = _service.GetByNrp(nrp);

            if (mhs == null)
            {
                ConsoleHelper.PrintError($"Mahasiswa dengan NRP '{nrp}' tidak ditemukan.");
                ConsoleHelper.Pause();
                return;
            }

            ConsoleHelper.PrintWarning($"Yakin ingin menghapus {mhs.Nama} ({mhs.Nrp})? Data tidak dapat dipulihkan.");
            Console.Write("Ketik 'YA' untuk mengonfirmasi: ");
            var konfirmasi = Console.ReadLine()?.Trim();

            if (konfirmasi?.Equals("YA", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (_service.Delete(nrp, out var error))
                {
                    ConsoleHelper.PrintSuccess($"Mahasiswa {mhs.Nama} berhasil dihapus.");
                }
                else
                {
                    ConsoleHelper.PrintError(error);
                }
            }
            else
            {
                Console.WriteLine("Penghapusan dibatalkan.");
            }

            ConsoleHelper.Pause();
        }

        private static void MenuStatistik()
        {
            ConsoleHelper.Clear();
            ConsoleHelper.PrintHeader("STATISTIK DAN RINGKASAN AKADEMIK KELAS");

            var (total, avgIpk, top) = _service.GetStatistik();

            Console.WriteLine($"• Total Mahasiswa Terdata : {total} Orang");
            Console.WriteLine($"• Rata-rata IPK Kelas     : {avgIpk:F2} / 4.00");
            if (top != null)
            {
                Console.WriteLine($"• Mahasiswa IPK Tertinggi : {top.Nama} ({top.Nrp}) dengan IPK {top.Ipk:F2} [{top.Predikat}]");
            }
            Console.WriteLine();
            Console.WriteLine("Distribusi Predikat Kelulusan:");
            var groups = _service.GetAll()
                .GroupBy(m => m.Predikat)
                .Select(g => new { Predikat = g.Key, Count = g.Count() });

            foreach (var g in groups)
            {
                Console.WriteLine($"  - {g.Predikat,-26} : {g.Count} Mahasiswa");
            }

            ConsoleHelper.Pause();
        }
    }
}
