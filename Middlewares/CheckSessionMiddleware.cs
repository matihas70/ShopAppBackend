using ShopApp.Entites;

namespace ShopApp.Middlewares
{
    public class CheckSessionMiddleware
    {
        private readonly RequestDelegate next;
        
        public CheckSessionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task InvokeAsync(HttpContext context, ShopContext db)
        {
            var cookies = context.Request.Cookies.ToList();
            await next(context);
        }
    }
}
