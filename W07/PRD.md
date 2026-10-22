PRODUCT REQUIREMENTS DOCUMENT
PEMROGRAMAN BERBASIS KERANGKA KERJA (C)
TutorMate - Aplikasi Manajemen Tutor Akademik

Disusun oleh:
Kelompok 8
5025241264 - Muhammad Hilman Azhar
5025241265 - A. Wildan Kevin Assyauqi
5025241248 - Al Jad Kaukabudduri Hardianto
Program Studi Teknik Informatika
Fakultas Teknologi Elektro dan Informatika Cerdas
Institut Teknologi Sepuluh Nopember
Surabaya
2026
DAFTAR ISI
1. Ringkasan Eksekutif        1
a. Informasi Dokumen        1
b. Tujuan Produk        1
c. Pernyataan Produk        2
2. Latar Belakang, Permasalahan, dan Manfaat        2
a. Latar Belakang        2
b. Permasalahan        2
c. Manfaat        2
3. Sasaran, Indikator, dan Batasan        3
4. Pengguna dan Hak Akses        3
a. Peran Pengguna        3
b. Matriks Hak Akses        4
5. Ruang Lingkup Produk        5
a. In Scope - Rilis Awal        5
b. Out of Scope - Rilis Berikutnya        6
6. Struktur Navigasi dan Halaman        6
a. Menu Sidebar per Peran        6
b. Daftar Halaman        6
7. Kebutuhan Fungsional        7
a. Daftar Kebutuhan Fungsional        7
b. Aturan Bisnis Utama        9
c. Siklus Status Pengajuan Tutor        9
d. Siklus Status Request        9
8. User Stories dan Kriteria Penerimaan        10
9. Kebutuhan UI/UX dan Design System        12
a. Ketentuan Desain        12
b. Isi Dashboard per Peran        12
10. Kebutuhan Nonfungsional        13
11. Rancangan Data        13
a. Entitas        13
b. Relasi Utama        14
12. Rekomendasi Arsitektur Teknis        16
a. Lapisan Aplikasi        16
b. Teknologi yang Digunakan        16
13. Alur Pengguna Utama        17
14. Rencana Implementasi Bertahap        17
15. Strategi Pengujian        18
16. Risiko dan Mitigasi        19
17. Asumsi dan Keputusan        19
18. Definisi Selesai (Definition of Done)        19
19. Deliverable Tugas        20
Lampiran A - Lembar Persetujuan PRD        21

1. Ringkasan Eksekutif
TutorMate adalah aplikasi desktop berbasis WPF (.NET) untuk mengelola layanan tutor akademik sebaya. Mahasiswa yang menguasai suatu mata kuliah dapat mendaftar menjadi tutor untuk mata kuliah tersebut, dan setelah diverifikasi Admin, mahasiswa lain dapat mengajukan permintaan (request) sesi belajar kepadanya. Seluruh proses, mulai dari registrasi, pendaftaran tutor, verifikasi, request, pelaksanaan sesi, hingga ulasan, tercatat dalam satu basis data dan diringkas dalam dashboard.
Produk dirancang dengan pendekatan Rich UI sesuai studi kasus SISMA: navigasi sidebar, kartu statistik, tabel data, grafik, aktivitas terbaru, formulir terstruktur, serta pilihan tema gelap/terang. Dokumen ini menjadi acuan desain, implementasi, dan pengujian.
a. Informasi Dokumen
Deskripsi
	Keterangan
	Nama Produk
	TutorMate - Aplikasi Manajemen Tutor Akademik
	Jenis Dokumen
	Product Requirements Document (PRD)
	Versi
	1.0 - 21 Oktober 2026
	Platform
	Desktop Windows 10/11 (64-bit) - WPF
	Teknologi Utama
	C#, XAML, .NET 10 (LTS), ADO.NET, SQLite
	Mata Kuliah
	Pemrograman Berbasis Kerangka Kerja (PBKK) - Kelas C
	Dosen
	Fajar Baskoro
	Kelompok
	Kelompok 8
	Referensi
	Studi kasus Rich UI dan PRD SISMA (Sistem Informasi Mahasiswa)

b. Tujuan Produk
* Menyediakan satu aplikasi desktop untuk mencari tutor per mata kuliah, mengajukan sesi belajar, dan memantau statusnya.
* Memberi wadah bagi mahasiswa berprestasi untuk mendaftar menjadi tutor sebaya pada mata kuliah yang dikuasainya.
* Membantu koordinator mengelola katalog mata kuliah, memverifikasi tutor, dan memantau layanan melalui pencarian, filter, dan operasi CRUD.
* Menyajikan ringkasan kondisi layanan dalam kartu statistik dan grafik yang dihitung dari basis data.
* Menerapkan arsitektur berlapis (MVVM, Service, Repository) agar aplikasi mudah diuji dan dikembangkan.
c. Pernyataan Produk
Untuk mahasiswa yang membutuhkan bantuan belajar maupun yang ingin berbagi kemampuan sebagai tutor, serta koordinator yang mengelola program tutor, TutorMate menyediakan aplikasi desktop terintegrasi yang menggabungkan katalog mata kuliah, pendaftaran dan verifikasi tutor, alur request tutor yang berstatus jelas, dan ringkasan analitik dalam satu lingkungan kerja.
2. Latar Belakang, Permasalahan, dan Manfaat
a. Latar Belakang
Mahasiswa yang kesulitan pada suatu mata kuliah umumnya mencari bantuan melalui grup percakapan atau kenalan. Di sisi lain, banyak mahasiswa berprestasi yang bersedia menjadi tutor sebaya, tetapi tidak ada wadah terstruktur yang mempertemukan keduanya. Program tutor yang dikelola himpunan, asisten, atau komunitas belajar juga masih mencatat pendaftar tutor dan permintaan sesi secara manual sehingga sulit dipantau.
b. Permasalahan
* Informasi tutor tersebar; mahasiswa sulit mengetahui siapa yang menguasai mata kuliah tertentu, jadwal ketersediaannya, dan apakah ia layak menjadi tutor.
* Permintaan bantuan lewat pesan pribadi tidak memiliki status yang jelas (diterima, ditolak, atau selesai) dan mudah tertimbun.
* Jadwal sesi sering bentrok karena tutor tidak memiliki catatan terpusat atas sesi yang sudah disepakati.
* Koordinator tidak memiliki gambaran cepat tentang mata kuliah yang paling banyak dicari, jumlah tutor aktif, dan kualitas layanan.
* Data perlu divalidasi, perubahannya dicatat, dan dilindungi dari akses oleh peran yang tidak berwenang.
c. Manfaat
Pihak
	Manfaat
	Mahasiswa (peminta)
	Menemukan tutor terverifikasi per mata kuliah, mengajukan sesi dalam satu formulir, dan memantau statusnya secara jelas.
	Mahasiswa sebagai tutor
	Mendapat wadah resmi untuk berbagi ilmu, menerima permintaan yang terstruktur (topik, jadwal, mode), terhindar dari jadwal bentrok, dan memiliki rekam jejak sesi serta ulasan.
	Admin / Koordinator
	Mengelola katalog mata kuliah dan verifikasi tutor di satu tempat serta memantau statistik layanan untuk pengambilan keputusan, misalnya membuka rekrutmen tutor pada mata kuliah yang paling diminati.

