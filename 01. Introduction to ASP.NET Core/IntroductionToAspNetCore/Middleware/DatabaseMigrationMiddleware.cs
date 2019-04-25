namespace IntroductionToAspNetCore.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using IntroductionToAspNetCore.Data;
    public class DatabaseMigrationMiddleware
    {
        private readonly RequestDelegate next;
        
        public DatabaseMigrationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context) 
        {
            context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();

            return this.next(context);
        }
    }
}