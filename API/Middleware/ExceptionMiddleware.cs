using System;
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next,
    ILogger<ExceptionMiddleware> logger, IHostEnvironment env)  //constructor injection
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var mode = env.IsDevelopment()
            ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(mode, options);
            await context.Response.WriteAsync(json);
        }
    }
}



// equestDelegate next: đại diện cho middleware tiếp theo trong pipeline. Middleware này phải gọi next(context) để chuyển tiếp request.

// ILogger<ExceptionMiddleware> logger: dùng để ghi log (thường là tới console hoặc file).

// IHostEnvironment env: cho biết môi trường hiện tại (Development, Staging, Production,...). Giúp kiểm tra xem có nên hiện StackTrace không.