namespace NET_9_Business_App_Middleware.MiddlewareComponents
{
    public class CustomErrorHandler:IMiddleware
    {
        public CustomErrorHandler()
        {
            
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("It's in CustomErrorHandler\r\n");

                await next(context);

                await context.Response.WriteAsync("It's after calling next in CustomErrorHandler\r\n");
            }
            catch (Exception ex)
            {
                //context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h2>Error: </h2>");
                await context.Response.WriteAsync($"<p>{ex.Message}</p>");
                throw;
            }
            
        }
    }
}

