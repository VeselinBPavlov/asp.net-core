namespace IntroductionToAspNetCore.Infrastructure.Extensions
{
    using System;
    using System.Reflection;
    using System.Collections;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using IntroductionToAspNetCore.Middleware;
    using IntroductionToAspNetCore.Handlers;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();

        public static IApplicationBuilder UseHtmlContentType(this IApplicationBuilder builder)
            => builder.UseMiddleware<HtmlContentTypeMiddleware>();

        public static IApplicationBuilder UseRequestHandlers(this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Condition, app => 
                {
                    app.Run(handler.RequestHandler);
                });
            }

            return builder;
        }        
    }
}