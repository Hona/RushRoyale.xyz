using Newtonsoft.Json;

namespace RushRoyale.Infrastructure;

public interface ICosmosDocument
{
    [JsonProperty("id")]
    string Id { get; }
}