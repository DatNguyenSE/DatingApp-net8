using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities;
  
public class Member
{
    public String Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public String? ImageUrl { get; set; }
    public required String UserName { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required String Gender { get; set; }
    public required String? Description { get; set; }
    public required String City { get; set; }
    public required String Country { get; set; }

    //Navigation property
    [JsonIgnore] // hidden data 
    public List<Photo> Photos { get; set; } = [];

    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public AppUser User { get; set; } = null!;

}
