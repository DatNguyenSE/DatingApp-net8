namespace API.DTOs;

public class LoginDto
{
    public required string Email { get; set; }
    public string? Username { get; set; }    
    public required string password { get; set; }          

}