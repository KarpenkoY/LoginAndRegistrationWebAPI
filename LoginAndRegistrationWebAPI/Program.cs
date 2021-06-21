using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LoginAndRegistrationWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Data.Database.EnsureCreated();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
