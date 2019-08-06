using System.Net.Http.Headers;
namespace IntroductionToAspNetCore.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using IntroductionToAspNetCore.Data;
    using IntroductionToAspNetCore.Infrastructure;
    public class HtmlContentTypeMiddleware
    {
        private readonly RequestDelegate next;
        
        public HtmlContentTypeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context) 
        {
            context.Response.Headers.Add(HttpHeader.ContentType, "text/html");

            return this.next(context);
        }
    }
}