using ShopApp.Exceptions;

namespace ShopApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (RessourceNotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
            }
            catch (WrongPasswordException e)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(e.Message);
            }
            catch (ItemNotAvailableException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);

            }
        }
    }
}
