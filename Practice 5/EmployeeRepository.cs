using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class EmployeeRepository
{
 private readonly string _connectionString;

 public EmployeeRepository(string Connectionstring)
 {
     _connectionString = Connectionstring;
 }
 public async Task<List<Employee>> GetEmployeeAsync()
 {
     var employees = new List<Employee>();
     using (var connection= new SQLConnection(_connectionString))
  {
            var query = "SELECT personaliID,gvari,saxeli,ganyofileba,qalaqi,regioni,raioni,xelfasi,asaki,staji,tarigi_dabadebi,sqesi,misamarti_saxli,teleponi_saxlis,mobiluri,email,ierarqia FROM Employees";
            var command = new SqlCommand(query, connection);
            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var employee = new Employee
                    {
                        personaliID = reader.GetInt32(0),
                        gvari = reader.GetString(1),
                        saxeli = reader.GetString(2),
                        ganyofileba = reader.GetString(3),
                        qalaqi = reader.GetString(4),
                        regioni = reader.GetString(5),
                        raioni = reader.GetString(6),
                        xelfasi = reader.GetInt32(7),
                        asaki = reader.GetString(8),
                        staji = reader.GetString(9),
                        tarigi_dabadebi = reader.GetString(10),
                        sqesi = reader.GetString(11),
                        misamarti_saxli = reader.GetString(12),
                        teleponi_saxlis = reader.GetString(13),
                        mobiluri = reader.GetString(14),
                        email = reader.GetString(15),
                        ierarqia = reader.GetString(16)

                    };
                    employees.Add(employee);
                }
            }
  }
        return employees;
 }                       
}                     
                       
                     
                      
                     
                        

                     
              
               
                  
                   