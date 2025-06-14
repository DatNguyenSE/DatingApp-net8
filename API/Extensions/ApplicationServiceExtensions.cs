using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{                                      //sử dụng this để Biến hàm này thành extension method cho IServiceCollection
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnecion"));
        });
        services.AddCors(); //Cross-Origin Resource Sharing
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}