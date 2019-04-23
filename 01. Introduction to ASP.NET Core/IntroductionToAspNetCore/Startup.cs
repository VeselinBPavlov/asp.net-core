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
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer("Server=.;Database=CatsServerDb;Integrated Security=True;"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();
                return next();
            });

            app.UseStaticFiles();
            
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return next();
            });

            app.MapWhen(
                ctx => ctx.Request.Path.Value == "/"
                && ctx.Request.Method == HttpMethod.Get,
                home =>
                {
                    home.Run(async (context) =>
                    {
                        await context.Response.WriteAsync($"<h1>Fluffy Duffy Munchkin Cats</h1>");

                        var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                        using (db)
                        {
                            var cats = db.Cats.Select(c => new
                            {
                                c.Id,
                                c.Name
                            }).ToList();

                            await context.Response.WriteAsync("<ul>");

                            foreach (var cat in cats)
                            {
                                await context.Response.WriteAsync($@"<li><a href=""/cats/{cat.Id}"">{cat.Name}</a></li>");
                            }

                            await context.Response.WriteAsync("</ul>");
                            await context.Response.WriteAsync(@"
                                        <form action=""/cat/add"">
                                          <input type=""submit"" value=""Add Cat"" />
                                        </form>");
                        }                        
                    });
                });

            app.MapWhen(ctx => ctx.Request.Path.Value == "/cat/add",                
                catAdd =>
                {
                    catAdd.Run(async (context) =>
                    {
                        if (context.Request.Method == HttpMethod.Get)
                        {
                            context.Response.Redirect("/cat-add-form.html");
                        }
                        else if (context.Request.Method == HttpMethod.Post)
                        {
                            
                            var form = context.Request.Form;

                            int age = 0;
                            int.TryParse(form["Age"], out age);

                            var cat = new Cat
                            {
                                Name = form["Name"],
                                Age = age,
                                Breed = form["Breed"],
                                ImageUrl = form["ImageUrl"]
                            };
           
                            try
                            {
                                if (!IsValid(cat))
                                {
                                    throw new InvalidOperationException("Invalid Cat Data!");
                                }
                                var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                                using (db)
                                {
                                    db.Add(cat);

                                    await db.SaveChangesAsync();
                                }

                                context.Response.Redirect("/");
                            }
                            catch (System.Exception)
                            {
                                await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                                await context.Response.WriteAsync($@"<a href=""/cat/add"">Back To The Form</>");
                            }
                        }
                         
                    });
                });

            app.MapWhen(ctx => ctx.Request.Path.Value.StartsWith("/cat")
                && ctx.Request.Method == HttpMethod.Get,
                catDetails =>
                {

                });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = HttpStatusCode.NotFound;
                await context.Response.WriteAsync("404 Page Was Not Found!");
            });
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}
