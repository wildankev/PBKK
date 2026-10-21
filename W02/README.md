# Laporan Praktikum PBKK (W02) - .NET Framework & Pemrograman C#

> **Mata Kuliah:** Pemrograman Berbasis Kerangka Kerja (PBKK) - Kelas C  
> **Pertemuan:** Minggu 2 (W02) / Pertemuan 2  
> **Topik:** .NET Framework Architecture & C# Console Application  
> **Dosen Pengampu:** Fajar Baskoro, S.Kom., M.T.  
> **Penyusun:** M. Wildan Kevin A.  
> **NRP:** 5025241265  
> **Departemen:** Teknik Informatika, Fakultas Teknologi Elektro dan Informatika Cerdas (FTEIC), Institut Teknologi Sepuluh Nopember (ITS)  
> **Tautan Repository:** [https://github.com/wildankev/PBKK](https://github.com/wildankev/PBKK)

---

## Daftar Isi
1. [Latar Belakang & Landasan Teori](#1-latar-belakang--landasan-teori)
   - [Pengenalan Microsoft .NET Framework](#11-pengenalan-microsoft-net-framework)
   - [Komponen Utama .NET Framework](#12-komponen-utama-net-framework)
   - [Evolusi: .NET Framework ke Modern .NET (.NET 8/9/10)](#13-evolusi-net-framework-ke-modern-net)
2. [Lingkungan Pengembangan & Prasyarat](#2-lingkungan-pengembangan--prasyarat)
3. [Latihan 1: Program "Hello World" & Pemeriksaan Lingkungan CLR](#3-latihan-1-program-hello-world--pemeriksaan-lingkungan-clr)
   - [Deskripsi Proyek](#31-deskripsi-proyek)
   - [Struktur Direktori](#32-struktur-direktori)
   - [Kode Sumber (Source Code)](#33-kode-sumber-source-code)
   - [Penjelasan Kode](#34-penjelasan-kode)
   - [Hasil Eksekusi Terminal](#35-hasil-eksekusi-terminal)
4. [Latihan 2: Sistem Manajemen Akademik & Kalkulator IPK Mahasiswa](#4-latihan-2-sistem-manajemen-akademik--kalkulator-ipk-mahasiswa)
   - [Deskripsi Proyek & Studi Kasus](#41-deskripsi-proyek--studi-kasus)
   - [Fitur Utama Aplikasi](#42-fitur-utama-aplikasi)
   - [Arsitektur & Struktur Proyek](#43-arsitektur--struktur-proyek)
   - [Kode Sumber (Source Code)](#44-kode-sumber-source-code)
     - [1. `SistemAkademik.csproj`](#1-sistemakademikcsproj)
     - [2. Model `MataKuliah.cs`](#2-model-matakuliahcs)
     - [3. Model `Mahasiswa.cs`](#3-model-mahasiswacs)
     - [4. Utility `ConsoleHelper.cs`](#4-utility-consolehelpercs)
     - [5. Service `AkademikService.cs`](#5-service-akademikservicecs)
     - [6. Entry Point `Program.cs`](#6-entry-point-programcs)
   - [Hasil Eksekusi & Dokumentasi Alur Program](#45-hasil-eksekusi--dokumentasi-alur-program)
5. [Kesimpulan](#5-kesimpulan)
6. [Referensi](#6-referensi)

---

## 1. Latar Belakang & Landasan Teori

### 1.1 Pengenalan Microsoft .NET Framework
**Microsoft .NET Framework** merupakan kerangka kerja pengembangan perangkat lunak (*software development framework*) komprehensif yang dirancang oleh Microsoft untuk memfasilitasi pembangunan dan eksekusi berbagai macam aplikasi, mulai dari aplikasi berbasis desktop (Windows Forms, WPF), aplikasi web (ASP.NET), hingga layanan jaringan dan sistem terdistribusi.

Berbeda dengan arsitektur kompilasi konvensional (misalnya C/C++ murni yang langsung menghasilkan *machine code* spesifik perangkat keras tertentu), program yang ditulis di atas platform .NET dikompilasi menjadi representasi kode perantara yang disebut **Common Intermediate Language (CIL)** atau **Microsoft Intermediate Language (MSIL)**. CIL ini kemudian dieksekusi di dalam lingkungan virtual terisolasi yang aman dan terkontrol.

### 1.2 Komponen Utama .NET Framework
Secara arsitektural, .NET Framework tersusun atas pilar-pilar penting:

```
+-------------------------------------------------------------+
|               Bahasa Pemrograman (C#, VB.NET, F#)            |
+-------------------------------------------------------------+
|        Common Language Specification (CLS) & CTS            |
+-------------------------------------------------------------+
|           .NET Framework Class Library (FCL / BCL)          |
|    - System, Collections, IO, Net, Linq, Threading, dll.   |
+-------------------------------------------------------------+
|               Common Language Runtime (CLR)                 |
|  - JIT (Just-In-Time) Compiler                              |
|  - Automatic Garbage Collection (GC)                        |
|  - Memory Management & Type Safety                          |
|  - Exception Handling & Thread Management                   |
+-------------------------------------------------------------+
|                 Sistem Operasi / Perangkat Keras            |
+-------------------------------------------------------------+
```

1. **Common Language Runtime (CLR):**
   - Jantung penggerak lingkungan eksekusi .NET.
   - Bertindak sebagai *application virtual machine* yang mengabstraksi CPU dan sistem operasi fisik.
   - Mengubah kode CIL menjadi instruksi mesin asli melalui kompilasi *Just-In-Time* (JIT).
   - Mengelola alokasi memori secara otomatis melalui **Garbage Collector (GC)**, mencegah kebocoran memori (*memory leak*) dan *dangling pointers*.
   - Menyediakan fitur *exception handling* terstruktur, keamanan berbasis izin (*Code Access Security*), serta manajemen *multithreading*.
2. **Common Language Infrastructure (CLI), CTS, dan CLS:**
   - **CLI:** Spesifikasi terbuka standar ECMA-335 yang mendeskripsikan format kode yang dapat dieksekusi dan runtime environment.
   - **Common Type System (CTS):** Standarisasi semua tipe data dan operasi yang berlaku lintas bahasa di lingkungan .NET.
   - **Common Language Specification (CLS):** Aturan dasar minimal yang menjamin interoperabilitas penuh antar-bahasa pemrograman (misal: kode C# dapat memanggil library yang ditulis dalam VB.NET atau F#).
3. **Framework Class Library (FCL):**
   - Kumpulan pustaka siap pakai (*pre-built reusable classes, interfaces, and value types*) yang sangat kaya untuk menangani operasi I/O, struktur data koleksi, manipulasi teks, enkripsi kriptografi, jaringan, hingga representasi data grafis.

### 1.3 Evolusi: .NET Framework ke Modern .NET
Dimulai dari rilis awal .NET Framework 1.0 (tahun 2002) hingga .NET Framework 4.8, ekosistem berkembang pesat. Microsoft kemudian melakukan perombakan arsitektur besar-besaran dengan merilis **.NET Core** yang bersifat *open-source*, modular, dan *cross-platform* (dapat berjalan di Windows, Linux, dan macOS), yang kemudian dikonsolidasikan menjadi **.NET 5, 6, 7, 8, dan 9**. 

Pada praktikum kali ini, implementasi menggunakan **.NET 8 (LTS)** dengan standar SDK modern, yang tetap memegang prinsip dasar runtime CLR, FCL, dan paradigma C#.

---

## 2. Lingkungan Pengembangan & Prasyarat

Berikut adalah rincian konfigurasi mesin dan perkakas pengembangan yang digunakan pada praktikum W02:

| Komponen | Spesifikasi |
|---|---|
| **Sistem Operasi** | Microsoft Windows 10/11 (64-bit Architecture) |
| **.NET SDK** | .NET 8.0 SDK (Version 8.0.426) |
| **Runtime Version** | .NET CoreCLR Runtime 8.0.32 |
| **Bahasa Pemrograman** | C# 12 / Modern C# |
| **Version Control System** | Git 2.4x / GitHub Repository |
| **Code Editor** | Visual Studio / Visual Studio Code / Antigravity IDE |

Verifikasi status instalasi SDK melalui terminal PowerShell:
```powershell
dotnet --version
dotnet --info
```

Output:
```text
8.0.426
.NET SDK:
 Version:           8.0.426
 OS Name:           Windows
 RID:               win-x64
 .NET SDKs installed: 8.0.426
```

---

## 3. Latihan 1: Program "Hello World" & Pemeriksaan Lingkungan CLR

### 3.1 Deskripsi Proyek
Latihan pertama bertujuan memverifikasi bahwa .NET SDK dan runtime CLR telah terkonfigurasi dengan sempurna. Program tidak hanya mencetak string salam `"Hello, World!"`, namun juga membaca dan mendemonstrasikan introspeksi terhadap metadata runtime sistem operasi dan CLR menggunakan library standar `System` dan `System.Runtime.InteropServices`.

### 3.2 Struktur Direktori
```text
W02/
└── 01-HelloWorld/
    ├── HelloWorld.csproj
    └── Program.cs
```

### 3.3 Kode Sumber (Source Code)

#### `HelloWorld.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>HelloWorld</RootNamespace>
  </PropertyGroup>

</Project>
```

#### `Program.cs`
```csharp
using System;
using System.Runtime.InteropServices;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=================================================");
            Console.WriteLine("        PBKK Pertemuan 2 - .NET Framework        ");
            Console.WriteLine("               Latihan 1: Hello World            ");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine("Hello, World! Selamat Datang di Ekosistem .NET!");
            Console.WriteLine();
            Console.WriteLine("--- Informasi Lingkungan Runtime .NET (CLR) ---");
            Console.WriteLine($"• .NET Version            : {Environment.Version}");
            Console.WriteLine($"• Framework Description   : {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"• OS Description          : {RuntimeInformation.OSDescription}");
            Console.WriteLine($"• OS Architecture         : {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"• Process Architecture    : {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"• Current Managed Thread  : {Environment.CurrentManagedThreadId}");
            Console.WriteLine($"• Machine Name            : {Environment.MachineName}");
            Console.WriteLine($"• User Domain / Name      : {Environment.UserDomainName}\\{Environment.UserName}");
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Program berhasil dieksekusi dengan baik.");
        }
    }
}
```

### 3.4 Penjelasan Kode
1. **`<OutputType>Exe</OutputType>`:** Menentukan jenis keluaran kompilasi berupa aplikasi biner yang dapat dieksekusi secara langsung.
2. **`RuntimeInformation.FrameworkDescription`:** Membaca nama serta versi distribusi framework .NET yang sedang menaungi eksekusi program.
3. **`Environment.CurrentManagedThreadId`:** Memperlihatkan bahwa CLR mengelola thread eksekusi terkelola (*managed thread*) secara mandiri terpisah dari ID thread OS native.

### 3.5 Hasil Eksekusi Terminal
Jalankan program melalui terminal:
```powershell
cd W02/01-HelloWorld
dotnet run
```

Tampilan Keluaran Program:
```text
=================================================
        PBKK Pertemuan 2 - .NET Framework        
               Latihan 1: Hello World            
=================================================

Hello, World! Selamat Datang di Ekosistem .NET!

--- Informasi Lingkungan Runtime .NET (CLR) ---
• .NET Version            : 8.0.32
• Framework Description   : .NET 8.0.32
• OS Description          : Microsoft Windows 10.0.26300
• OS Architecture         : X64
• Process Architecture    : X64
• Current Managed Thread  : 1
• Machine Name            : DESKTOP-VSVMC5P
• User Domain / Name      : DESKTOP-VSVMC5P\wilda

=================================================
Program berhasil dieksekusi dengan baik.
```

---

## 4. Latihan 2: Sistem Manajemen Akademik & Kalkulator IPK Mahasiswa

### 4.1 Deskripsi Proyek & Studi Kasus
Pada latihan utama pertemuan ke-2 ini, dibangun sebuah aplikasi konsol interaktif berbasis C# dan .NET dengan tema **Sistem Manajemen Akademik & Kalkulator IPK Mahasiswa**.

Aplikasi ini menggabungkan dua elemen latihan penting dari silabus materi PBKK Pertemuan 2:
1. **Sistem Pengelolaan Data Mahasiswa (CRUD):** Kemampuan membaca, menambah, mengubah, menghapus, serta mencari data mahasiswa dengan prinsip enkapsulasi Object-Oriented Programming (OOP) dan struktur data `List<T>` dalam memori CLR.
2. **Kalkulator Akademik Terpadu:** Modul kalkulasi nilai bobot SKS, penghitungan Indeks Prestasi Kumulatif (IPK), penetapan predikat kelulusan (Cumlaude / Sangat Memuaskan / Memuaskan), serta evaluasi otomatis batas beban SKS semester berikutnya berdasarkan peraturan akademik perguruan tinggi.

### 4.2 Fitur Utama Aplikasi
1. **Tampilkan Semua Mahasiswa & Prestasi:** Menampilkan tabel seluruh mahasiswa beserta total SKS yang telah ditempuh, IPK saat ini, dan predikat kelulusan.
2. **Tambah Mahasiswa Baru:** Input data mahasiswa (NRP, Nama, Kelas, Angkatan) dengan validasi keunikan NRP agar tidak terjadi duplikasi.
3. **Pencarian Data (Search Engine):** Menemukan mahasiswa berdasarkan kata kunci NRP, Nama Lengkap, atau Kode Kelas secara fleksibel menggunakan LINQ.
4. **Kalkulator IPK & Transkrip:** 
   - Memilih mahasiswa tertentu untuk melihat rincian mata kuliah yang diambil.
   - Menambahkan mata kuliah baru lengkap dengan SKS (1-6) dan nilai huruf (`A`, `AB`, `B`, `BC`, `C`, `D`, `E`).
   - Secara otomatis menghitung total bobot perkalian ($\sum (\text{SKS} \times \text{Bobot})$) dan membaginya dengan total SKS untuk mendapatkan IPK akurat.
   - Menampilkan batas rekomendasi pengambilan SKS untuk semester depan (misal: IPK $\ge$ 3.00 berhak mengambil hingga 24 SKS).
5. **Ubah Data Profil:** Mengubah nama, kelas, atau angkatan mahasiswa yang sudah terdaftar.
6. **Hapus Data Mahasiswa:** Menghapus data mahasiswa dari memori dengan konfirmasi keamanan.
7. **Statistik & Analisis Kelas:** Menghitung total mahasiswa, rata-rata IPK kelas, mahasiswa berprestasi dengan IPK tertinggi, dan distribusi predikat kelulusan.

### 4.3 Arsitektur & Struktur Proyek
Proyek dirancang dengan pola arsitektur bersih (*Clean Separation of Concerns*):

```text
W02/
└── 02-SistemAkademik/
    ├── SistemAkademik.csproj
    ├── Program.cs                     (UI Menu Controller & Loop Eksekusi)
    ├── Models/
    │   ├── MataKuliah.cs              (Entitas Model Mata Kuliah & Bobot Nilai)
    │   └── Mahasiswa.cs               (Entitas Model Mahasiswa & Kalkulator IPK)
    ├── Services/
    │   └── AkademikService.cs         (Business Logic CRUD & LINQ Repository)
    └── Utils/
        └── ConsoleHelper.cs           (Utility UI Console, Sanitasi Input & Format)
```

---

### 4.4 Kode Sumber (Source Code)

#### 1. `SistemAkademik.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>SistemAkademik</RootNamespace>
  </PropertyGroup>

</Project>
```

#### 2. Model `MataKuliah.cs`
File ini merepresentasikan unit mata kuliah dengan kalkulasi bobot otomatis:
```csharp
namespace SistemAkademik.Models
{
    public class MataKuliah
    {
        public string Kode { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public int Sks { get; set; } = 3;
        public string NilaiHuruf { get; set; } = "A";

        // Konversi Nilai Huruf ke Bobot Angka Skala 4.0
        public double Bobot => NilaiHuruf.ToUpper() switch
        {
            "A"  => 4.0,
            "AB" => 3.5,
            "B"  => 3.0,
            "BC" => 2.5,
            "C"  => 2.0,
            "D"  => 1.0,
            _    => 0.0
        };

        // Total nilai mutu mata kuliah
        public double TotalBobotSks => Bobot * Sks;
    }
}
```

#### 3. Model `Mahasiswa.cs`
File ini menampung data profil mahasiswa, daftar riwayat mata kuliah, serta *computed properties* untuk kalkulasi IPK, predikat, dan hak SKS:
```csharp
using System.Collections.Generic;
using System.Linq;

namespace SistemAkademik.Models
{
    public class Mahasiswa
    {
        public string Nrp { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Kelas { get; set; } = "C";
        public string Angkatan { get; set; } = "2024";
        public List<MataKuliah> DaftarNilai { get; set; } = new();

        public int TotalSks => DaftarNilai.Sum(m => m.Sks);

        // Rumus Kalkulasi IPK: Total (Bobot x SKS) / Total SKS
        public double Ipk
        {
            get
            {
                if (DaftarNilai == null || !DaftarNilai.Any()) return 0.0;
                var totalSks = TotalSks;
                if (totalSks == 0) return 0.0;
                return DaftarNilai.Sum(m => m.TotalBobotSks) / totalSks;
            }
        }

        public string Predikat => Ipk switch
        {
            >= 3.51 => "Dengan Pujian (Cumlaude)",
            >= 3.00 => "Sangat Memuaskan",
            >= 2.50 => "Memuaskan",
            >= 2.00 => "Cukup",
            _ => "Perlu Peningkatan"
        };

        public int MaxSksBerikutnya => Ipk switch
        {
            >= 3.00 => 24,
            >= 2.50 => 22,
            >= 2.00 => 20,
            _ => 18
        };
    }
}
```

#### 4. Utility `ConsoleHelper.cs`
Menyediakan pembungkus pembacaan input aman, sanitasi karakter UTF-8 BOM, serta pewarnaan konsol:
```csharp
using System;

namespace SistemAkademik.Utils
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine($"   {title}");
            Console.WriteLine("================================================================================");
            Console.ResetColor();
        }

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[✓] {message}");
            Console.ResetColor();
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[!] {message}");
            Console.ResetColor();
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[✗] {message}");
            Console.ResetColor();
        }

        public static string ReadMandatoryString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var line = Console.ReadLine();
                var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t') ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                PrintError("Input tidak boleh kosong. Silakan ulangi.");
            }
        }

        public static string ReadOptionalString(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} (default: {defaultValue}): ");
            var line = Console.ReadLine();
            var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t');
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var line = Console.ReadLine();
                var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t');
                if (int.TryParse(input, out var result) && result >= min && result <= max)
                {
                    return result;
                }
                PrintError($"Input harus berupa bilangan bulat antara {min} dan {max}.");
            }
        }

        public static void Pause()
        {
            if (Console.IsInputRedirected) return;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Tekan [Enter] untuk melanjutkan...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void Clear()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
    }
}
```

#### 5. Service `AkademikService.cs`
Mengelola *in-memory collection* `List<Mahasiswa>` dengan data seeder awal dan query LINQ:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using SistemAkademik.Models;

namespace SistemAkademik.Services
{
    public class AkademikService
    {
        private readonly List<Mahasiswa> _daftarMahasiswa = new();

        public AkademikService()
        {
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            var m1 = new Mahasiswa
            {
                Nrp = "5025241265",
                Nama = "M. Wildan Kevin A.",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4402", Nama = "Kecerdasan Komputasional", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "AB" },
                    new() { Kode = "IF4404", Nama = "Rekayasa Perangkat Lunak", Sks = 3, NilaiHuruf = "A" }
                }
            };

            var m2 = new Mahasiswa
            {
                Nrp = "5025241033",
                Nama = "Ferdian Ardra Hafizhan",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4402", Nama = "Kecerdasan Komputasional", Sks = 3, NilaiHuruf = "AB" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "B" }
                }
            };

            var m3 = new Mahasiswa
            {
                Nrp = "5025241150",
                Nama = "Nabila Shafa Rahayu",
                Kelas = "PBKK-C",
                Angkatan = "2024",
                DaftarNilai = new List<MataKuliah>
                {
                    new() { Kode = "IF4401", Nama = "Pemrograman Berbasis Kerangka Kerja", Sks = 3, NilaiHuruf = "A" },
                    new() { Kode = "IF4403", Nama = "Jaringan Komputer", Sks = 4, NilaiHuruf = "A" }
                }
            };

            _daftarMahasiswa.AddRange(new[] { m1, m2, m3 });
        }

        public IReadOnlyList<Mahasiswa> GetAll() => _daftarMahasiswa.AsReadOnly();

        public Mahasiswa? GetByNrp(string nrp)
        {
            return _daftarMahasiswa.FirstOrDefault(m => m.Nrp.Equals(nrp.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Mahasiswa> Search(string keyword)
        {
            var q = keyword.Trim().ToLowerInvariant();
            return _daftarMahasiswa.Where(m =>
                m.Nrp.ToLowerInvariant().Contains(q) ||
                m.Nama.ToLowerInvariant().Contains(q) ||
                m.Kelas.ToLowerInvariant().Contains(q)
            ).ToList();
        }

        public bool Add(Mahasiswa mahasiswa, out string errorMessage)
        {
            if (GetByNrp(mahasiswa.Nrp) != null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{mahasiswa.Nrp}' sudah terdaftar!";
                return false;
            }

            _daftarMahasiswa.Add(mahasiswa);
            errorMessage = string.Empty;
            return true;
        }

        public bool Update(string nrpLama, string namaBaru, string kelasBaru, string angkatanBaru, out string errorMessage)
        {
            var mhs = GetByNrp(nrpLama);
            if (mhs == null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{nrpLama}' tidak ditemukan!";
                return false;
            }

            mhs.Nama = namaBaru;
            mhs.Kelas = kelasBaru;
            mhs.Angkatan = angkatanBaru;
            errorMessage = string.Empty;
            return true;
        }

        public bool Delete(string nrp, out string errorMessage)
        {
            var mhs = GetByNrp(nrp);
            if (mhs == null)
            {
                errorMessage = $"Mahasiswa dengan NRP '{nrp}' tidak ditemukan!";
                return false;
            }

            _daftarMahasiswa.Remove(mhs);
            errorMessage = string.Empty;
            return true;
        }

        public (int TotalMahasiswa, double RataRataIpk, Mahasiswa? MahasiswaTertinggi) GetStatistik()
        {
            if (!_daftarMahasiswa.Any()) return (0, 0.0, null);

            var total = _daftarMahasiswa.Count;
            var avgIpk = _daftarMahasiswa.Average(m => m.Ipk);
            var top = _daftarMahasiswa.OrderByDescending(m => m.Ipk).FirstOrDefault();

            return (total, avgIpk, top);
        }
    }
}
```

#### 6. Entry Point `Program.cs`
File pengendali navigasi menu interaktif:
```csharp
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
                    case "1": MenuTampilkanSemua(); break;
                    case "2": MenuTambahMahasiswa(); break;
                    case "3": MenuCariMahasiswa(); break;
                    case "4": MenuKalkulatorIpk(); break;
                    case "5": MenuUbahMahasiswa(); break;
                    case "6": MenuHapusMahasiswa(); break;
                    case "7": MenuStatistik(); break;
                    case "0":
                        Console.WriteLine("\nTerima kasih telah menggunakan sistem ini. Sampai jumpa!");
                        return;
                    default:
                        ConsoleHelper.PrintError($"Pilihan menu '{pilihan}' tidak valid. Silakan pilih 0-7.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }

        // Submenu implementations (MenuTampilkanSemua, MenuTambahMahasiswa, MenuCariMahasiswa,
        // MenuKalkulatorIpk, MenuUbahMahasiswa, MenuHapusMahasiswa, MenuStatistik)
        // [Implementasi lengkap berada pada file Program.cs]
    }
}
```

---

### 4.5 Hasil Eksekusi & Dokumentasi Alur Program

#### 1. Menu Utama Aplikasi
```text
================================================================================
   SISTEM MANAJEMEN AKADEMIK & KALKULATOR IPK (.NET 8)
================================================================================
Praktikum PBKK W02 - .NET Framework & C# Console Application
Pengembang: M. Wildan Kevin A. (5025241265) - Kelas PBKK C
--------------------------------------------------------------------------------
 [1] Tampilkan Semua Mahasiswa & Transkrip
 [2] Tambah Mahasiswa Baru
 [3] Cari Mahasiswa (NRP / Nama / Kelas)
 [4] Kalkulator IPK & Kelola Nilai Mata Kuliah
 [5] Ubah Data Profil Mahasiswa
 [6] Hapus Data Mahasiswa
 [7] Ringkasan Statistik Kelas
 [0] Keluar Aplikasi
--------------------------------------------------------------------------------
Pilih menu (0-7):
```

#### 2. Tampilkan Semua Mahasiswa (Menu 1)
```text
================================================================================
   DAFTAR SELURUH MAHASISWA & PRESTASI AKADEMIK
================================================================================
+----+--------------+----------------------------+--------+------+-----+------+--------------------------+
| No | NRP          | Nama Mahasiswa             | Kelas  | Angk | SKS | IPK  | Predikat Kelulusan       |
+----+--------------+----------------------------+--------+------+-----+------+--------------------------+
|  1 | 5025241265   | M. Wildan Kevin A.         | PBKK-C | 2024 |  13 | 3.85 | Dengan Pujian (Cumlaude) |
|  2 | 5025241033   | Ferdian Ardra Hafizhan     | PBKK-C | 2024 |  10 | 3.45 | Sangat Memuaskan         |
|  3 | 5025241150   | Nabila Shafa Rahayu        | PBKK-C | 2024 |   7 | 4.00 | Dengan Pujian (Cumlaude) |
+----+--------------+----------------------------+--------+------+-----+------+--------------------------+
```

#### 3. Detail Transkrip Nilai & Kalkulator IPK (Menu 4)
Memilih mahasiswa dengan NRP `5025241265`:
```text
================================================================================
   TRANSKRIP & KALKULATOR IPK: M. Wildan Kevin A. (5025241265)
================================================================================
Kelas       : PBKK-C
Angkatan    : 2024
Total SKS   : 13 SKS
IPK Saat Ini: 3.85 / 4.00
Predikat    : Dengan Pujian (Cumlaude)
Batas Beban : Maksimal 24 SKS untuk semester depan
--------------------------------------------------------------------------------
Daftar Mata Kuliah yang Diambil:
+----+--------+------------------------------------+-----+-------+-------+------------+
| No | Kode   | Mata Kuliah                        | SKS | Nilai | Bobot | SKS x Bobot|
+----+--------+------------------------------------+-----+-------+-------+------------+
|  1 | IF4401 | Pemrograman Berbasis Kerangka K... |   3 | A     |   4.0 |       12.0 |
|  2 | IF4402 | Kecerdasan Komputasional           |   3 | A     |   4.0 |       12.0 |
|  3 | IF4403 | Jaringan Komputer                  |   4 | AB    |   3.5 |       14.0 |
|  4 | IF4404 | Rekayasa Perangkat Lunak           |   3 | A     |   4.0 |       12.0 |
+----+--------+------------------------------------+-----+-------+-------+------------+

[1] Tambah Mata Kuliah & Hitung Ulang IPK
[2] Hapus Mata Kuliah
[0] Kembali ke Menu Utama
Pilihan (0-2):
```

#### 4. Statistik dan Ringkasan Kelas (Menu 7)
```text
================================================================================
   STATISTIK DAN RINGKASAN AKADEMIK KELAS
================================================================================
• Total Mahasiswa Terdata : 3 Orang
• Rata-rata IPK Kelas     : 3.77 / 4.00
• Mahasiswa IPK Tertinggi : Nabila Shafa Rahayu (5025241150) dengan IPK 4.00 [Dengan Pujian (Cumlaude)]

Distribusi Predikat Kelulusan:
  - Dengan Pujian (Cumlaude)   : 2 Mahasiswa
  - Sangat Memuaskan           : 1 Mahasiswa
```

---

## 5. Kesimpulan
1. **Arsitektur CLR & FCL:** Ekosistem .NET menawarkan pondasi runtime yang sangat solid melalui CLR, di mana pengelolaan alokasi memori, garbage collection, interoperabilitas tipe data (CTS), dan type safety dilakukan secara otomatis di balik layar.
2. **Efisiensi C# Modern:** Pemrograman C# pada .NET 8 memungkinkan penulisan kode yang ringkas, aman (*null-safety*), ekspresif (dengan *pattern matching switch expressions*), dan berorientasi objek murni.
3. **Kekuatan Aplikasi Desktop & Console:** Aplikasi konsol .NET sangat andal untuk pemrosesan logika bisnis yang intensif, kalkulator analitik terstruktur, dan manipulasi data in-memory dengan waktu booting yang instan dan jejak memori yang efisien.

---

## 6. Referensi
1. Baskoro, Fajar. (2021). *.NET Framework*. Diakses dari [http://fajarbaskoro.blogspot.com/2021/03/net-framework.html](http://fajarbaskoro.blogspot.com/2021/03/net-framework.html).
2. Baskoro, Fajar. (2022). *PBKK: Latihan 1 Membuat Aplikasi Desktop*. Diakses dari [http://fajarbaskoro.blogspot.com/2022/02/pbkk-latihan-1-membuat-aplikasi-desktop.html](http://fajarbaskoro.blogspot.com/2022/02/pbkk-latihan-1-membuat-aplikasi-desktop.html).
3. Microsoft Learn. *.NET Architecture and CLR Overview*. Diakses dari [https://learn.microsoft.com/en-us/dotnet/standard/clr](https://learn.microsoft.com/en-us/dotnet/standard/clr).
4. Microsoft Learn. *C# Documentation & Language Reference*. Diakses dari [https://learn.microsoft.com/en-us/dotnet/csharp/](https://learn.microsoft.com/en-us/dotnet/csharp/).
