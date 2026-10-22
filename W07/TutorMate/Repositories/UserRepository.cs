using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TutorMate.Models;

namespace TutorMate.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString = DatabaseConfig.ConnectionString;

        // 1. Autentikasi Login
        public User Authenticate(string nim, string password)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "SELECT UserId, NIM, Nama, Role FROM Users WHERE NIM=@NIM AND Password=@Password";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NIM", nim);
            command.Parameters.AddWithValue("@Password", password);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    NIM = reader.GetString(1),
                    Nama = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }
            return null;
        }

        // 2. Tambah User Baru (Register)
        public void Insert(User user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "INSERT INTO Users (NIM, Nama, Password, Role) VALUES (@NIM, @Nama, @Password, @Role)";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NIM", user.NIM);
            command.Parameters.AddWithValue("@Nama", user.Nama);
            command.Parameters.AddWithValue("@Password", user.Password ?? "1234");
            command.Parameters.AddWithValue("@Role", user.Role);
            command.ExecuteNonQuery();
        }

        // 3. Ambil Semua User (Dipakai MainViewModel)
        public List<User> GetAll()
        {
            var users = new List<User>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "SELECT UserId, NIM, Nama, Role FROM Users";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32(0),
                    NIM = reader.GetString(1),
                    Nama = reader.GetString(2),
                    Role = reader.GetString(3)
                });
            }
            return users;
        }

        // 4. Update Data User (Dipakai MainViewModel)
        public void Update(User user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "UPDATE Users SET NIM=@NIM, Nama=@Nama, Role=@Role WHERE UserId=@UserId";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@NIM", user.NIM);
            command.Parameters.AddWithValue("@Nama", user.Nama);
            command.Parameters.AddWithValue("@Role", user.Role);
            command.ExecuteNonQuery();
        }

        // 5. Hapus User (Dipakai MainViewModel)
        public void Delete(int userId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "DELETE FROM Users WHERE UserId=@UserId";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.ExecuteNonQuery();
        }
    }
}