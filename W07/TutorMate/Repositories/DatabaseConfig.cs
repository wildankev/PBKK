using System;
using Microsoft.Data.SqlClient;

namespace TutorMate.Repositories
{
    public static class DatabaseConfig
    {
        private static string? _cachedConnectionString;

        public static string ConnectionString
        {
            get
            {
                if (_cachedConnectionString != null) return _cachedConnectionString;

                string[] candidates = new[]
                {
                    "Server=localhost\\SQLEXPRESS;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;",
                    "Server=.\\SQLEXPRESS;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;",
                    "Server=localhost;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;",
                    "Server=(localdb)\\MSSQLLocalDB;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;"
                };

                foreach (var connStr in candidates)
                {
                    try
                    {
                        using var conn = new SqlConnection(connStr);
                        conn.Open();
                        _cachedConnectionString = connStr;
                        return _cachedConnectionString;
                    }
                    catch
                    {
                        // Try next server candidate
                    }
                }

                _cachedConnectionString = "Server=localhost\\SQLEXPRESS;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;";
                return _cachedConnectionString;
            }
        }
    }
}
