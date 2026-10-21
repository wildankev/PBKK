-- =====================================================================
-- Tugas PBKK Pertemuan 6: Hands-on 1 - Database SQL Server
-- Nama  : A. Wildan Kevin Assyauqi
-- NRP   : 5025241265
-- Kelas : Pemrograman Berbasis Kerangka Kerja - C
-- =====================================================================

-- 1. Buat Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StudentRegistrationDB')
BEGIN
    CREATE DATABASE StudentRegistrationDB;
END;
GO

USE StudentRegistrationDB;
GO

-- 2. Buat Tabel Master: Programs (Program Studi)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Programs')
BEGIN
    CREATE TABLE dbo.Programs (
        ProgramId INT IDENTITY(1,1) PRIMARY KEY,
        ProgramCode VARCHAR(20) UNIQUE NOT NULL,
        ProgramName NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END;
GO

-- 3. Buat Tabel Transaksi / Anak: Students (Pendaftaran Mahasiswa)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Students')
BEGIN
    CREATE TABLE dbo.Students (
        StudentId INT IDENTITY(1,1) PRIMARY KEY,
        NIM VARCHAR(20) UNIQUE NOT NULL,
        FullName NVARCHAR(150) NOT NULL,
        Email VARCHAR(100) NOT NULL,
        Gender NVARCHAR(20) NOT NULL,
        BirthDate DATE NULL,
        PhoneNumber VARCHAR(25) NULL,
        Address NVARCHAR(255) NULL,
        ProgramId INT NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Students_Programs FOREIGN KEY (ProgramId)
            REFERENCES dbo.Programs(ProgramId)
            ON UPDATE CASCADE
            ON DELETE NO ACTION
    );
END;
GO

-- 4. Seeding Data Master: Programs
IF NOT EXISTS (SELECT 1 FROM dbo.Programs)
BEGIN
    INSERT INTO dbo.Programs (ProgramCode, ProgramName, IsActive)
    VALUES
        ('IF', 'Teknik Informatika', 1),
        ('SI', 'Sistem Informasi', 1),
        ('TE', 'Teknik Elektro', 1),
        ('TI', 'Teknik Industri', 1),
        ('TS', 'Teknik Sipil', 1);
END;
GO

-- 5. Seeding Data Mahasiswa (Hanya Data Wildan Sesuai Ketentuan)
IF NOT EXISTS (SELECT 1 FROM dbo.Students)
BEGIN
    DECLARE @ProgramIF INT = (SELECT TOP 1 ProgramId FROM dbo.Programs WHERE ProgramCode = 'IF');

    INSERT INTO dbo.Students (NIM, FullName, Email, Gender, BirthDate, PhoneNumber, Address, ProgramId)
    VALUES
        ('5025241265', 'A. Wildan Kevin Assyauqi', '5025241265@student.its.ac.id', 'Laki-laki', '2004-05-15', '081234567890', 'Surabaya, Jawa Timur', @ProgramIF);
END;
GO

-- 6. Query Pengujian Relasi INNER JOIN
SELECT 
    s.StudentId,
    s.NIM,
    s.FullName,
    s.Email,
    s.Gender,
    p.ProgramCode,
    p.ProgramName,
    s.BirthDate,
    s.PhoneNumber,
    s.Address,
    s.CreatedAt
FROM dbo.Students s
INNER JOIN dbo.Programs p ON s.ProgramId = p.ProgramId;
GO
