using System.Text.Json.Serialization;

namespace RushRoyale.WebUI.Models;

public class Talent
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    [JsonPropertyName("icon")] public string IconUrl { get; set; } = null!;
}