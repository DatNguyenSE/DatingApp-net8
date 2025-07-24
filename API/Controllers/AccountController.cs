using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // account/register

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        // neu same username thi hien error


        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSaft = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }


    [HttpPost("login")] //account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) //login by usernam & password
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>  
        x.UserName == loginDto.Username.ToLower());         //FirstOrDefaultAsync-> return User if true, else null
                                                            // tìm user theo tên sau đó mới xét tới password

        if (user == null) return Unauthorized("Invalid username or password"); // néu sai ten -> obj user = null

        using var hmac = new HMACSHA512(user.PasswordSaft);            //tim dung cthuc da ma khoa ->  

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));//tim duoc cthuc, thi ma khoa again rồi ss , ss password de sosanh chuoi password trong db

        for (int i = 0; i < ComputeHash.Length; i++)
        {
            if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password"); // nếu 1 ký tự nào khác
        }
        return new UserDto{
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
    
}