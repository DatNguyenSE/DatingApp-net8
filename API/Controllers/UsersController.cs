using System.Threading.Tasks; //: Cung cấp các loại như Task để thực hiện xử lý bất đồng bộ (async).
using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; //Cung cấp các tính năng để xây dựng Web API như ControllerBase, [HttpGet], ActionResult, v.v.
using Microsoft.EntityFrameworkCore;

namespace API;

 // localhost 5001 -> api/users
public class UsersController(DataContext context) : BaseApiController //cung cấp nhiều phương thức tiện ích như Ok(), NotFound(), BadRequest()…
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    
    [Authorize]
    [HttpGet("{id}")]  //localhost 5001 -> api/users/{id}
    public async Task<ActionResult<AppUser>> GetUsers(String id)
    {
        var user = await context.Users.FindAsync(id); // await -> Hãy đi tìm user trong database và khi tìm xong THÌ MỚI gán kết quả vào biến user.”
        if (user == null) return NotFound();

        return user;
    }

}