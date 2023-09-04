using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;

namespace ShopApp.Middlewares
{
    public class CheckSessionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IDbContextFactory<ShopContext> dbFactory;
        
        public CheckSessionMiddleware(RequestDelegate _next, IDbContextFactory<ShopContext> _dbFactory)
        {
            next = _next;
            dbFactory = _dbFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string sessionCookie = context.Request.Cookies["Id"];
            
            if(!Guid.TryParse(sessionCookie, out Guid guid))
            {
                context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return;
            }
            using ShopContext db = await dbFactory.CreateDbContextAsync();

            Session session = db.Sessions.FirstOrDefault(s => s.Id == guid);
            if (session == null)
            {
                context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return;
            }
            if(session.ExpirationDate < DateTime.Now)
            {
                context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return;
            }
            context.Response.Redirect(Consts.Urls.Front.home);
            await next(context);
        }
    }
}
