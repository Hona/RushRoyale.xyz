using Newtonsoft.Json;
using RushRoyale.Infrastructure;

namespace RushRoyale.Application.Features.Player.Profile.Models;

public class PlayerProfile : ICosmosDocument
{
    public string UserId { get; set; } = string.Empty;
    public List<PlayerUnit> Units { get; set; } = new();
    
    public string Id => UserId;
}