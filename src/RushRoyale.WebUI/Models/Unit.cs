using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class Unit
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("icon")] public string IconUrl { get; set; } = null!;
    [JsonPropertyName("rarity")] public Rarity Rarity { get; set; }
    [JsonPropertyName("faction")] public FactionType FactionType { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("talents")] public UnitTalents? Talents { get; set; } 
}