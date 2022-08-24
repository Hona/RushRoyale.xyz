using System.Collections.Immutable;
using System.Text.Json;
using RushRoyale.DataMiner;
using RushRoyale.DataMiner.Models.Output;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("RushRoyale.xyz")
        .LeftAligned()
        .Color(Color.Red));
        
AnsiConsole.Write(
    new FigletText("- Data Miner")
        .LeftAligned()
        .Color(Color.Green));
        
var rushRoyaleDirectory = AnsiConsole.Prompt(
    new TextPrompt<string>("Input the directory to Rush Royale's installation?")
        .DefaultValue(@"C:\GameCenter\Rush Royale PC"));

int? localizationKeysCount = 0;

await using (var localizationKeysStream = File.OpenRead(Path.Join(rushRoyaleDirectory, RushRoyalePaths.LocalizationKeys)))
{
    var localizationKeys = await JsonSerializer.DeserializeAsync<Dictionary<string, int>>(localizationKeysStream);

    localizationKeysCount = localizationKeys?.Count;
    AnsiConsole.WriteLine($"Loaded Localization Keys: {localizationKeysCount?.ToString() ?? "null"}");
}

var englishTranslations = await File.ReadAllTextAsync(Path.Join(rushRoyaleDirectory, RushRoyalePaths.LocalizationEnglishValues));

const char Splitter = '\uFFFD';

string ReplaceMetadata(string input, int startIndex = 0)
{
    const int metadataLength = 4;
    const int metadataMaxHex = 0x20;
    
    for (var i = startIndex; i < input.Length - metadataLength; i++)
    {
        var chunk = input.Substring(i, metadataLength);

        if (chunk.Count(x => x is ' ') >= 2)
        {
            continue;
        }
        
        if (chunk[0] <= metadataMaxHex && chunk[1] <= metadataMaxHex && chunk[3] <= metadataMaxHex)
        {
            Console.WriteLine($"Found metadata: \\u0x{(int)chunk[0]:x8}\\u0x{(int)chunk[1]:x8}\\u0x{(int)chunk[2]:x8}\\u0x{(int)chunk[3]:x8} '{chunk}'");
            input = input.Replace(chunk, Splitter.ToString());

            return ReplaceMetadata(input, i - metadataLength);
        }
    }

    return input;
}


var parts = ReplaceMetadata(englishTranslations)
    .TrimStart(Splitter)
    .Split(Splitter);

var localizationDictionary = new LocalizationDictionary();
foreach (var kvp in parts.Chunk(2))
{
    if (kvp.Length != 2)
    {
        Console.WriteLine(JsonSerializer.Serialize(kvp));
        
        continue;
    }

    var key = kvp[0].Trim().Trim('\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u000E', '\u000F', '\u0010');
    var value = kvp[1].Trim().Trim('\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u000E', '\u000F', '\u0010');
    localizationDictionary[key] = value;
}


Directory.CreateDirectory(Paths.OutputFolder);
var localizationsPath = Path.Join(Paths.OutputFolder, "localizations.json");

// Sort for the UI
var sortedLocalizationDictionary = localizationDictionary
    .OrderBy(x => x.Key)
    .ToImmutableSortedDictionary();

// Additional bytes still exist if you dont clear first
File.Delete(localizationsPath);
await using (var outputLocalizationStream = File.OpenWrite(localizationsPath))
{
    await JsonSerializer.SerializeAsync(outputLocalizationStream, sortedLocalizationDictionary);
}

Console.WriteLine($"Keys in Index: {localizationKeysCount}, keys data mined: {sortedLocalizationDictionary.Count}");

var shouldCopyToUi = AnsiConsole.Prompt(
    new ConfirmationPrompt("Copy to UI folder?"));

const string CopyOutputDirectory = "../../../../../RushRoyale.WebUI/wwwroot/data-mine";

if (shouldCopyToUi)
{
    Console.WriteLine("Copying...");
    Util.CopyDirectory(Paths.OutputFolder, CopyOutputDirectory, true);
    Console.WriteLine("Done copying!");
}


Console.WriteLine("Done, press any key to exit...");
Console.ReadLine();