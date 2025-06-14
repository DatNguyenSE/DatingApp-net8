using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
        
         //đóng gói key(byte) dùng trong việc ký SigningCredentials
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim> //.khai báo 1 List<Claim> claims để thêm thông tin vào phần payload  ,Mỗi claim là một thông tin user
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };
       // chưa ký, chỉ dùng để dịnh nghĩa cách ký
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


        var tokenDescriptor = new SecurityTokenDescriptor //Payload
        {
            Subject = new ClaimsIdentity(claims), //(payload)
            Expires = DateTime.UtcNow.AddDays(7), //(payload)
            SigningCredentials = creds  // cach ky (header)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

        //(17) key là tokenKey ở dạng byte và được bọc lại bởi SymmetricSecurityKey
        // bọc mảng byte này vào một đối tượng của thư viện, để các thành phần khác (như SigningCredentials, JwtSecurityTokenHandler) biết đây là một “khóa đối xứng” hợp lệ.

        //(24)“Hãy dùng chính cái key này, theo thứ tự HMAC‑SHA512, để đóng dấu (ký) tất cả các token mà tôi sắp tạo.”