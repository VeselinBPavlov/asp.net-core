namespace IntroductionToAspNetCore
{
    using IntroductionToAspNetCore.Data;
    using IntroductionToAspNetCore.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Extensions;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                //options.UseSqlServer("Server=.;Database=CatsServerDb;Integrated Security=True;"));
                options.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\CatsServerDb_Data.mdf;Database=CatsServerDb;Integrated Security=true;"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            app.UseStaticFiles();
            
            app.UseHtmlContentType();

            app.UseRequestHandlers();
         
            app.MapWhen(ctx => ctx.Request.Path.Value.StartsWith("/cat")
                && ctx.Request.Method == HttpMethod.Get,
                catDetails =>
                {
                    catDetails.Run(async (context) => 
                    {
                        var urlParts = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                        
                        if (urlParts.Length < 2) 
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        var catId = 0;
                        int.TryParse(urlParts[1], out catId);
                        
                        if (catId == 0)
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        var db = context.RequestServices.GetRequiredService<CatsDbContext>();    

                        using(db) 
                        {
                            var cat = await db.Cats.FindAsync(catId);

                            if (cat == null)
                            {
                                context.Response.Redirect("/");
                                return; 
                            }

                            await context.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                            await context.Response.WriteAsync($@"<img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300"" />");
                            await context.Response.WriteAsync($"<p>{cat.Age}</p>");
                            await context.Response.WriteAsync($"<p>{cat.Breed}</p>");
                        }

                    });                                      
                });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = HttpStatusCode.NotFound;
                await context.Response.WriteAsync("404 Page Was Not Found!");
            });
        }


    }
}
