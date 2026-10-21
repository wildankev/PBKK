using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using StudentRegistrationApp.Models;

namespace StudentRegistrationApp.Data
{
    public class StudentRepository
    {
        private static readonly string[] CandidateServers = new[]
        {
            "localhost\\SQLEXPRESS",
            "DESKTOP-VSVMC5P\\SQLEXPRESS",
            ".\\SQLEXPRESS",
            "(localdb)\\MSSQLLocalDB"
        };

        private string _activeConnectionString = "Server=localhost\\SQLEXPRESS;Database=StudentRegistrationDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=3;";

        public bool IsConnectedToDatabase { get; private set; }

        private readonly List<ProgramStudy> _fallbackPrograms = new();
        private readonly List<Student> _fallbackStudents = new();
        private int _nextStudentId = 1;

        public StudentRepository()
        {
            InitializeFallbackData();
            TryInitializeDatabase();
        }

        private void InitializeFallbackData()
        {
            _fallbackPrograms.AddRange(new[]
            {
                new ProgramStudy { ProgramId = 1, ProgramCode = "IF", ProgramName = "Teknik Informatika", IsActive = true },
                new ProgramStudy { ProgramId = 2, ProgramCode = "SI", ProgramName = "Sistem Informasi", IsActive = true },
                new ProgramStudy { ProgramId = 3, ProgramCode = "TE", ProgramName = "Teknik Elektro", IsActive = true },
                new ProgramStudy { ProgramId = 4, ProgramCode = "TI", ProgramName = "Teknik Industri", IsActive = true },
                new ProgramStudy { ProgramId = 5, ProgramCode = "TS", ProgramName = "Teknik Sipil", IsActive = true }
            });

            _fallbackStudents.Add(new Student
            {
                StudentId = _nextStudentId++,
                NIM = "5025241265",
                FullName = "A. Wildan Kevin Assyauqi",
                Email = "5025241265@student.its.ac.id",
                Gender = "Laki-laki",
                BirthDate = new DateTime(2004, 5, 15),
                PhoneNumber = "081234567890",
                Address = "Surabaya, Jawa Timur",
                ProgramId = 1,
                ProgramName = "Teknik Informatika",
                CreatedAt = DateTime.Now
            });
        }

        private void TryInitializeDatabase()
        {
            foreach (var server in CandidateServers)
            {
                try
                {
                    string masterConnStr = $"Server={server};Database=master;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;";
                    using (var masterConn = new SqlConnection(masterConnStr))
                    {
                        masterConn.Open();
                        using var checkCmd = new SqlCommand(
                            "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StudentRegistrationDB') " +
                            "CREATE DATABASE StudentRegistrationDB;", masterConn);
                        checkCmd.ExecuteNonQuery();
                    }

                    string dbConnStr = $"Server={server};Database=StudentRegistrationDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;";
                    using (var dbConn = new SqlConnection(dbConnStr))
                    {
                        dbConn.Open();

                        string ddl = @"
                            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Programs')
                            BEGIN
                                CREATE TABLE dbo.Programs (
                                    ProgramId INT IDENTITY(1,1) PRIMARY KEY,
                                    ProgramCode VARCHAR(20) UNIQUE NOT NULL,
                                    ProgramName NVARCHAR(100) NOT NULL,
                                    IsActive BIT NOT NULL DEFAULT 1,
                                    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
                                );
                                INSERT INTO dbo.Programs (ProgramCode, ProgramName, IsActive)
                                VALUES
                                    ('IF', 'Teknik Informatika', 1),
                                    ('SI', 'Sistem Informasi', 1),
                                    ('TE', 'Teknik Elektro', 1),
                                    ('TI', 'Teknik Industri', 1),
                                    ('TS', 'Teknik Sipil', 1);
                            END;

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
                                INSERT INTO dbo.Students (NIM, FullName, Email, Gender, BirthDate, PhoneNumber, Address, ProgramId)
                                VALUES
                                    ('5025241265', 'A. Wildan Kevin Assyauqi', '5025241265@student.its.ac.id', 'Laki-laki', '2004-05-15', '081234567890', 'Surabaya, Jawa Timur', 1);
                            END;";

                        using var ddlCmd = new SqlCommand(ddl, dbConn);
                        ddlCmd.ExecuteNonQuery();
                    }

                    _activeConnectionString = dbConnStr;
                    IsConnectedToDatabase = true;
                    return;
                }
                catch
                {
                    // Try next candidate server
                }
            }

            IsConnectedToDatabase = false;
        }

