using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
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
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving employees: {ex.Message}");
                }
            }

            return employees;
        }

        // Add Employee to Database
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO Employees 
                              (personalID, gvari, saxeli, ganyofileba, qalaqi, regioni, raioni, xelfasi, asaki, staji, tarigi_dabadebis, sqesi, misamarti_saxlis, teleponi_saxlis, mobiluri, email) 
                              VALUES 
                              (@personalID, @gvari, @saxeli, @ganyofileba, @qalaqi, @regioni, @raioni, @xelfasi, @asaki, @staji, @tarigi_dabadebis, @sqesi, @misamarti_saxlis, @teleponi_saxlis, @mobiluri, @email)";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@personalID", employee.personalID);
                command.Parameters.AddWithValue("@gvari", employee.gvari);
                command.Parameters.AddWithValue("@saxeli", employee.saxeli);
                command.Parameters.AddWithValue("@ganyofileba", employee.ganyofileba);
                command.Parameters.AddWithValue("@qalaqi", employee.qalaqi);
                command.Parameters.AddWithValue("@regioni", employee.regioni);
                command.Parameters.AddWithValue("@raioni", employee.raioni);
                command.Parameters.AddWithValue("@xelfasi", employee.xelfasi);
                command.Parameters.AddWithValue("@asaki", employee.asaki);
                command.Parameters.AddWithValue("@staji", employee.staji);
                command.Parameters.AddWithValue("@tarigi_dabadebis", employee.tarigi_dabadebis);
                command.Parameters.AddWithValue("@sqesi", employee.sqesi);
                command.Parameters.AddWithValue("@misamarti_saxlis", employee.misamarti_saxlis);
                command.Parameters.AddWithValue("@teleponi_saxlis", employee.teleponi_saxlis);
                command.Parameters.AddWithValue("@mobiluri", employee.mobiluri);
                command.Parameters.AddWithValue("@email", employee.email);

                try
                {
                    await connection.OpenAsync();
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding employee: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
