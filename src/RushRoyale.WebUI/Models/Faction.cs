using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class Faction
{
    public string Name { get; set; } = null!;
    public FactionType? Value { get; set; }
    [JsonPropertyName("icon")] public string IconUrl { get; set; } = null!;
}