3. Sasaran, Indikator, dan Batasan
Sasaran
	Indikator keberhasilan awal
	Akun dan akses
	Pengguna dapat registrasi dan login; menu serta aksi dibatasi sesuai peran Admin atau Mahasiswa, dan fitur tutor hanya terbuka bagi mahasiswa yang pengajuannya disetujui.
	Pengelolaan data master
	Admin dapat menambah, melihat, mengubah, dan menghapus/menonaktifkan mata kuliah serta memverifikasi pengajuan tutor.
	Pencarian data
	Pencarian berdasarkan kode/nama mata kuliah atau nama tutor menghasilkan data yang relevan.
	Request tutor
	Mahasiswa dapat membuat request dan status berpindah sesuai alur (Menunggu → Diterima/Ditolak → Selesai) tanpa transisi yang tidak sah.
	Dashboard
	Kartu statistik dan grafik menggunakan hasil query basis data, bukan angka statis.
	Kualitas UI
	Navigasi konsisten, teks terbaca, dan tampilan mendukung dark/light mode.
	Keandalan
	Kesalahan validasi atau basis data ditangani dengan pesan yang dapat dipahami tanpa menutup aplikasi.
	Keamanan dasar
	Akses berbasis peran, query berparameter, dan password disimpan sebagai hash.

Batasan: dikerjakan oleh tiga orang dalam waktu sekitar dua minggu sebelum ETS; berupa aplikasi desktop berbasis .NET dengan Rich UI; judul unik di kelas dan tidak sama dengan SISMA; aplikasi dapat diunduh dan dijalankan tanpa instalasi server basis data. Angka pada desain antarmuka hanyalah contoh tampilan dan harus dihitung dari basis data.
4. Pengguna dan Hak Akses
a. Peran Pengguna
TutorMate memiliki dua peran akun, yaitu Admin dan Mahasiswa. Status tutor bukan peran akun terpisah, melainkan kemampuan tambahan yang dimiliki mahasiswa setelah pengajuan tutornya untuk suatu mata kuliah disetujui Admin. Dengan demikian, seorang mahasiswa dapat menjadi tutor pada mata kuliah yang dikuasainya sekaligus meminta bantuan pada mata kuliah lain.
Peran
	Kebutuhan utama
	Hak akses
	Admin / Koordinator
	Mengelola akun, katalog mata kuliah, verifikasi tutor, dan memantau seluruh layanan.
	Akses penuh termasuk pengguna, laporan, dan pengaturan. Akun dibuat melalui data awal (seed), tidak dapat registrasi mandiri.
	Mahasiswa
	Mencari tutor, mengajukan request, memantau status, memberi ulasan, dan mendaftar menjadi tutor.
	Melihat katalog dan tutor yang disetujui; membuat, melihat, dan membatalkan request miliknya; mengajukan diri sebagai tutor.
	Mahasiswa sebagai tutor
	Menanggapi request yang ditujukan kepadanya dan mengatur ketersediaan.
	Semua hak Mahasiswa ditambah menu Request Masuk; menerima, menolak, membatalkan, dan menyelesaikan request miliknya; menonaktifkan diri sebagai tutor.
	Pengguna belum login
	Masuk atau mendaftar.
	Hanya halaman Login dan Registrasi.

b. Matriks Hak Akses
Fitur
	Admin
	Mahasiswa
	Mahasiswa sebagai tutor
	Dashboard
	Semua data
	Data sendiri
	Data sendiri + ringkasan tutor
	Mata kuliah
	CRUD
	Lihat
	Lihat
	Pengajuan tutor
	Verifikasi, nonaktifkan
	Ajukan, lihat status
	Ubah jadwal/topik, aktif/nonaktif
	Daftar tutor
	Lihat semua
	Lihat yang disetujui
	Lihat yang disetujui
	Request tutor
	Lihat semua
	Buat, lihat, batal milik sendiri
	Terima, tolak, batal, selesai
	Ulasan
	Lihat
	Buat (sekali per sesi selesai)
	Lihat milik sendiri
	Pengguna
	Lihat, aktif/nonaktif, reset password
	-
	-
	Laporan
	Ya
	-
	-
	Pengaturan (tema, profil, password)
	Ya
	Ya
	Ya

