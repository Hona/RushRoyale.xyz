namespace RushRoyale.WebUI.Models.DeckSuggestions;

public class DeckSuggestion
{
    public Rating Rating { get; set; }
    public List<string> UnitIds { get; set; } = new();

    public bool IsValid() => UnitIds.Count == 5;
}