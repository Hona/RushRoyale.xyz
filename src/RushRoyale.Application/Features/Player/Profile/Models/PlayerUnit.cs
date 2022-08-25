using System.Text.Json.Serialization;

namespace RushRoyale.Application.Features.Player.Profile.Models;

public class PlayerUnit
{
    public string UnitId { get; set; }
    public int UnitLevel { get; set; }
    
    [JsonIgnore]
    public bool Unlocked => UnitLevel is not 0;
}