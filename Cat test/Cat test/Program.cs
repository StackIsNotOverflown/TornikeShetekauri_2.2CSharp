using System.Security.Cryptography.X509Certificates;

namespace Cat_test
{
    internal static class Program
    {
      
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
           
        }
    }
}