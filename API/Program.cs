using API.Extensions;
using API.Middleware; //AddApplicationServices() ,AddIdentityService()

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.-----------

builder.Services.AddApplicationServices(builder.Configuration);  
builder.Services.AddIdentityService(builder.Configuration);     

var app = builder.Build();

// Configure the HTTP request pipeline.------------
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()       //Cho phép frontend truy cập API
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();                 // xac thuc Nếu hợp lệ → gắn danh tính (user identity) vào HttpContext.User
app.UseAuthorization();            // roi moi uy quyen

app.MapControllers();          //	Gọi đến controller để xử lý logic

app.Run();
