using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  // Đăng ký dịch vụ xác thực (Authentication) bằng JWT 
    .AddJwtBearer(options =>                                              // cấu hình cách hệ thống kiểm tra token.
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
        options.TokenValidationParameters = new TokenValidationParameters    // Cấu hình cách hệ thống xác thực (validate) JWT token
        {
            ValidateIssuerSigningKey = true,                                                //  Nếu là false: app sẽ chấp nhận tất cả token kể cả không được ký 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)), // chỉ định khóa ký
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

        return services;
    }
}