using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Practice_5
{
    public class EmployeeRepository
    {
        private readonly string _connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;";

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            ID = reader.GetInt32(0),
                            Gvari = reader.GetString(1),
                            Saxeli = reader.GetString(2),
                            Ganyofileba = reader.GetString(3),
                            Qalaqi = reader.GetString(4),
                            Regioni = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Raioni = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Xelfasi = reader.GetDouble(7),
                            Asaki = reader.GetInt32(8),
                            Staji = reader.GetInt32(9),
                            Tarigi_Dabadebis = reader.GetDateTime(10),
                            Sqesi = reader.GetString(11),
                            Misamarti_Saxlis = reader.GetString(12),
                            Teleponi_Saxlis = reader.GetString(13),
                            Mobiluri = reader.GetString(14),
                            Email = reader.GetString(15),
                            UnknownColumn = reader.GetInt32(16)
                        });
                    }
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Employees (Gvari, Saxeli, Ganyofileba, Qalaqi, Regioni, Raioni, Xelfasi, Asaki, Staji, Tarigi_Dabadebis, Sqesi, Misamarti_Saxlis, Teleponi_Saxlis, Mobiluri, Email, UnknownColumn) " +
                               "VALUES (@Gvari, @Saxeli, @Ganyofileba, @Qalaqi, @Regioni, @Raioni, @Xelfasi, @Asaki, @Staji, @Tarigi_Dabadebis, @Sqesi, @Misamarti_Saxlis, @Teleponi_Saxlis, @Mobiluri, @Email, @UnknownColumn)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Gvari", employee.Gvari);
                    command.Parameters.AddWithValue("@Saxeli", employee.Saxeli);
                    command.Parameters.AddWithValue("@Ganyofileba", employee.Ganyofileba);
                    command.Parameters.AddWithValue("@Qalaqi", employee.Qalaqi);
                    command.Parameters.AddWithValue("@Regioni", (object)employee.Regioni ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Raioni", (object)employee.Raioni ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Xelfasi", employee.Xelfasi);
                    command.Parameters.AddWithValue("@Asaki", employee.Asaki);
                    command.Parameters.AddWithValue("@Staji", employee.Staji);
                    command.Parameters.AddWithValue("@Tarigi_Dabadebis", employee.Tarigi_Dabadebis);
                    command.Parameters.AddWithValue("@Sqesi", employee.Sqesi);
                    command.Parameters.AddWithValue("@Misamarti_Saxlis", employee.Misamarti_Saxlis);
                    command.Parameters.AddWithValue("@Teleponi_Saxlis", employee.Teleponi_Saxlis);
                    command.Parameters.AddWithValue("@Mobiluri", employee.Mobiluri);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@UnknownColumn", employee.UnknownColumn);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
