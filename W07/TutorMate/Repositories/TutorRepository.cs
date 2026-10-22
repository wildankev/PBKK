using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TutorMate.Models;

namespace TutorMate.Repositories
{
    public class TutorRepository
    {
        private readonly string _connectionString = DatabaseConfig.ConnectionString;

        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "SELECT SubjectId, KodeMK, NamaMataKuliah, Deskripsi FROM Subjects";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subjects.Add(new Subject
                {
                    SubjectId = reader.GetInt32(0),
                    KodeMK = reader.GetString(1),
                    NamaMataKuliah = reader.GetString(2),
                    Deskripsi = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }
            return subjects;
        }

        public void InsertSubject(Subject subject)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "INSERT INTO Subjects (KodeMK, NamaMataKuliah, Deskripsi) VALUES (@KodeMK, @Nama, @Deskripsi)";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@KodeMK", subject.KodeMK);
            command.Parameters.AddWithValue("@Nama", subject.NamaMataKuliah);
            command.Parameters.AddWithValue("@Deskripsi", subject.Deskripsi ?? "");
            command.ExecuteNonQuery();
        }

        // Ambil Pendataan Tutor Terdaftar
        public List<TutorApplication> GetAllTutors()
        {
            var list = new List<TutorApplication>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT ta.ApplicationId, ta.TutorId, u.Nama, ta.SubjectId, s.NamaMataKuliah, 
                                    ta.NilaiHuruf, ta.HariJadwal, ta.TopikKeahlian
                             FROM TutorApplications ta
                             JOIN Users u ON ta.TutorId = u.UserId
                             JOIN Subjects s ON ta.SubjectId = s.SubjectId";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new TutorApplication
                {
                    ApplicationId = reader.GetInt32(0),
                    TutorId = reader.GetInt32(1),
                    NamaTutor = reader.GetString(2),
                    SubjectId = reader.GetInt32(3),
                    NamaMataKuliah = reader.GetString(4),
                    NilaiHuruf = reader.GetString(5),
                    HariJadwal = reader.GetString(6),
                    TopikKeahlian = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            }
            return list;
        }

        public void RegisterTutor(TutorApplication app)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"INSERT INTO TutorApplications (TutorId, SubjectId, NilaiHuruf, HariJadwal, TopikKeahlian) 
                             VALUES (@TutorId, @SubjectId, @NilaiHuruf, @HariJadwal, @TopikKeahlian)";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TutorId", app.TutorId);
            command.Parameters.AddWithValue("@SubjectId", app.SubjectId);
            command.Parameters.AddWithValue("@NilaiHuruf", app.NilaiHuruf);
            command.Parameters.AddWithValue("@HariJadwal", app.HariJadwal);
            command.Parameters.AddWithValue("@TopikKeahlian", app.TopikKeahlian ?? "");
            command.ExecuteNonQuery();
        }
    }
}