        public List<ProgramStudy> GetPrograms()
        {
            if (!IsConnectedToDatabase)
            {
                return new List<ProgramStudy>(_fallbackPrograms);
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();
                using var cmd = new SqlCommand("SELECT ProgramId, ProgramCode, ProgramName, IsActive FROM dbo.Programs WHERE IsActive = 1 ORDER BY ProgramCode ASC", conn);
                using var reader = cmd.ExecuteReader();

                var list = new List<ProgramStudy>();
                while (reader.Read())
                {
                    list.Add(new ProgramStudy
                    {
                        ProgramId = reader.GetInt32(0),
                        ProgramCode = reader.GetString(1),
                        ProgramName = reader.GetString(2),
                        IsActive = reader.GetBoolean(3)
                    });
                }
                return list;
            }
            catch
            {
                IsConnectedToDatabase = false;
                return new List<ProgramStudy>(_fallbackPrograms);
            }
        }

        public List<Student> GetAllStudents(string keyword = "", int? programIdFilter = null)
        {
            if (!IsConnectedToDatabase)
            {
                var query = _fallbackStudents.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim().ToLowerInvariant();
                    query = query.Where(s => s.NIM.ToLowerInvariant().Contains(kw) ||
                                             s.FullName.ToLowerInvariant().Contains(kw) ||
                                             s.Email.ToLowerInvariant().Contains(kw) ||
                                             s.ProgramName.ToLowerInvariant().Contains(kw));
                }
                if (programIdFilter.HasValue && programIdFilter.Value > 0)
                {
                    query = query.Where(s => s.ProgramId == programIdFilter.Value);
                }
                return query.OrderByDescending(s => s.StudentId).Select(s => s.Clone()).ToList();
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();

                string sql = @"
                    SELECT 
                        s.StudentId, s.NIM, s.FullName, s.Email, s.Gender, 
                        s.BirthDate, s.PhoneNumber, s.Address, s.ProgramId, 
                        p.ProgramName, s.CreatedAt
                    FROM dbo.Students s
                    INNER JOIN dbo.Programs p ON s.ProgramId = p.ProgramId
                    WHERE (@kw = '' OR s.NIM LIKE @kwLike OR s.FullName LIKE @kwLike OR s.Email LIKE @kwLike OR p.ProgramName LIKE @kwLike)
                      AND (@progId IS NULL OR s.ProgramId = @progId)
                    ORDER BY s.StudentId DESC;";

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@kw", SqlDbType.VarChar, 100).Value = keyword ?? "";
                cmd.Parameters.Add("@kwLike", SqlDbType.VarChar, 100).Value = $"%{keyword}%";
                cmd.Parameters.Add("@progId", SqlDbType.Int).Value = (programIdFilter.HasValue && programIdFilter.Value > 0) ? programIdFilter.Value : DBNull.Value;

                using var reader = cmd.ExecuteReader();
                var list = new List<Student>();
                while (reader.Read())
                {
                    list.Add(new Student
                    {
                        StudentId = reader.GetInt32(0),
                        NIM = reader.GetString(1),
                        FullName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Gender = reader.GetString(4),
                        BirthDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                        PhoneNumber = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        Address = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        ProgramId = reader.GetInt32(8),
                        ProgramName = reader.GetString(9),
                        CreatedAt = reader.GetDateTime(10)
                    });
                }
                return list;
            }
            catch
            {
                IsConnectedToDatabase = false;
                return GetAllStudents(keyword, programIdFilter);
            }
        }

