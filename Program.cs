using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//Three example middleware components are added to the application pipeline. Each middleware component writes a message to the response stream before and after calling the next middleware component in the pipeline. The Run method is called to complete the application pipeline. This is a demonstration of the call order of the middleware components in the pipeline.Only a demo for real world example. 

// Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");
    
    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
});

//Using app.MapWhen() to show conditional branching in the pipeline. The .MapWhen() method creates a branched pipeline that is executed when the request path matches the specified path. It branches to another Pipeline, does that branch's computations and then returns to original pipeline, if its conditions are met. Confirmed by test

app.MapWhen((context) => {

    return context.Request.Path.StartsWithSegments("/employees") 
        && context.Request.Query.ContainsKey("id");
    }, 
    
    (appBuilder) =>
{
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #4: Before calling next\r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #4: After calling next\r\n");

    });

    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #7: Before calling next\r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #7: After calling next\r\n");
    });
});


//Using app.Map() method to demonstrate that .Map() method creates a branched pipeline that is executed when the request path matches the specified path. It branches to another Pipeline, does that branch's computations and then returns to original pipeline. Confirmed by test
app.Map("/employees", (appBuilder) =>
{
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #5: Before calling next\r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #5: After calling next\r\n");
    });

    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #6: Before calling next\r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #6: After calling next\r\n");
    });
});


// Middleware #2
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Middleware #2: Before calling next\r\n");

    await next(context);

    await context.Response.WriteAsync("Middleware #2: After calling next\r\n");
});


/*// Middleware #2 using  app.Run() method, demonstrating that .Run() method always creates a terminating middleware component that completes the HTTP response and returns to the previous middleware component in the pipeline. Breakpoints set and verified.
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Middleware #2: Before calling next\r\n");

    
});*/

// Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling next\r\n");

    await next(context);

    await context.Response.WriteAsync("Middleware #3: After calling next\r\n");
});


app.Run();//always creates a terminating middleware component that completes the HTTP response.

