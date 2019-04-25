namespace IntroductionToAspNetCore.Handlers
{
    using System;
    using Microsoft.AspNetCore.Http;
    using IntroductionToAspNetCore.Infrastructure;
    using IntroductionToAspNetCore.Data;
    using Microsoft.Extensions.DependencyInjection;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition => 
            ctx => ctx.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler => async (context) =>
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
        };

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}