        public bool IsNimExists(string nim, int excludeStudentId = 0)
        {
            if (!IsConnectedToDatabase)
            {
                return _fallbackStudents.Any(s => s.NIM.Equals(nim, StringComparison.OrdinalIgnoreCase) && s.StudentId != excludeStudentId);
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();
                using var cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.Students WHERE NIM = @nim AND StudentId != @excludeId", conn);
                cmd.Parameters.Add("@nim", SqlDbType.VarChar, 20).Value = nim;
                cmd.Parameters.Add("@excludeId", SqlDbType.Int).Value = excludeStudentId;

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch
            {
                IsConnectedToDatabase = false;
                return IsNimExists(nim, excludeStudentId);
            }
        }

        public bool InsertStudent(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (IsNimExists(student.NIM))
            {
                errorMessage = $"NIM '{student.NIM}' sudah terdaftar!";
                return false;
            }

            if (!IsConnectedToDatabase)
            {
                student.StudentId = _nextStudentId++;
                student.CreatedAt = DateTime.Now;
                var prog = _fallbackPrograms.FirstOrDefault(p => p.ProgramId == student.ProgramId);
                student.ProgramName = prog != null ? prog.ProgramName : "Tidak Diketahui";
                _fallbackStudents.Add(student.Clone());
                return true;
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();
                string sql = @"
                    INSERT INTO dbo.Students (NIM, FullName, Email, Gender, BirthDate, PhoneNumber, Address, ProgramId, CreatedAt)
                    OUTPUT INSERTED.StudentId
                    VALUES (@nim, @name, @email, @gender, @bdate, @phone, @addr, @progId, GETDATE());";

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@nim", SqlDbType.VarChar, 20).Value = student.NIM;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = student.FullName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = student.Email;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 20).Value = student.Gender;
                cmd.Parameters.Add("@bdate", SqlDbType.Date).Value = (object?)student.BirthDate ?? DBNull.Value;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 25).Value = (object?)student.PhoneNumber ?? DBNull.Value;
                cmd.Parameters.Add("@addr", SqlDbType.NVarChar, 255).Value = (object?)student.Address ?? DBNull.Value;
                cmd.Parameters.Add("@progId", SqlDbType.Int).Value = student.ProgramId;

                student.StudentId = Convert.ToInt32(cmd.ExecuteScalar());
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdateStudent(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (IsNimExists(student.NIM, student.StudentId))
            {
                errorMessage = $"NIM '{student.NIM}' sudah digunakan mahasiswa lain!";
                return false;
            }

            if (!IsConnectedToDatabase)
            {
                var existing = _fallbackStudents.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (existing != null)
                {
                    existing.NIM = student.NIM;
                    existing.FullName = student.FullName;
                    existing.Email = student.Email;
                    existing.Gender = student.Gender;
                    existing.BirthDate = student.BirthDate;
                    existing.PhoneNumber = student.PhoneNumber;
                    existing.Address = student.Address;
                    existing.ProgramId = student.ProgramId;
                    var prog = _fallbackPrograms.FirstOrDefault(p => p.ProgramId == student.ProgramId);
                    existing.ProgramName = prog != null ? prog.ProgramName : "";
                    return true;
                }
                errorMessage = "Data mahasiswa tidak ditemukan.";
                return false;
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();
                string sql = @"
                    UPDATE dbo.Students
                    SET NIM = @nim,
                        FullName = @name,
                        Email = @email,
                        Gender = @gender,
                        BirthDate = @bdate,
                        PhoneNumber = @phone,
                        Address = @addr,
                        ProgramId = @progId
                    WHERE StudentId = @id;";

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = student.StudentId;
                cmd.Parameters.Add("@nim", SqlDbType.VarChar, 20).Value = student.NIM;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = student.FullName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = student.Email;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 20).Value = student.Gender;
                cmd.Parameters.Add("@bdate", SqlDbType.Date).Value = (object?)student.BirthDate ?? DBNull.Value;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 25).Value = (object?)student.PhoneNumber ?? DBNull.Value;
                cmd.Parameters.Add("@addr", SqlDbType.NVarChar, 255).Value = (object?)student.Address ?? DBNull.Value;
                cmd.Parameters.Add("@progId", SqlDbType.Int).Value = student.ProgramId;

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool DeleteStudent(int studentId, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!IsConnectedToDatabase)
            {
                var item = _fallbackStudents.FirstOrDefault(s => s.StudentId == studentId);
                if (item != null)
                {
                    _fallbackStudents.Remove(item);
                    return true;
                }
                errorMessage = "Data tidak ditemukan.";
                return false;
            }

            try
            {
                using var conn = new SqlConnection(_activeConnectionString);
                conn.Open();
                using var cmd = new SqlCommand("DELETE FROM dbo.Students WHERE StudentId = @id;", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = studentId;

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
