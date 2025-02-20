using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Practice_5
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Employees";
                var command = new SqlCommand(query, connection);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var employee = new Employee
                        {
                            personalID = reader.GetInt32(0),
                            gvari = reader.GetString(1),
                            saxeli = reader.GetString(2),
                            ganyofileba = reader.GetString(3),
                            qalaqi = reader.GetString(4),
                            regioni = reader.GetString(5),
                            raioni = reader.GetString(6),
                            xelfasi = reader.GetDecimal(7),
                            asaki = reader.GetInt32(8),
                            staji = reader.GetInt32(9),
                            tarigi_dabadebis = reader.GetString(10),
                            sqesi = reader.GetString(11),
                            misamarti_saxlis = reader.GetString(12),
                            teleponi_saxlis = reader.GetString(13),
                            mobiluri = reader.GetString(14),
                            email = reader.GetString(15)
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }
    }
}







