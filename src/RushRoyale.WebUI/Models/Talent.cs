using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class Talent
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("icon")] public string IconUrl { get; set; } = null!;
}