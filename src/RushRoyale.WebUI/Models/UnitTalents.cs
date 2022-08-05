using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class UnitTalents
{
    public UnitTalents()
    {
        Level9 = new List<Talent>
        {
            new(), new()
        };
        Level11 = new List<Talent>
        {
            new(), new()
        };
        Level13 = new List<Talent>
        {
            new(), new()
        };
        Level15 = new Talent();
    }
    
    [JsonPropertyName("9")] public List<Talent> Level9 { get; set; }
    [JsonPropertyName("11")] public List<Talent> Level11 { get; set; }
    [JsonPropertyName("13")] public List<Talent> Level13 { get; set; }
    [JsonPropertyName("15")] public Talent Level15 { get; set; }
}