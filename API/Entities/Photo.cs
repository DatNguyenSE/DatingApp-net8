
using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }
    public required String Url { get; set; }
    public String? PublicId { get; set; }

    //Navigation property   
    [JsonIgnore]
    public Member Member { get; set; } = null!;
    public String MemberId { get; set; } = null!;
}
