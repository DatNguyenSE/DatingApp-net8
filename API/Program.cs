using API.Data;
using API.Extensions; //AddApplicationServices() ,AddIdentityService()
using API.Interfaces;
using API.Middleware;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.-----------

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddScoped<IMemberRepository, MemberRepository>();  


var app = builder.Build();

// Configure the HTTP request pipeline.------------
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()       //Cho phép frontend truy cập API
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();                 // xac thuc Nếu hợp lệ → gắn danh tính (user identity) vào HttpContext.User
app.UseAuthorization();            // roi moi uy quyen

app.MapControllers();          //	Gọi đến controller để xử lý logic

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
    }
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
