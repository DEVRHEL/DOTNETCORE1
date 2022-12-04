using FirstCourse.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FirstCourse.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddlewareExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware1>();
        }
    }
}
