namespace RushRoyale.WebUI.Models.DataMine;

public class LocalizationDictionary : Dictionary<string, string> { };

public class Localization
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}