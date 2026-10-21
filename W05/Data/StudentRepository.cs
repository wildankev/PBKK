using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using StudentManager.Models;

namespace StudentManager.Data
{
    public class StudentRepository
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

        private readonly List<Student> _fallbackMemoryData = new();
        private int _memoryNextId = 1;
        public bool IsDatabaseConnected { get; private set; } = false;

        public StudentRepository()
        {
            InitializeRepository();
        }

        private void InitializeRepository()
        {
            // Cek apakah LocalDB dapat dihubungkan
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                IsDatabaseConnected = true;

                // Buat tabel jika belum ada
                using var command = new SqlCommand(@"
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
                        INSERT INTO dbo.Students (NIM, Nama, Jurusan, Gender, Email)
                        VALUES
                            ('5025241265', 'A. Wildan Kevin Assyauqi', 'Teknik Informatika', 'Laki-laki', 'wildankev@student.its.ac.id'),
                            ('5025241033', 'Ferdian Ardra Hafizhan', 'Teknik Informatika', 'Laki-laki', 'ferdian@student.its.ac.id'),
                            ('5025241041', 'Salwa Nadia Maharani', 'Sistem Informasi', 'Perempuan', 'salwa@student.its.ac.id');
                    END;", connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                IsDatabaseConnected = false;
                SeedFallbackData();
            }
        }

        private void SeedFallbackData()
        {
            if (_fallbackMemoryData.Any()) return;

            _fallbackMemoryData.Add(new Student
            {
                Id = _memoryNextId++,
                NIM = "5025241265",
                Nama = "A. Wildan Kevin Assyauqi",
                Jurusan = "Teknik Informatika",
                Gender = "Laki-laki",
                Email = "wildankev@student.its.ac.id"
            });
            _fallbackMemoryData.Add(new Student
            {
                Id = _memoryNextId++,
                NIM = "5025241033",
                Nama = "Ferdian Ardra Hafizhan",
                Jurusan = "Teknik Informatika",
                Gender = "Laki-laki",
                Email = "ferdian@student.its.ac.id"
            });
            _fallbackMemoryData.Add(new Student
            {
                Id = _memoryNextId++,
                NIM = "5025241041",
                Nama = "Salwa Nadia Maharani",
                Jurusan = "Sistem Informasi",
                Gender = "Perempuan",
                Email = "salwa@student.its.ac.id"
            });
        }

        public List<Student> GetAll(string keyword = "")
        {
            if (IsDatabaseConnected)
            {
                try
                {
                    using var connection = new SqlConnection(ConnectionString);
                    using var command = new SqlCommand(
                        "SELECT Id, NIM, Nama, Jurusan, Gender, Email FROM Students " +
                        "WHERE NIM LIKE @keyword OR Nama LIKE @keyword OR Jurusan LIKE @keyword ORDER BY Id DESC",
                        connection);

                    command.Parameters.Add("@keyword", SqlDbType.VarChar, 100).Value = $"%{keyword}%";
                    connection.Open();

                    using var reader = command.ExecuteReader();
                    var list = new List<Student>();
                    while (reader.Read())
                    {
                        list.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            NIM = reader.GetString(1),
                            Nama = reader.GetString(2),
                            Jurusan = reader.GetString(3),
                            Gender = reader.GetString(4),
                            Email = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        });
                    }
                    return list;
                }
                catch
                {
                    IsDatabaseConnected = false;
                }
            }

            // Fallback memori
            var q = keyword.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q))
            {
                return _fallbackMemoryData.OrderByDescending(s => s.Id).ToList();
            }

            return _fallbackMemoryData
                .Where(s => s.NIM.ToLowerInvariant().Contains(q) ||
                            s.Nama.ToLowerInvariant().Contains(q) ||
                            s.Jurusan.ToLowerInvariant().Contains(q))
                .OrderByDescending(s => s.Id)
                .ToList();
        }

        public void Save(Student student)
        {
            if (IsDatabaseConnected)
            {
                try
                {
                    using var connection = new SqlConnection(ConnectionString);
                    var sql = student.Id == 0
                        ? "INSERT INTO Students (NIM, Nama, Jurusan, Gender, Email) VALUES (@nim, @nama, @jurusan, @gender, @email)"
                        : "UPDATE Students SET NIM=@nim, Nama=@nama, Jurusan=@jurusan, Gender=@gender, Email=@email WHERE Id=@id";

                    using var command = new SqlCommand(sql, connection);
                    command.Parameters.Add("@nim", SqlDbType.VarChar, 20).Value = student.NIM;
                    command.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = student.Nama;
                    command.Parameters.Add("@jurusan", SqlDbType.VarChar, 100).Value = student.Jurusan;
                    command.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = student.Gender;
                    command.Parameters.Add("@email", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email;

                    if (student.Id != 0)
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = student.Id;
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                    return;
                }
                catch
                {
                    IsDatabaseConnected = false;
                }
            }

            // Fallback memori
            if (student.Id == 0)
            {
                student.Id = _memoryNextId++;
                _fallbackMemoryData.Add(student.Clone());
            }
            else
            {
                var existing = _fallbackMemoryData.FirstOrDefault(s => s.Id == student.Id);
                if (existing != null)
                {
                    existing.NIM = student.NIM;
                    existing.Nama = student.Nama;
                    existing.Jurusan = student.Jurusan;
                    existing.Gender = student.Gender;
                    existing.Email = student.Email;
                }
            }
        }

        public void Delete(int id)
        {
            if (IsDatabaseConnected)
            {
                try
                {
                    using var connection = new SqlConnection(ConnectionString);
                    using var command = new SqlCommand("DELETE FROM Students WHERE Id=@id", connection);
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    connection.Open();
                    command.ExecuteNonQuery();
                    return;
                }
                catch
                {
                    IsDatabaseConnected = false;
                }
            }

            // Fallback memori
            var existing = _fallbackMemoryData.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                _fallbackMemoryData.Remove(existing);
            }
        }
    }
}
