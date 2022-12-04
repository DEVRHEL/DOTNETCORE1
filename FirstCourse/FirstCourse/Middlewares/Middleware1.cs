using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FirstCourse.Middlewares
{
    public class Middleware1
    {
        private readonly RequestDelegate _next;
        public Middleware1(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<div>Call from middleware 1a</div>");
            await _next(context);
            await context.Response.WriteAsync("<div>Return from middleware 1a</div>");
        }
    }
}
