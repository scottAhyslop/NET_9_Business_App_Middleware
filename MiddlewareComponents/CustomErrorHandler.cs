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
                context.Response.ContentType = "text/html";//the entire pipeline will be text/html
                await context.Response.WriteAsync("It's in CustomErrorHandler<br/>");

                await next(context);

                await context.Response.WriteAsync("It's after calling next in CustomErrorHandler<br/>");
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("<h2>Error:</h2>");
                await context.Response.WriteAsync($"<p>{ex.Message.ToString()}</p>");
                throw;
            }
            
        }
    }
}