Hak akses diperiksa di lapisan Service, bukan hanya dengan menyembunyikan tombol atau menu, sehingga aksi yang tidak diizinkan tetap ditolak.

Gambar 1. Diagram use case TutorMate
5. Ruang Lingkup Produk
a. In Scope - Rilis Awal
* Registrasi mahasiswa, login, dan logout.
* Dashboard per peran dengan kartu statistik, grafik, dan aktivitas terbaru.
* Katalog mata kuliah dengan pencarian, filter, dan pengurutan; CRUD mata kuliah oleh Admin.
* Pendaftaran tutor per mata kuliah, verifikasi oleh Admin, dan status tutor.
* Daftar tutor yang disetujui beserta jadwal ketersediaan, topik keahlian, dan rating.
* Pengajuan request tutor, tanggapan tutor, siklus status, riwayat status, dan ulasan.
* Pengelolaan pengguna dan laporan rekap untuk Admin.
* Tema dark mode dan light mode yang disimpan per pengguna.
* Validasi formulir, konfirmasi penghapusan, notifikasi hasil operasi, dan penanganan kesalahan.
* Basis data lokal SQLite yang dibuat otomatis saat aplikasi pertama dijalankan, beserta data contoh (seed) untuk demo.
b. Out of Scope - Rilis Berikutnya
* Pembayaran tutor secara daring dan integrasi dengan sistem akademik kampus.
* Chat realtime, panggilan video terintegrasi, dan notifikasi melalui email/WhatsApp.
* Penggunaan bersama melalui jaringan (multi-perangkat) dan sinkronisasi cloud.
* Versi web atau aplikasi mobile.
* Pemulihan kata sandi mandiri melalui email (reset dilakukan oleh Admin).
6. Struktur Navigasi dan Halaman
a. Menu Sidebar per Peran
Peran
	Menu sidebar
	Admin
	Dashboard · Mata Kuliah · Verifikasi Tutor · Daftar Tutor · Semua Request · Laporan · Pengguna · Pengaturan · Logout
	Mahasiswa
	Dashboard · Katalog Mata Kuliah · Daftar Tutor · Request Saya · Jadi Tutor · Pengaturan · Logout
	Mahasiswa sebagai tutor
	Menu Mahasiswa ditambah Request Masuk

b. Daftar Halaman
Halaman
	Elemen UI utama
	Tujuan
	Login
	Logo, NRP, password, tombol Masuk, tautan Daftar Akun Baru.
	Memverifikasi identitas pengguna.
	Registrasi
	NRP, nama lengkap, email, password, konfirmasi password.
	Membuat akun mahasiswa.
	Dashboard
	Kartu statistik, grafik, tabel ringkas, panel aktivitas terbaru.
	Ringkasan kondisi layanan sesuai peran.
	Mata Kuliah
	Pencarian, filter semester, DataGrid, jumlah tutor per mata kuliah; tombol Tambah/Edit/Hapus untuk Admin.
	Menelusuri dan mengelola katalog.
	Form Mata Kuliah
	Kode MK, nama, SKS, semester, deskripsi, Simpan/Batal.
	Membuat atau memperbarui mata kuliah.
	Daftar Tutor
	Kartu/tabel tutor: nama, mata kuliah, nilai, jadwal ketersediaan, topik keahlian, rating; filter mata kuliah; tombol Request.
	Memilih tutor.
	Jadi Tutor
	Form pengajuan (mata kuliah, nilai huruf, hari & jam ketersediaan, topik keahlian) dan tabel status pengajuan milik sendiri.
	Mendaftar dan memantau status sebagai tutor.
	Verifikasi Tutor
	Tabel pengajuan berstatus Menunggu Verifikasi, detail pengaju, tombol Setujui/Tolak dengan catatan.
	Memverifikasi tutor.
	Request Saya / Request Masuk / Semua Request
	Tab status (Menunggu, Diterima, Selesai, Lainnya), pencarian, DataGrid, badge status.
	Memantau request sesuai peran.
	Form Request
	Mata kuliah → tutor (terfilter), topik, tanggal & jam, durasi, mode, lokasi/tautan, catatan.
	Mengajukan request tutor.
	Detail Request
	Informasi request, linimasa riwayat status, tombol aksi sesuai peran, form ulasan.
	Menindaklanjuti satu request.
	Laporan
	Filter periode, mata kuliah, tutor, status; grafik dan tabel rekap; ekspor Excel (opsional).
	Menyajikan rekap layanan.
	Pengguna
	DataGrid pengguna, filter peran, aktif/nonaktif, reset password.
	Mengelola akun.
	Pengaturan
	Toggle tema, ubah profil, ganti password.
	Preferensi pengguna.

