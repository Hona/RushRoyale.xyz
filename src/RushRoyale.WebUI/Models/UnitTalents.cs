using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class UnitTalents
{
    [JsonPropertyName("9")] public List<Talent> Level9 { get; set; } = null!;
    [JsonPropertyName("11")] public List<Talent> Level11 { get; set; } = null!;
    [JsonPropertyName("13")] public List<Talent> Level13 { get; set; } = null!;
    [JsonPropertyName("15")] public Talent Level15 { get; set; } = null!;
}