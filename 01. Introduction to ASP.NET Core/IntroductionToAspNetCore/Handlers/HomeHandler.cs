namespace IntroductionToAspNetCore.Handlers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using IntroductionToAspNetCore.Infrastructure;
    using IntroductionToAspNetCore.Data;


    public class HomeHandler : IHandler
    {
        public int Order => 1;

        public Func<HttpContext, bool> Condition => 
                ctx => ctx.Request.Path.Value == "/" && ctx.Request.Method == HttpMethod.Get;

        
        public RequestDelegate RequestHandler => async (context) =>
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
                };
    }
}