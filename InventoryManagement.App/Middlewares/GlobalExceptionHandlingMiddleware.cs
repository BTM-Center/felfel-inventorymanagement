using Shared.Exceptions;

namespace InventoryManagement.App.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotEnoughStockException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
            catch
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }
        }
    }
}
