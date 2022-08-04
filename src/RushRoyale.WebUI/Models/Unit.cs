using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class Unit
{
    public string Name { get; set; } = null!;
    [JsonPropertyName("icon")] public string IconUrl { get; set; } = null!;
    public Rarity Rarity { get; set; }
    [JsonPropertyName("faction")] public FactionType FactionType { get; set; }
    public string Description { get; set; } = null!;
}