using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;

namespace ShopApp.Middlewares
{
    public class ToHomeMiddleware
    {
        protected readonly RequestDelegate next;
        protected readonly IDbContextFactory<ShopContext> dbFactory;

        public ToHomeMiddleware(RequestDelegate _next, IDbContextFactory<ShopContext> _dbFactory)
        {
            next = _next;
            dbFactory = _dbFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Redirect(Consts.Urls.Front.home);
        }
    }
}