7. Kebutuhan Fungsional
a. Daftar Kebutuhan Fungsional
ID
	Fitur
	Persyaratan
	Prioritas
	FR-01
	Registrasi
	Mahasiswa mendaftar dengan NRP, nama, email, dan password. NRP 10 digit dan unik, email unik dan berformat valid, password minimal 8 karakter dan sama dengan konfirmasi. Akun baru berperan Mahasiswa.
	Must
	FR-02
	Login
	Sistem menerima NRP dan password yang valid serta menolak kredensial salah atau akun nonaktif dengan pesan umum yang aman.
	Must
	FR-03
	Logout
	Pengguna keluar, sesi dibersihkan, dan aplikasi kembali ke halaman Login.
	Must
	FR-04
	Hak akses
	Menu dan aksi dibatasi sesuai peran dan status tutor; pengecekan dilakukan juga di lapisan Service.
	Must
	FR-05
	Dashboard statistik
	Kartu statistik per peran dihitung dari data tersimpan (lihat Bab 9.b).
	Must
	FR-06
	Grafik
	Admin melihat jumlah request per mata kuliah (5 teratas) dan tren request per bulan (6 bulan terakhir) hasil query.
	Must
	FR-07
	Aktivitas terbaru
	Sistem menampilkan 10 aktivitas terakhir (tambah, ubah, hapus, perubahan status).
	Should
	FR-08
	Katalog mata kuliah
	Tabel mata kuliah dengan pencarian kode/nama, filter semester, pengurutan kolom, dan jumlah tutor yang disetujui.
	Must
	FR-09
	CRUD mata kuliah
	Admin menambah, mengubah, dan menghapus mata kuliah. Kode MK unik, SKS 1-6, semester 1-8.
	Must
	FR-10
	Integritas hapus
	Mata kuliah yang sudah dipakai pada pengajuan tutor atau request tidak dihapus permanen, tetapi dinonaktifkan setelah konfirmasi.
	Must
	FR-11
	Pengajuan tutor
	Mahasiswa mengajukan diri sebagai tutor untuk satu mata kuliah dengan mengisi nilai huruf, hari dan jam ketersediaan, serta topik keahlian. Satu mahasiswa hanya memiliki satu pengajuan per mata kuliah.
	Must
	FR-12
	Syarat tutor
	Nilai huruf pada mata kuliah yang diajukan minimal B (A, AB, atau B); jadwal ketersediaan wajib diisi.
	Must
	FR-13
	Verifikasi tutor
	Admin menyetujui atau menolak pengajuan; catatan wajib diisi saat menolak. Status pengajuan: Menunggu Verifikasi, Disetujui, Ditolak, Nonaktif (Gambar 2).
	Must
	FR-14
	Daftar tutor
	Daftar tutor berstatus Disetujui dengan pencarian nama, filter mata kuliah, jadwal ketersediaan, topik keahlian, rating rata-rata, dan jumlah sesi selesai.
	Must
	FR-15
	Status tutor
	Tutor atau Admin dapat menonaktifkan dan mengaktifkan kembali pengajuan yang Disetujui. Hanya tutor Disetujui yang dapat dipilih pada request.
	Must
	FR-16
	Buat request
	Mahasiswa memilih mata kuliah, lalu tutor yang disetujui untuk mata kuliah tersebut, dan mengisi topik, jadwal, durasi, mode (Online/Offline), lokasi atau tautan, serta catatan.
	Must
	FR-17
	Validasi request
	Jadwal minimal 1 jam dari sekarang; durasi 30-180 menit; tidak bentrok dengan sesi Diterima milik tutor; maksimal 3 request Menunggu per mahasiswa; mahasiswa tidak dapat meminta dirinya sendiri.
	Must
	FR-18
	Tanggapi request
	Tutor menerima atau menolak request; alasan wajib diisi saat menolak. Saat menerima, sistem memeriksa ulang bentrok jadwal.
	Must
	FR-19
	Siklus status request
	Status hanya berpindah sesuai diagram (Gambar 3) dan setiap perubahan dicatat pada riwayat status.
	Must
	FR-20
	Batalkan request
	Peminta dapat membatalkan request Menunggu/Diterima sebelum jadwal; tutor dapat membatalkan request Diterima sebelum jadwal dengan alasan.
	Must
	FR-21
	Kedaluwarsa otomatis
	Request Menunggu yang jadwalnya terlewati ditandai Kedaluwarsa saat data dimuat.
	Should
	FR-22
	Ulasan
	Setelah status Selesai, peminta memberi rating 1-5 dan komentar, satu kali per request.
	Should
	FR-23
	Kelola pengguna
	Admin melihat, menonaktifkan/mengaktifkan akun, dan mereset password pengguna.
	Should
	FR-24
	Laporan
	Admin melihat rekap request berdasarkan periode, mata kuliah, tutor, dan status.
	Should
	FR-25
	Tema UI
	Pengguna dapat mengganti dark/light mode; preferensi disimpan per pengguna.
	Should
	FR-26
	Notifikasi
	Sistem menampilkan pesan berhasil/gagal setelah aksi penting.
	Must
	FR-27
	Penanganan error
	Error basis data ditangani tanpa menutup aplikasi secara tidak terkendali dan dicatat ke log.
	Must
	FR-28
	Data awal
	Saat pertama dijalankan, aplikasi membuat basis data, akun Admin, dan data contoh agar dashboard dapat didemonstrasikan.
	Must
	FR-29
	Ekspor
	Admin dapat mengekspor tabel laporan ke Excel (.xlsx).
	Could

b. Aturan Bisnis Utama
1. Kelayakan tutor: request hanya dapat ditujukan kepada pengajuan tutor berstatus Disetujui untuk mata kuliah yang dipilih (relasi TutorRequests → TutorApplications).
2. Bukan diri sendiri: mahasiswa tidak dapat membuat request kepada pengajuan tutor miliknya sendiri.
3. Bentrok jadwal: dua sesi berstatus Diterima milik tutor yang sama tidak boleh beririsan, yaitu jika JadwalMulai baru < JadwalSelesai lama dan JadwalSelesai baru > JadwalMulai lama.
4. Batas antrean: satu mahasiswa maksimal memiliki 3 request berstatus Menunggu.
5. Penyelesaian sesi: request baru dapat ditandai Selesai setelah waktu jadwal mulai terlewati.
6. Rating tutor dihitung dari rata-rata tabel Reviews saat ditampilkan, bukan disimpan sebagai kolom, agar tidak terjadi data ganda yang tidak sinkron.
7. Pengajuan ganda: kombinasi tutor dan mata kuliah bersifat unik; pengajuan yang Ditolak dapat diajukan ulang dengan memperbarui data, bukan membuat baris baru.
c. Siklus Status Pengajuan Tutor

Gambar 2. Diagram status pengajuan tutor
d. Siklus Status Request

