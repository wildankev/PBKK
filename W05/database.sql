-- Skrip Pembuatan Database dan Tabel StudentDB untuk SQL Server LocalDB
IF DB_ID('StudentDB') IS NULL
    CREATE DATABASE StudentDB;
GO

USE StudentDB;
GO

IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        NIM VARCHAR(20) NOT NULL,
        Nama VARCHAR(100) NOT NULL,
        Jurusan VARCHAR(100) NOT NULL,
        Gender VARCHAR(20) NOT NULL,
        Email VARCHAR(100) NULL
    );

    -- Data Awal (Seeding)
    INSERT INTO dbo.Students (NIM, Nama, Jurusan, Gender, Email)
    VALUES
        ('5025241265', 'A. Wildan Kevin Assyauqi', 'Teknik Informatika', 'Laki-laki', 'wildankev@student.its.ac.id'),
        ('5025241033', 'Ferdian Ardra Hafizhan', 'Teknik Informatika', 'Laki-laki', 'ferdian@student.its.ac.id'),
        ('5025241041', 'Salwa Nadia Maharani', 'Sistem Informasi', 'Perempuan', 'salwa@student.its.ac.id');
END;
GO
