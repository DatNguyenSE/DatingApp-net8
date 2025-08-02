using System;

namespace API.DTOs;

public class SeedUserDto
{
    public required String Id { get; set; }
    public required String Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required String UserName { get; set; }
    public String? ImageUrl { get; set; }
    public DateTime Created { get; set; } 
    public DateTime LastActive { get; set; } 
    public required String Gender { get; set; }
    public required String? Description { get; set; }
    public required String City { get; set; }
    public required String Country { get; set; }

}
