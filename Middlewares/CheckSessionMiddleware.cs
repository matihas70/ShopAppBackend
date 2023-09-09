using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;

namespace ShopApp.Middlewares
{
    public abstract class CheckSessionMiddleware
    {
        protected readonly RequestDelegate next;
        protected readonly IDbContextFactory<ShopContext> dbFactory;

        public CheckSessionMiddleware(RequestDelegate _next, IDbContextFactory<ShopContext> _dbFactory)
        {
            next = _next;
            dbFactory = _dbFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (await checkSession(context))
            {
                await next.Invoke(context);
            }
                

        }
        protected abstract Task<bool> checkSession(HttpContext context);
    }

    public class CheckSessionHomeMiddleware : CheckSessionMiddleware
    {
        public CheckSessionHomeMiddleware(RequestDelegate _next, IDbContextFactory<ShopContext> _dbFactory) : base(_next, _dbFactory)
        {
        }
        protected override async Task<bool> checkSession(HttpContext context)
        {
            string sessionCookie = context.Request.Cookies["Id"];

            if (!Guid.TryParse(sessionCookie, out Guid guid))
            {
                if (sessionCookie != null)
                    context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return false;
            }
            using ShopContext db = await dbFactory.CreateDbContextAsync();

            Session session = db.Sessions.FirstOrDefault(s => s.Id == guid);
            if (session == null)
            {
                context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return false;
            }
            if (session.ExpirationDate < DateTime.Now)
            {
                context.Response.Cookies.Delete("Id");
                context.Response.Redirect(Consts.Urls.Front.login);
                return false;
            }
            return true;

        }
    }

    public class CheckSessionLoginMiddleware : CheckSessionHomeMiddleware
    {
        public CheckSessionLoginMiddleware(RequestDelegate _next, IDbContextFactory<ShopContext> _dbFactory) : base(_next, _dbFactory)
        {
        }
        protected override async Task<bool> checkSession(HttpContext context)
        {
            string sessionCookie = context.Request.Cookies["Id"];

            if (!Guid.TryParse(sessionCookie, out Guid guid))
            {
                context.Response.Cookies.Delete("Id");
                return true;
            }
            using ShopContext db = await dbFactory.CreateDbContextAsync();

            Session session = db.Sessions.FirstOrDefault(s => s.Id == guid);
            if (session == null)
            {
                context.Response.Cookies.Delete("Id");
                return true;
            }
            if (session.ExpirationDate < DateTime.Now)
            {
                context.Response.Cookies.Delete("Id");
                return true;
            }
            context.Response.Redirect(Consts.Urls.Front.home);
            return false;
        }
    }
}
