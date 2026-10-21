using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemMahasiswa;

internal class Program
{
    private static readonly List<Mahasiswa> daftarMahasiswa = new()
    {
        new Mahasiswa { Nrp = "5025241265", Nama = "A. Wildan Kevin Assyauqi", Kelas = "PBKK-C", Tahun = "2024" }
    };

    public static void Main(string[] args)
    {
        while (true)
        {
            ClearScreen();
            Console.WriteLine("=== SISTEM DATA MAHASISWA ===");
            Console.WriteLine("1. Lihat data");
            Console.WriteLine("2. Tambah data");
            Console.WriteLine("3. Ubah data");
            Console.WriteLine("4. Hapus data");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih menu: ");

            var line = Console.ReadLine();
            if (line == null) break;
            var pilihan = line.Trim('\uFEFF', ' ', '\r', '\n', '\t');
            if (string.IsNullOrEmpty(pilihan)) continue;

            switch (pilihan)
            {
                case "1": LihatData(); break;
                case "2": TambahData(); break;
                case "3": UbahData(); break;
                case "4": HapusData(); break;
                case "5": return;
                default: Pesan("Pilihan tidak tersedia."); break;
            }
        }
    }

    private static void LihatData()
    {
        ClearScreen();
        Console.WriteLine("=== DAFTAR DATA MAHASISWA ===");
        if (daftarMahasiswa.Count == 0)
        {
            Console.WriteLine("Belum ada data mahasiswa.");
        }
        else
        {
            Console.WriteLine("No | NRP          | Nama                          | Kelas  | Angkatan");
            Console.WriteLine("---------------------------------------------------------------------");
            for (var i = 0; i < daftarMahasiswa.Count; i++)
            {
                var m = daftarMahasiswa[i];
                Console.WriteLine($"{i + 1,2} | {m.Nrp,-12} | {m.Nama,-29} | {m.Kelas,-6} | {m.Tahun,-8}");
            }
        }
        Jeda();
    }

    private static void TambahData()
    {
        ClearScreen();
        Console.WriteLine("=== TAMBAH DATA MAHASISWA ===");
        var data = BacaData();

        if (daftarMahasiswa.Any(m => m.Nrp.Equals(data.Nrp, StringComparison.OrdinalIgnoreCase)))
        {
            Pesan("Gagal: Mahasiswa dengan NRP tersebut sudah terdaftar!");
            return;
        }

        daftarMahasiswa.Add(data);
        Pesan("Data berhasil ditambahkan.");
    }

    private static void UbahData()
    {
        ClearScreen();
        Console.WriteLine("=== UBAH DATA MAHASISWA ===");
        var nrp = BacaWajib("Masukkan NRP yang ingin diubah: ");
        var data = daftarMahasiswa.FirstOrDefault(m => m.Nrp.Equals(nrp, StringComparison.OrdinalIgnoreCase));

        if (data == null)
        {
            Pesan("Data mahasiswa tidak ditemukan.");
            return;
        }

        Console.WriteLine($"\nData lama: {data.Nama} ({data.Nrp}) | {data.Kelas} | {data.Tahun}");
        data.Nama = BacaWajib("Nama Baru     : ");
        data.Kelas = BacaWajib("Kelas Baru    : ");
        data.Tahun = BacaWajib("Angkatan Baru : ");

        Pesan("Data berhasil diubah.");
    }

    private static void HapusData()
    {
        ClearScreen();
        Console.WriteLine("=== HAPUS DATA MAHASISWA ===");
        var nrp = BacaWajib("Masukkan NRP yang ingin dihapus: ");
        var data = daftarMahasiswa.FirstOrDefault(m => m.Nrp.Equals(nrp, StringComparison.OrdinalIgnoreCase));

        if (data == null)
        {
            Pesan("Data mahasiswa tidak ditemukan.");
            return;
        }

        daftarMahasiswa.Remove(data);
        Pesan("Data berhasil dihapus.");
    }

    private static Mahasiswa BacaData() => new()
    {
        Nrp = BacaWajib("NRP      : "),
        Nama = BacaWajib("Nama     : "),
        Kelas = BacaWajib("Kelas    : "),
        Tahun = BacaWajib("Angkatan : ")
    };

    private static string BacaWajib(string label)
    {
        while (true)
        {
            Console.Write(label);
            var line = Console.ReadLine();
            var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t') ?? "";
            if (!string.IsNullOrWhiteSpace(input)) return input;
            Console.WriteLine("Input wajib diisi.");
        }
    }

    private static void Pesan(string pesan)
    {
        Console.WriteLine($"\n{pesan}");
        Jeda();
    }

    private static void Jeda()
    {
        if (Console.IsInputRedirected) return;
        Console.WriteLine("\nTekan Enter untuk kembali ke menu...");
        Console.ReadLine();
    }

    private static void ClearScreen()
    {
        if (!Console.IsOutputRedirected) Console.Clear();
    }
}
