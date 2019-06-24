namespace Panda.App
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    //using Panda.Persistence;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //using (var db = new PandaDbContext())
            //{
            //    db.Database.EnsureCreated();
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
