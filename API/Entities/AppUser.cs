namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required String UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSaft { get; set; }
}
