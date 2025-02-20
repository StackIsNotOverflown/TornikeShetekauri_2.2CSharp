public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddSingleton(new EmployeeRepository(Configuration.GetConnectionString("DefaultConnection")));
    services.AddSingleton<ExcelService>();
}