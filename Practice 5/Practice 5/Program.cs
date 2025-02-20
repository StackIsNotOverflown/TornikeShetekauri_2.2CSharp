using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Practice_5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddSingleton(new EmployeeRepository(connectionString));
            builder.Services.AddSingleton<ExcelService>();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

          
            app.MapGet("/export", async (EmployeeRepository repo, ExcelService excelService) =>
            {
                var employees = await repo.GetEmployeesAsync();
                var excelData = excelService.GenerateExcel(employees);
                var fileName = "Employees.xlsx";

                return Results.File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            })
            .WithName("ExportEmployeesToExcel")
            .WithOpenApi();

            app.Run();
        }
    }
}