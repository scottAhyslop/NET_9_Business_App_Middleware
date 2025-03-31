namespace NET_9_Business_App_Middleware.MiddlewareComponents
{
    public class CustomDemonstrationware:IMiddleware
    {
        public CustomDemonstrationware()
        {

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("It's in CustomDemonstrationware\r\n");
            
            await next(context);

            await context.Response.WriteAsync("It's after calling next in CustomDemonstrationware\r\n");
        }

    }//End class CustomDemonstrationware
}//End namespace MiddlewareComponents