Gambar 3. Diagram status request tutor
Status request
	Arti
	Dapat berpindah ke
	Menunggu
	Request baru, belum ditanggapi tutor.
	Diterima, Ditolak, Dibatalkan, Kedaluwarsa
	Diterima
	Tutor menyetujui; jadwal terkunci.
	Selesai, Dibatalkan
	Ditolak
	Tutor menolak dengan alasan.
	- (status akhir)
	Dibatalkan
	Dibatalkan oleh peminta atau tutor sebelum jadwal.
	- (status akhir)
	Kedaluwarsa
	Jadwal lewat tanpa tanggapan tutor.
	- (status akhir)
	Selesai
	Sesi telah berlangsung.
	- (status akhir; dapat diberi ulasan)

8. User Stories dan Kriteria Penerimaan
US-01 - Registrasi dan login
Sebagai mahasiswa, saya ingin membuat akun dan masuk agar dapat memakai TutorMate.
* NRP atau email yang sudah terdaftar ditolak dengan pesan yang jelas, bukan error aplikasi.
* Password tersimpan sebagai hash, bukan teks biasa.
* Setelah login, menu yang tampil sesuai peran dan status tutor pengguna.
US-02 - Melihat dashboard
Sebagai pengguna yang telah login, saya ingin melihat ringkasan data agar memahami kondisi layanan dengan cepat.
* Kartu menampilkan nilai hasil query basis data.
* Menambah satu request baru langsung mengubah kartu dan grafik terkait.
* Jika data kosong, sistem menampilkan nilai nol atau empty state yang jelas.
US-03 - Mengelola mata kuliah
Sebagai Admin, saya ingin menambah, mengubah, dan menghapus mata kuliah agar katalog selalu mutakhir.
* Kolom wajib dan format divalidasi; kode MK duplikat ditolak.
* Dialog konfirmasi muncul sebelum penghapusan; pembatalan tidak mengubah data.
* Mata kuliah yang sudah dipakai hanya dapat dinonaktifkan.
US-04 - Mendaftar menjadi tutor
Sebagai mahasiswa, saya ingin mendaftar menjadi tutor untuk mata kuliah yang saya kuasai.
* Nilai di bawah B atau jadwal kosong ditolak dengan pesan yang jelas.
* Pengajuan kedua untuk mata kuliah yang sama ditolak.
* Pengajuan tersimpan berstatus Menunggu Verifikasi dan tampil di halaman Jadi Tutor.
US-05 - Memverifikasi tutor
Sebagai Admin, saya ingin memverifikasi pengajuan tutor agar hanya tutor yang layak tampil.
* Halaman Verifikasi menampilkan pengajuan Menunggu Verifikasi beserta nilai, jadwal, dan topik.
* Menolak tanpa catatan tidak dapat disimpan.
* Pengajuan yang disetujui langsung muncul di Daftar Tutor dan menu Request Masuk terbuka bagi tutor tersebut.
US-06 - Membuat request
Sebagai mahasiswa, saya ingin memilih tutor untuk suatu mata kuliah dan mengajukan sesi belajar.
* Daftar tutor terfilter otomatis sesuai mata kuliah dan hanya menampilkan tutor Disetujui selain diri sendiri.
* Jadwal bentrok, sudah lewat, atau durasi di luar batas ditolak dengan pesan yang jelas.
* Request tersimpan berstatus Menunggu dan muncul di Request Saya serta Request Masuk milik tutor.
US-07 - Menanggapi request
Sebagai tutor, saya ingin menerima atau menolak request agar jadwal saya terkelola.
* Tombol Terima dan Tolak hanya muncul untuk request Menunggu milik tutor tersebut.
* Menolak tanpa alasan tidak dapat disimpan.
* Perubahan status tercatat pada riwayat dan terlihat oleh peminta.
US-08 - Menyelesaikan sesi dan memberi ulasan
Sebagai peminta, saya ingin memberi ulasan setelah sesi selesai agar kualitas tutor terukur.
* Form ulasan hanya tersedia untuk request Selesai yang belum diulas.
* Rating wajib bernilai 1-5.
* Rating rata-rata tutor diperbarui di Daftar Tutor dan dashboard.
US-09 - Mengganti tema
Sebagai pengguna, saya ingin memilih tema tampilan yang nyaman.
* Tema diterapkan pada seluruh komponen utama.
* Kontras teks dan kontrol tetap terbaca pada kedua tema.
* Pilihan tema tetap berlaku saat login berikutnya.
9. Kebutuhan UI/UX dan Design System
a. Ketentuan Desain
Aspek
	Ketentuan desain
	Gaya visual
	Desktop modern, profesional, bersih, dan berorientasi produktivitas (mengacu pada referensi Rich UI SISMA).
	Warna
	Navy gelap (#2C3E50) untuk sidebar dan latar login, biru (#3498DB) untuk aksi utama, hijau (#1ABC9C) untuk aksi tutor, oranye (#E67E22) untuk aksi Admin, merah (#E74C3C) untuk aksi berbahaya, dan warna berbeda per kartu statistik. Status: Menunggu kuning, Diterima biru, Disetujui/Selesai hijau, Ditolak merah, Dibatalkan/Kedaluwarsa/Nonaktif abu-abu.
	Layout
	Sidebar tetap di kiri (240 px) berisi nama aplikasi, nama dan peran pengguna, menu, dan Logout; header berisi judul halaman, pencarian, dan toggle tema; area konten berisi kartu statistik, grafik, tabel, dan panel aktivitas.
	Tipografi
	Font sans-serif (Segoe UI) dengan hierarki judul (26 px), subjudul (18 px), label, isi, dan helper text yang konsisten.
	Komponen
	Tombol primary/secondary/danger dengan sudut membulat, input, dropdown, date-time picker, DataGrid, dialog, badge status, notifikasi, toggle tema, kartu tutor.
	Status UI
	Tersedia state loading, sukses, error, data kosong, dan akses ditolak.
	Konsistensi
	Margin, padding, radius, ikon, warna, dan ukuran kontrol diatur melalui ResourceDictionary bersama (design tokens) untuk tema terang dan gelap.
	Aksesibilitas
	Kontras memadai, label kontrol jelas, fokus keyboard terlihat, dan status tidak hanya dibedakan dengan warna (badge berisi teks).
	Responsivitas
	Layout menyesuaikan ukuran jendela; ukuran minimum 1280 × 720 tanpa elemen saling menutupi.

b. Isi Dashboard per Peran
Peran
	Kartu statistik
	Grafik dan panel
	Admin
	Mata kuliah aktif · Tutor disetujui · Pengajuan menunggu verifikasi · Request menunggu · Sesi selesai bulan ini
	Bar: request per mata kuliah (5 teratas); Line: tren request 6 bulan; Donut: proporsi status request; tabel tutor dengan rating tertinggi; aktivitas terbaru.
	Mahasiswa
	Request aktif · Sesi selesai · Mata kuliah yang memiliki tutor
	Status request terbaru; tutor dengan rating tertinggi pada mata kuliah yang pernah diminta; status pengajuan tutor (jika ada).
	Mahasiswa sebagai tutor
	Tambahan: Request masuk (Menunggu) · Sesi minggu ini · Rating rata-rata
	Jadwal sesi terdekat; ulasan terbaru.

Prototipe antarmuka dibuat di Figma sebelum implementasi penuh dan menjadi acuan visual bagi seluruh anggota.
10. Kebutuhan Nonfungsional
ID
	Kategori
	Persyaratan
	NFR-01
	Performa
	Navigasi antarhalaman dan interaksi UI terasa responsif (< 1 detik) pada laptop mahasiswa dengan ±1.000 baris data contoh.
	NFR-02
	Performa data
	Operasi baca/tulis tidak membuat UI membeku; gunakan async/await untuk operasi basis data.
	NFR-03
	Keamanan
	Seluruh query memakai parameter, password di-hash dengan BCrypt, tidak ada kredensial atau password asli pada skrip data awal, dan prinsip least privilege per peran.
	NFR-04
	Keandalan
	Kegagalan membuka basis data atau pelanggaran constraint (misalnya NRP ganda) ditangani dengan pesan yang dapat ditindaklanjuti.
	NFR-05
	Pemeliharaan
	Pemisahan View, ViewModel, Model, Repository, dan Service; logika bisnis tidak ditulis di code-behind; connection string disimpan di satu tempat.
	NFR-06
	Integritas data
	Primary key, foreign key, unique constraint, check constraint, dan transaksi untuk perubahan status beserta riwayatnya.
	NFR-07
	Kompatibilitas
	Windows 10/11 64-bit; dipaket self-contained sehingga tidak perlu memasang .NET runtime maupun server basis data.
	NFR-08
	Logging
	Error teknis dicatat ke berkas log lokal tanpa menampilkan password atau data sensitif kepada pengguna.

11. Rancangan Data
a. Entitas
Tabel
	Atribut inti
	Catatan
	Users
	UserId, NRP, Nama, Email, PasswordHash, Role, IsActive, PreferensiTema, CreatedAt
	NRP dan Email unik; Role ∈ {Admin, Mahasiswa}; password tidak disimpan sebagai teks.
	Subjects
	SubjectId, KodeMK, NamaMataKuliah, SKS, Semester, Deskripsi, IsActive
	KodeMK unik; SKS 1-6; Semester 1-8.
	TutorApplications
	ApplicationId, TutorId, SubjectId, NilaiHuruf, HariJadwal, TopikKeahlian, Status, CatatanAdmin, CreatedAt
	UNIQUE (TutorId, SubjectId); NilaiHuruf ∈ {A, AB, B}; Status ∈ {Menunggu Verifikasi, Disetujui, Ditolak, Nonaktif}.
	TutorRequests
	RequestId, MahasiswaId, ApplicationId, Topik, JadwalMulai, DurasiMenit, Mode, LokasiAtauTautan, Catatan, Status, AlasanStatus, CreatedAt, UpdatedAt
	DurasiMenit 30-180; Mode ∈ {Online, Offline}; indeks pada (ApplicationId, JadwalMulai) dan Status.
	RequestStatusHistory
	HistoryId, RequestId, StatusLama, StatusBaru, DiubahOleh, Waktu
	Ditulis dalam transaksi yang sama dengan perubahan status.
	Reviews
	ReviewId, RequestId, Rating, Komentar, CreatedAt
	RequestId unik (satu ulasan per request); Rating 1-5.
	ActivityLogs
	LogId, UserId, Aksi, Entitas, EntitasId, Waktu
	Sumber panel aktivitas terbaru; tidak menyimpan password atau rahasia.

b. Relasi Utama
* Users 1 ── N TutorApplications (mahasiswa sebagai tutor).
* Subjects 1 ── N TutorApplications.
* TutorApplications 1 ── N TutorRequests (menjamin tutor disetujui untuk mata kuliah yang diminta).
* Users 1 ── N TutorRequests (mahasiswa sebagai peminta).
* TutorRequests 1 ── N RequestStatusHistory dan 1 ── 0..1 Reviews.
* Users 1 ── N ActivityLogs.

Gambar 4. Entity Relationship Diagram (ERD) TutorMate
Aturan penghapusan: relasi dari Users, Subjects, dan TutorApplications memakai Restrict (data yang sudah dipakai dinonaktifkan, bukan dihapus), sedangkan RequestStatusHistory dan Reviews ikut terhapus (Cascade) hanya jika request dihapus oleh Admin.
12. Rekomendasi Arsitektur Teknis
a. Lapisan Aplikasi
Lapisan
	Tanggung jawab
	Contoh
	View
	Presentasi dan interaksi visual (XAML) tanpa logika bisnis.
	LoginView, RegisterView, MainWindow (shell + sidebar), DashboardView, SubjectView, TutorListView, TutorApplicationView, RequestView
	ViewModel
	State UI, ICommand, validasi tampilan, pemanggilan service; mengimplementasikan INotifyPropertyChanged.
	LoginViewModel, DashboardViewModel, SubjectViewModel, RequestFormViewModel
	Model
	Representasi entitas/domain.
	User, Subject, TutorApplication, TutorRequest, Review
	Repository
	Query SQL berparameter dan pemetaan hasil (ADO.NET).
	UserRepository, SubjectRepository, TutorApplicationRepository, RequestRepository
	Service
	Aturan bisnis lintas repository, hashing password, dan pengecekan hak akses.
	AuthService, TutorApplicationService, RequestService (status & bentrok), DashboardService, ActivityLogger
	Database
	Persistensi, constraint, dan query.
	SQLite: tutormate.db (dibuat otomatis dari skrip skema + seed)

b. Teknologi yang Digunakan
Kebutuhan
	Teknologi / Pustaka
	Alasan
	Bahasa & UI
	C#, XAML, WPF di atas .NET 10 (LTS)
	Sesuai kebutuhan aplikasi desktop .NET dan studi kasus SISMA.
	Pola MVVM
	INotifyPropertyChanged, ICommand (RelayCommand), ObservableCollection
	Sesuai rekomendasi arsitektur SISMA; tanpa pustaka tambahan.
	Akses data
	ADO.NET dengan Microsoft.Data.Sqlite
	Pola Connection-Command-Parameter yang sama dengan SqlClient pada SISMA, query berparameter.
	Basis data
	SQLite (berkas lokal)
	Tidak memerlukan instalasi server sehingga aplikasi dapat langsung dijalankan dari tautan unduhan.
	Grafik
	LiveCharts2 (LiveChartsCore.SkiaSharpView.WPF)
	Grafik bar, line, dan donut yang mendukung WPF dan tema.
	Keamanan
	BCrypt.Net-Next
	Hash password dengan salt.
	Ekspor (Could)
	ClosedXML
	Ekspor laporan ke .xlsx.
	Kolaborasi
	Git dan GitHub
	Satu repository kelompok, branch per fitur, .gitignore untuk bin/obj/.vs.
	Distribusi
	dotnet publish self-contained, single-file, win-x64
	Diunggah ke GitHub Releases atau Google Drive sebagai tautan unduhan.

Catatan: SISMA menggunakan SQL Server, sedangkan ketentuan tugas membebaskan pilihan basis data. SQLite dipilih agar dosen dan asisten dapat menjalankan aplikasi dari tautan unduhan tanpa konfigurasi server. Karena akses data tetap memakai pola ADO.NET berparameter, kode repository dapat dipindahkan ke SQL Server dengan perubahan minimal.
13. Alur Pengguna Utama
1. Mahasiswa membuka TutorMate, mendaftar akun, lalu login dengan NRP dan password.
2. Sistem memvalidasi kredensial dan peran, lalu menampilkan dashboard sesuai peran dari data basis data.
3. Mahasiswa yang ingin menjadi tutor membuka menu Jadi Tutor dan mengisi pengajuan; Admin memverifikasi di menu Verifikasi Tutor.
4. Mahasiswa lain membuka Daftar Tutor, memilih mata kuliah, dan melihat tutor yang disetujui beserta jadwal dan rating.
5. Mahasiswa mengisi Form Request; sistem memvalidasi jadwal, durasi, bentrok, dan batas antrean, lalu menyimpan request berstatus Menunggu.
6. Tutor membuka Request Masuk, lalu menerima (sistem memeriksa ulang bentrok) atau menolak dengan alasan.
7. Setiap perubahan status disimpan bersama riwayatnya dalam satu transaksi; notifikasi dan aktivitas diperbarui.
8. Setelah sesi berlangsung, tutor menandai Selesai dan peminta memberi ulasan.
9. Admin memantau dashboard, laporan, dan data master; pengguna logout dan akses ke halaman terlindungi dihentikan.
14. Rencana Implementasi Bertahap
Fase
	Tanggal
	Aktivitas
	Deliverable
	0 - Prototipe awal
	21 Okt 2026 (selesai)
	Login, registrasi, sidebar, katalog mata kuliah, form daftar tutor, daftar tutor, publish profile.
	Prototipe WPF yang berjalan
	1 - Perancangan
	22-23 Okt 2026
	Finalisasi PRD, ERD, desain UI di Figma, repository GitHub.
	PRD final, desain Figma, repo
	2 - Fondasi
	24-25 Okt 2026
	Migrasi ke SQLite + skema otomatis + seed, hash password, struktur MVVM dan Service, design tokens dan tema.
	Shell aplikasi dan basis data teruji
	3 - Data master
	26-27 Okt 2026
	CRUD mata kuliah, pengajuan dan verifikasi tutor, daftar tutor dengan filter.
	Modul mata kuliah dan tutor
	4 - Request
	28-29 Okt 2026
	Form request, validasi, terima/tolak/batal/selesai, riwayat, ulasan.
	Modul request tutor
	5 - Dashboard
	30-31 Okt 2026
	Query statistik, kartu, grafik, aktivitas, laporan, pengguna.
	Dashboard dinamis
	6 - QA & rilis
	1-2 Nov 2026
	Pengujian, perbaikan, publish, video demo, unggah tautan.
	Build rilis, video, tautan unduhan

Tanggal menyesuaikan jadwal ETS (minggu ke-8/9). Pembagian peran: Al Jad Kaukabudduri Hardianto - basis data, autentikasi, dan CRUD mata kuliah (melanjutkan prototipe awal); A. Wildan Kevin Assyauqi - modul pengajuan tutor dan request tutor beserta aturan bisnisnya; Muhammad Hilman Azhar - PRD, desain UI/UX (Figma), tema dan design system, dashboard dan grafik. Pengujian, video demo, dan publikasi dikerjakan bersama.
15. Strategi Pengujian
Area
	Skenario minimum
	Hasil yang diharapkan
	Registrasi
	NRP/email duplikat, NRP bukan 10 digit, password < 8 karakter, konfirmasi berbeda.
	Input tidak valid ditolak dengan pesan jelas, aplikasi tidak crash.
	Login
	Kredensial benar, salah, akun nonaktif.
	Akses sesuai aturan; pesan aman dan jelas.
	CRUD mata kuliah
	Tambah, lihat, ubah, hapus/nonaktif; kode MK duplikat.
	Perubahan tersimpan konsisten dan tampil di tabel.
	Pengajuan tutor
	Nilai C, jadwal kosong, pengajuan ganda, setujui, tolak tanpa catatan.
	Hanya pengajuan valid tersimpan; status berpindah sesuai Gambar 2.
	Validasi request
	Jadwal lampau, durasi 20/200 menit, jadwal bentrok, request Menunggu ke-4, request ke diri sendiri.
	Semua ditolak sesuai aturan bisnis.
	Alur status request
	Menunggu → Diterima → Selesai → Ulasan; penolakan tanpa alasan; pembatalan; kedaluwarsa.
	Hanya transisi sah yang berhasil; riwayat tercatat.
	Pencarian & filter
	Cari kode/nama, filter semester/status, hasil ada dan tidak ada.
	Hasil benar dan empty state terlihat.
	Dashboard
	Basis data berisi data dan kosong; tambah request baru.
	Statistik sesuai query dan berubah setelah data berubah.
	Hak akses
	Mahasiswa mencoba aksi Admin; tutor menanggapi request milik tutor lain.
	Aksi ditolak di Service, bukan hanya disembunyikan.
	Instalasi
	Jalankan hasil publish di komputer lain tanpa Visual Studio dan tanpa basis data.
	Aplikasi berjalan, basis data dan data awal dibuat otomatis.
	Tema & usability
	Pindah dark/light mode, ubah ukuran jendela, navigasi keyboard.
	Komponen konsisten dan terbaca.

16. Risiko dan Mitigasi
Risiko
	Dampak
	Mitigasi
	Waktu pengerjaan singkat
	Fitur tidak selesai sebelum ETS.
	Kerjakan prioritas Must lebih dulu; fitur Should/Could ditunda.
	Basis data membutuhkan server
	Aplikasi dari tautan unduhan tidak dapat dijalankan dosen.
	Gunakan SQLite dengan pembuatan skema otomatis; uji di komputer lain.
	Logika di code-behind
	Kode sulit diuji dan dirawat.
	Pindahkan state dan command ke ViewModel; review kode sebelum merge.
	Password tersimpan sebagai teks
	Kebocoran kredensial.
	Hash dengan BCrypt; data awal memakai akun dummy.
	Data dashboard statis
	Informasi tidak mencerminkan basis data.
	Gunakan query agregasi dan uji dengan menambah data.
	Hak akses hanya disembunyikan di UI
	Aksi tanpa izin tetap berjalan.
	Validasi peran di Service.
	Konflik kode antaranggota
	Pekerjaan hilang atau rusak.
	Git + GitHub, branch per fitur, pembagian modul jelas.
	Library grafik tidak kompatibel
	Keterlambatan implementasi.
	Uji paket pada Fase 2; siapkan alternatif grafik sederhana.

17. Asumsi dan Keputusan
* Rilis awal digunakan pada satu perangkat (demo dan pengujian); penggunaan bersama melalui jaringan di luar cakupan.
* Satu akun memiliki satu peran (Admin atau Mahasiswa); status tutor melekat pada pengajuan per mata kuliah.
* Akun Admin disediakan melalui data awal dan kredensialnya dicantumkan pada panduan instalasi.
* Ekspor Excel (FR-29) dikerjakan hanya jika fitur prioritas Must dan Should telah selesai.
* Preferensi tema disimpan per pengguna di basis data.
18. Definisi Selesai (Definition of Done)
* Semua persyaratan berprioritas Must telah diimplementasikan.
* Operasi CRUD, pencarian, pengajuan tutor, dan alur status request lulus skenario uji.
* Statistik dan grafik bersumber dari basis data.
* Validasi, pesan error, dan state data kosong tersedia.
* Query berparameter dan password tidak disimpan sebagai teks biasa.
* Akses fitur mengikuti peran yang ditetapkan.
* UI konsisten dengan design system dan mendukung tema gelap/terang.
* Aplikasi dapat diunduh dan dijalankan di Windows tanpa instalasi tambahan.
* Kode, skrip basis data, cara instalasi, hasil pengujian, dan video demo terdokumentasi.
19. Deliverable Tugas
No
	Deliverable
	Keterangan
	1
	PRD
	Dokumen ini.
	2
	Desain UI/UX
	Prototipe Figma halaman utama (Login, Dashboard per peran, Mata Kuliah, Daftar Tutor, Jadi Tutor, Request).
	3
	Implementasi
	Source code WPF di repository GitHub kelompok.
	4
	Video demo
	Demonstrasi alur lengkap: registrasi → pengajuan tutor → verifikasi Admin → request → tutor menerima → sesi selesai → ulasan → dashboard berubah.
	5
	Tautan unduhan
	Berkas aplikasi self-contained (zip/installer) di GitHub Releases atau Google Drive, beserta panduan singkat dan akun demo.

________________

Lampiran A - Lembar Persetujuan PRD
Peran
	Nama
	Tanggal
	Paraf
	Anggota
	Muhammad Hilman Azhar

	Anggota
	A. Wildan Kevin Assyauqi

	Anggota
	Al Jad Kaukabudduri Hardianto