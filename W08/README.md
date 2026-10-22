# Evaluasi Tengah Semester (ETS) / Pertemuan 8 - Tugas Mandiri: Analisis dan Infografis TutorMate

Dokumentasi tugas evaluasi tengah semester mandiri mata kuliah **Pemrograman Berbasis Kerangka Kerja (PBKK) Kelas C - 2026**, Departemen Teknik Informatika, Fakultas Teknologi Elektro dan Informatika Cerdas, Institut Teknologi Sepuluh Nopember Surabaya.

---

## Identitas Pengerjaan

- **Nama Mahasiswa** : A. Wildan Kevin Assyauqi
- **NRP**            : 5025241265
- **Mata Kuliah**    : Pemrograman Berbasis Kerangka Kerja - C
- **Tugas**    : Evaluasi Tengah Semester
- **Proyek Aplikasi**: TutorMate - Platform Manajemen Tutor Akademik Sebaya (Basis Kolaborasi Kelompok 8)
- **Anggota Kelompok Proyek (W07)**:
  1. Muhammad Hilman Azhar (NRP: 5025241264)
  2. A. Wildan Kevin Assyauqi (NRP: 5025241265)
  3. Al Jad Kaukabudduri Hardianto (NRP: 5025241248)

---

## Deskripsi

Laporan evaluasi tengah semester ini menyajikan kajian akademis mandiri yang mencakup tiga pilar utama:
1. **Analisis Komparasi Arsitektur:** Perbandingan mendalam karakteristik aplikasi desktop (WPF / Rich UI) terhadap ekosistem web dan mobile, serta tinjauan kritis mengapa platform desktop tetap esensial dalam lanskap komputasi modern.
2. **Problem Statement & Fitur Solusi TutorMate:** Pemetaan permasalahan nyata mahasiswa seputar bimbingan akademik sebaya di lingkungan kampus ITS, latar belakang, manfaat, serta definisi spesifikasi fitur solusi dan batasan ruang lingkup aplikasi.
3. **Poster Infografis 1 Halaman:** Desain representasi visual ringkas satu halaman yang merangkum arsitektur, nilai produk, alur interaksi pengguna, dan teknologi aplikasi TutorMate.

---

## Tautan Dokumen & Repositori

