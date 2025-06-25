using GreenSchoolCAT.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GreenSchoolCAT.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User DecryptedUser(string fullName)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DecryptUserByFullName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FullName", fullName);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Role = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }
    }
}

