-- =====================================================================
-- TutorMate - Aplikasi Manajemen Tutor Akademik (PBKK C Kelompok 8)
-- Anggota Kelompok:
-- 1. 5025241264 - Muhammad Hilman Azhar
-- 2. 5025241265 - A. Wildan Kevin Assyauqi
-- 3. 5025241248 - Al Jad Kaukabudduri Hardianto
-- =====================================================================

-- 1. Inisialisasi Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TutorMateDB')
BEGIN
    CREATE DATABASE [TutorMateDB];
END;
GO

USE [TutorMateDB];
GO

-- 2. Tabel Master Mata Kuliah (Subjects)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Subjects')
BEGIN
    CREATE TABLE [dbo].[Subjects](
        [SubjectId] [int] IDENTITY(1,1) PRIMARY KEY,
        [KodeMK] [varchar](20) NOT NULL,
        [NamaMataKuliah] [varchar](100) NOT NULL,
        [Deskripsi] [varchar](255) NULL
    );
END;
GO

-- 3. Tabel Pengguna (Users)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users](
        [UserId] [int] IDENTITY(1,1) PRIMARY KEY,
        [NIM] [varchar](20) UNIQUE NOT NULL,
        [Nama] [varchar](100) NOT NULL,
        [Password] [varchar](100) NOT NULL,
        [Role] [varchar](20) NOT NULL
    );
END;
GO

-- 4. Tabel Pengajuan dan Pendataan Tutor (TutorApplications)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TutorApplications')
BEGIN
    CREATE TABLE [dbo].[TutorApplications](
        [ApplicationId] [int] IDENTITY(1,1) PRIMARY KEY,
        [TutorId] [int] NOT NULL,
        [SubjectId] [int] NOT NULL,
        [NilaiHuruf] [varchar](5) NOT NULL,
        [HariJadwal] [varchar](100) NOT NULL,
        [TopikKeahlian] [varchar](255) NULL,
        CONSTRAINT FK_TutorApplications_Users FOREIGN KEY ([TutorId])
            REFERENCES [dbo].[Users] ([UserId])
            ON DELETE CASCADE,
        CONSTRAINT FK_TutorApplications_Subjects FOREIGN KEY ([SubjectId])
            REFERENCES [dbo].[Subjects] ([SubjectId])
            ON DELETE CASCADE
    );
END;
GO

-- 5. Seeding Data Master: Mata Kuliah
IF NOT EXISTS (SELECT 1 FROM [dbo].[Subjects])
BEGIN
    INSERT INTO [dbo].[Subjects] ([KodeMK], [NamaMataKuliah], [Deskripsi]) VALUES
    ('IF184501', 'Pemrograman Berbasis Kerangka Kerja', 'Pengembangan aplikasi desktop dan web modern berbasis framework .NET'),
    ('IF184301', 'Pemrograman Berorientasi Objek', 'Konsep OOP lanjutan, inheritance, polimorfisme, dan interface'),
    ('IF184201', 'Struktur Data', 'Pohon biner, graf, hash table, dan algoritma efisien'),
    ('IF184401', 'Basis Data', 'Perancangan skema relasional, SQL Server, dan normalisasi');
END;
GO

-- 6. Seeding Data Pengguna (Kelompok 8)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users])
BEGIN
    INSERT INTO [dbo].[Users] ([NIM], [Nama], [Password], [Role]) VALUES
    ('5025241265', 'A. Wildan Kevin Assyauqi', '1234', 'Mentor'),
    ('5025241264', 'Muhammad Hilman Azhar', '1234', 'Mentor'),
    ('5025241248', 'Al Jad Kaukabudduri Hardianto', '1234', 'Peserta');
END;
GO

-- 7. Seeding Data Pendaftaran Tutor Awal
IF NOT EXISTS (SELECT 1 FROM [dbo].[TutorApplications])
BEGIN
    DECLARE @UidWildan INT = (SELECT TOP 1 UserId FROM [dbo].[Users] WHERE NIM = '5025241265');
    DECLARE @SubPbkk INT = (SELECT TOP 1 SubjectId FROM [dbo].[Subjects] WHERE KodeMK = 'IF184501');
    DECLARE @UidHilman INT = (SELECT TOP 1 UserId FROM [dbo].[Users] WHERE NIM = '5025241264');
    DECLARE @SubPbo INT = (SELECT TOP 1 SubjectId FROM [dbo].[Subjects] WHERE KodeMK = 'IF184301');

    IF @UidWildan IS NOT NULL AND @SubPbkk IS NOT NULL
    BEGIN
        INSERT INTO [dbo].[TutorApplications] ([TutorId], [SubjectId], [NilaiHuruf], [HariJadwal], [TopikKeahlian])
        VALUES (@UidWildan, @SubPbkk, 'A', 'Senin & Rabu, 19:00 WIB', 'WPF, MVVM, dan C# .NET');
    END;

    IF @UidHilman IS NOT NULL AND @SubPbo IS NOT NULL
    BEGIN
        INSERT INTO [dbo].[TutorApplications] ([TutorId], [SubjectId], [NilaiHuruf], [HariJadwal], [TopikKeahlian])
        VALUES (@UidHilman, @SubPbo, 'A', 'Selasa & Kamis, 16:00 WIB', 'Java OOP dan Desain Pola');
    END;
END;
GO

-- 8. Kueri Verifikasi
SELECT 
    ta.ApplicationId,
    u.NIM,
    u.Nama AS NamaTutor,
    u.Role,
    s.KodeMK,
    s.NamaMataKuliah,
    ta.NilaiHuruf,
    ta.HariJadwal,
    ta.TopikKeahlian
FROM dbo.TutorApplications ta
INNER JOIN dbo.Users u ON ta.TutorId = u.UserId
INNER JOIN dbo.Subjects s ON ta.SubjectId = s.SubjectId;
GO