| No | Komponen | Keterangan / Tautan |
| :-: | :--- | :--- |
| 1 | **Product Requirements Document (PRD)** | [Google Docs - PRD TutorMate Kelompok 8](https://docs.google.com/document/d/1uZbdgMPmGHO2TZzvQ9n4VjdaaDinSiKx4SfpOjJBlLM/edit?tab=t.0) |
| 2 | **Kode Sumber Aplikasi (W07)** | [GitHub - PBKK/W07](https://github.com/wildankev/PBKK/tree/main/W07) |
| 3 | **Unduh Aplikasi Siap Pakai (.exe)** | [GitHub - TutorMate.exe (Release v1.0)](https://github.com/wildankev/PBKK/blob/main/W07/release/TutorMate.exe) |
| 4 | **Video Dokumentasi & Demo** | [YouTube - Playlist Demo & Dokumentasi TutorMate](https://www.youtube.com/playlist?list=PLXAd8ywX2Pf4) |

---

## Struktur Direktori

```text
W08/
├── assets/
│   └── poster.png
└── README.md
```

---

## Soal 1: Perbedaan Aplikasi Desktop dengan Aplikasi Lain & Alasan Mengapa Masih Dibutuhkan

### A. Perbandingan Karakteristik (Desktop vs Web vs Mobile)

Aplikasi perangkat lunak modern terbagi ke dalam tiga ekosistem utama: **Aplikasi Desktop**, **Aplikasi Web**, dan **Aplikasi Mobile**. Masing-masing platform memiliki model arsitektur, lingkungan eksekusi, serta trade-off performa yang berbeda:

| Parameter Komparasi | Aplikasi Desktop (WPF / WinForms / Native) | Aplikasi Web (SPA / SSR / Browser) | Aplikasi Mobile (Android / iOS) |
| :--- | :--- | :--- | :--- |
| **Lingkungan Eksekusi** | Berjalan langsung di atas sistem operasi lokal (native execution / managed CLR runtime). | Berjalan di dalam lingkungan terisolasi (*sandbox*) peramban web (browser engine seperti V8 / Blink). | Berjalan di atas OS mobile (Android ART / iOS Darwin) dengan batasan siklus hidup baterai dan memori. |
| **Akses Hardware & OS** | Akses penuh dan langsung (*direct access*) ke CPU multi-core, GPU hardware acceleration (DirectX), memory pointer, filesystem lokal, dan port periferal (COM, USB, scanner). | Terbatas oleh API standar web (*security sandbox*), tidak memiliki akses bebas ke sistem berkas atau hardware mesin klien. | Dibatasi oleh sistem perizinan runtime ketat (*runtime permission*), sandboxed storage, serta manajemen daya agresif. |
| **Performa & Latensi** | Performa komputasi maksimal dengan latensi ultra-rendah (*near-zero UI latency*). Mampu merender ribuan baris data (*DataGrid*) tanpa penurunan frame-rate. | Rentan terhadap latensi jaringan (*network round-trip latency*), beban parsing DOM/CSS, dan batasan eksekusi single-threaded JavaScript. | Cepat untuk task sehari-hari, namun terbatas oleh performa thermal throttling chip mobile dan kapasitas memori terbatas. |
| **Ketergantungan Jaringan** | Bersifat *Offline-First*. Mampu beroperasi penuh 100% tanpa sambungan internet menggunakan basis data lokal (*LocalDB / SQLite / on-premise SQL Server*). | Sangat bergantung pada konektivitas jaringan stabil; fungsi aplikasi terhenti jika sambungan internet/server pusat down (meskipun PWA menawarkan caching terbatas). | Dapat berjalan offline, namun umumnya bergantung pada sinkronisasi berkala dengan cloud backend via REST API. |
| **Keamanan & Kedaulatan Data** | Kedaulatan data tinggi (*Data Sovereignty*). Data sensitif dapat disimpan sepenuhnya di jaringan lokal / penyimpanan internal pengguna tanpa eksposur ke cloud publik. | Data tersimpan di server pihak ketiga atau cloud provider, menuntut kepatuhan keamanan data transfer (HTTPS, CORS, TLS). | Data tersimpan di perangkat mobile atau cloud; rentan terhadap kehilangan fisik perangkat. |
| **Ergonomi UI & Produktivitas** | Dioptimalkan untuk produktivitas kerja intensif: multi-window, multi-monitor, *keyboard shortcuts* kompleks, dialog modal, dan visualisasi berdensitas tinggi (*high data density*). | Dioptimalkan untuk konsumsi informasi cepat, scrolling vertikal, dan layout responsif lintas ukuran layar. | Dioptimalkan untuk navigasi sentuhan (*touch gestures*), layar vertikal tunggal (*one-hand usage*), dan sesi interaksi singkat. |

---

### B. Alasan Mengapa Aplikasi Desktop Masih Sangat Dibutuhkan

Meskipun aplikasi web dan mobile berkembang pesat, aplikasi desktop tetap memegang peranan krusial yang tidak dapat digantikan sepenuhnya di berbagai domain industri dan operasional:

1. **Efisiensi Komputasi & Pemrosesan Data Berat (Heavy-Duty Computing):**  
   Aplikasi desktop dikompilasi secara native atau semi-native (seperti .NET JIT), memungkinkan pemanfaatan multi-threading sejati, SIMD vectorization, dan akselerasi grafis GPU (DirectX pada WPF). Ini sangat vital untuk aplikasi CAD/CAM, editing video, simulasi sains, dan sistem pengolahan tabel data berukuran ribuan hingga jutaan baris tanpa latensi.

2. **Kemandirian Operasional & Keandalan Misi Kritis (Mission-Critical Zero Dependency):**  
   Di lingkungan fasilitas medis, pabrik manufaktur, sistem kasir (Point of Sale), laboratorium, dan administrasi kampus, ketergantungan pada koneksi internet adalah risiko operasional. Aplikasi desktop dapat terus berjalan andal selama listrik menyala tanpa terpengaruh gangguan ISP atau downtime server cloud pihak ketiga.

3. **Keamanan, Kerahasiaan, dan Kedaulatan Data (On-Premise Data Governance):**  
   Banyak institusi perbankan, militer, dan universitas menerapkan regulasi ketat yang melarang data rahasia (seperti rekam medis atau berkas nilai akademik) diunggah ke internet publik. Aplikasi desktop memungkinkan arsitektur basis data *on-premise* yang terisolasi dalam *Local Area Network* (LAN).

4. **Integrasi Perangkat Keras dan Periferal Khusus:**  
   Aplikasi desktop dapat dengan mudah berkomunikasi langsung dengan perangkat eksternal melalui port serial, driver perangkat keras, scanner barcode industri, smart card reader, atau modul mikrokontroler.

5. **Pengalaman Produktivitas Kerja Intensif (Power User Experience):**  
   Pengguna profesional (seperti staf administrasi akademik, akuntan, atau analis data) membutuhkan navigasi cepat berbasis keyboard (*hotkeys*), kemampuan membuka banyak jendela kerja secara bersamaan (*multi-window workspace*), dan tata letak formulir terpadu yang padat informasi (*high-density data entry*).

---

## Soal 2: Problem yang Diselesaikan dan Definisi Fitur Solusi pada TutorMate

### A. Latar Belakang & Problem Statement (Permasalahan Nyata)

Dalam kehidupan perkuliahan di Departemen Teknik Informatika ITS, mahasiswa menghadapi kurikulum dengan beban komputasi dan logika yang menantang (seperti mata kuliah PBKK, Algoritma Pemrograman, dan Struktur Data). Berdasarkan studi lapangan dan penyusunan PRD pada W07, ditemukan beberapa permasalahan utama:

1. **Fragmentasi Pencarian Tutor di Grup Chat Informal:**  
   Pencarian bantuan belajar antar mahasiswa selama ini berlangsung sporadis melalui grup WhatsApp angkatan atau pesan personal. Pesan permintaan tolong sering kali tertimbun ribuan obrolan lain, sehingga mahasiswa yang membutuhkan bantuan tidak kunjung mendapatkan mentor.

2. **Asimetri Informasi dan Keraguan Kualifikasi Mentor:**  
   Calon peserta tutor sering ragu terhadap kompetensi mahasiswa yang menawarkan bantuan belajar karena tidak ada transparansi riwayat nilai mata kuliah maupun topik spesifik yang benar-benar dikuasai oleh calon tutor tersebut.

3. **Koordinasi Waktu yang Tidak Efisien & Tabrakan Jadwal Kuliah:**  
   Proses tawar-menawar jadwal bimbingan belajar melalui pesan singkat memakan waktu lama, rawan salah paham, dan kerap berujung pada pembatalan sepihak karena jadwal kuliah yang saling bentrok.

4. **Ketiadaan Katalog Mata Kuliah yang Terstandarisasi:**  
   Tidak tersedianya wadah direktori resmi yang mengelompokkan mata kuliah apa saja yang sedang dibuka untuk bimbingan, sehingga kebutuhan belajar mahasiswa pada mata kuliah krusial sering tidak terfasilitasi.

---

### B. Manfaat Aplikasi TutorMate

Aplikasi **TutorMate** dirancang untuk menghadirkan ekosistem pembelajaran sebaya (*peer-assisted learning*) yang terstruktur:

- **Bagi Mahasiswa Pencari Bimbingan (Peserta / Mentee):**
  - Menemukan mentor yang tepat secara cepat melalui katalog terpusat.
  - Mendapatkan kepastian kualifikasi mentor melalui transparansi nilai kelulusan mata kuliah dan topik keahlian.
  - Menghemat waktu koordinasi jadwal bimbingan belajar.

- **Bagi Mahasiswa Pengajar (Mentor / Tutor):**
  - Memperoleh wadah resmi untuk mengasah kemampuan komunikasi dan penguasaan materi akademik.
  - Membangun portofolio pengajaran dan reputasi akademik di lingkungan mahasiswa.
  - Fleksibilitas menentukan jadwal luang bimbingan sesuai preferensi pribadi.

- **Bagi Pengelola Akademik & Civitas Kampus:**
  - Mendorong terciptanya budaya belajar kolaboratif antar mahasiswa (*peer learning culture*).
  - Membantu menekan angka ketidaklulusan pada mata kuliah berbobot komputasi tinggi.
  - Menyediakan basis data riwayat kegiatan bimbingan akademik yang tertata rapi.

---

### C. Pemetaan Masalah ke Fitur Solusi

```text
+------------------------------------------+        +-----------------------------------------------+
|           PERMASALAHAN NYATA             | -----> |             FITUR SOLUSI TUTORMATE            |
+------------------------------------------+        +-----------------------------------------------+
| 1. Pencarian tutor terpencar di chat WA  | -----> | FR-01: Katalog Terpusat & Direktori Tutor     |
| 2. Ragu akan kompetensi/nilai tutor      | -----> | FR-02: Transparansi Nilai & Topik Ahli        |
| 3. Jadwal bimbingan bentrok/tidak pasti  | -----> | FR-03: Pencatatan Preferensi Hari & Jadwal    |
| 4. Hak akses tidak teratur antar user    | -----> | FR-04: Autentikasi Role-Based (Peserta/Mentor)|
| 5. Sulit mengusulkan mata kuliah baru    | -----> | FR-05: Modul Penambahan Katalog Mata Kuliah   |
+------------------------------------------+        +-----------------------------------------------+
```

---

### D. Definisi Fitur Solusi (Ruang Lingkup Rilis / In-Scope)

Berdasarkan implementasi kode pada folder `W07`, fitur solusi yang disediakan mencakup:

1. **Sistem Autentikasi & Kontrol Akses Berbasis Peran (FR-01):**  
   - *Solusi untuk:* Memisahkan alur kerja pengguna berdasarkan peran (*Peserta* vs *Mentor*).
   - *Implementasi:* Window Login (`LoginWindow.xaml`) dan Registrasi (`RegisterWindow.xaml`) dengan validasi keunikan NIM, enkripsi kata sandi, dan penyimpanan di tabel `dbo.Users`.

2. **Katalog Mata Kuliah Terstruktur (FR-02):**  
   - *Solusi untuk:* Standardisasi daftar mata kuliah bimbingan agar mahasiswa tidak bingung mencari mata kuliah yang dibuka.
   - *Implementasi:* DataGrid katalog interaktif pada `MainWindow.xaml` yang membaca data kode MK, nama mata kuliah, dan deskripsi dari tabel `dbo.Subjects`.

3. **Formulir Pengajuan & Registrasi Tutor Terbuka (FR-03):**  
   - *Solusi untuk:* Menyelesaikan asimetri informasi dan memverifikasi kualifikasi calon tutor.
   - *Implementasi:* Formulir pendaftaran tutor dengan input nilai huruf kelulusan mata kuliah (misal: Nilai `A`), topik spesifik yang dikuasai (misal: *WPF, MVVM, dan C# .NET*), serta pilihan hari bimbingan ke tabel `dbo.TutorApplications`.

4. **Direktori Terbuka Tutor Aktif (FR-04):**  
   - *Solusi untuk:* Memudahkan mahasiswa melihat siapa saja tutor yang tersedia beserta jadwal dan topik keahliannya.
   - *Implementasi:* Tampilan DataGrid utama yang menggabungkan relasi SQL antara `Users`, `Subjects`, dan `TutorApplications` secara instan dan bebas latensi.

5. **Modul Penambahan Mata Kuliah Baru oleh Mentor (FR-05):**  
   - *Solusi untuk:* Memastikan katalog bimbingan adaptif terhadap kebutuhan kurikulum baru.
   - *Implementasi:* Formulir khusus pengguna berstatus *Mentor* untuk mendaftarkan mata kuliah baru langsung ke basis data SQL Server.

---

### E. Batasan Aplikasi (Di Luar Ruang Lingkup / Out-of-Scope)

Untuk menjaga fokus implementasi desktop Rich UI pada fase awal ini, batasan sistem ditetapkan sebagai berikut:
- **Transaksi Pembayaran Otomatis:** Tidak mencakup integrasi *payment gateway* daring (layanan tutor bersifat sukarela atau diselesaikan langsung antarmahasiswa secara tunai).
- **Aplikasi Web / Mobile Publik:** Aplikasi difokuskan secara eksklusif sebagai aplikasi desktop Windows dengan WPF dan .NET 8.
- **Panggilan Video Terintegrasi:** Sesi bimbingan tatap muka diselenggarakan secara langsung di ruang belajar kampus atau platform meeting eksternal.
- **Cakupan Data Wilayah:** Dikhususkan untuk lingkup mahasiswa Departemen Teknik Informatika ITS.

---

## Soal 3: Poster Infografis Aplikasi TutorMate (1 Halaman)

Berikut adalah visualisasi poster infografis satu halaman untuk aplikasi TutorMate:

![Poster Infografis TutorMate](assets/poster.png)

Terimakasih.

---

## Dokumentasi Tampilan Antarmuka Aplikasi TutorMate

Sebagai referensi implementasi dari aplikasi yang telah dibangun pada modul `W07`, berikut adalah ringkasan visual antarmuka sistem:

1. **Modul Autentikasi Pengguna:**
   - Antarmuka **Login** (`LoginWindow.xaml`) untuk verifikasi identitas NIM dan kata sandi.
   - Antarmuka **Registrasi Akun Baru** (`RegisterWindow.xaml`) dengan penentuan peran awal (*Peserta* atau *Mentor*).

2. **Modul Dashboard Utama & Direktori Tutor:**
   - Tampilan antarmuka utama (`MainWindow.xaml`) yang dilengkapi navigasi sidebar modern, ringkasan kartu data, serta tabel *DataGrid* interaktif untuk penjelajahan tutor aktif.

3. **Modul Katalog Mata Kuliah & Pengajuan Tutor:**
   - Halaman katalog daftar mata kuliah aktif lengkap dengan kode mata kuliah resmi.
   - Formulir pendaftaran tutor baru dengan pencatatan nilai huruf, topik keahlian, dan jadwal luang.
   - Formulir penambahan mata kuliah baru khusus bagi akun berstatus *Mentor*.
