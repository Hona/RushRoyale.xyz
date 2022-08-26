using RushRoyale.Application.Features.Player.Clans.Models;
using RushRoyale.Infrastructure.Repositories;

namespace RushRoyale.Application.Features.Player.Clans;

public class ClanService
{
    private readonly GenericRepository _genericRepository;

    public ClanService(GenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }
    
    public async Task RegisterClanAsync(RegisteredClan registeredClan)
    {
        await _genericRepository.CreateItemAsync(registeredClan,
            x => x.UserId!);
    }

    public async Task<IEnumerable<RegisteredClan>> GetRegisteredClansAsync(string userId)
    {
        var output =  await _genericRepository.GetItemListAsync<RegisteredClan>(userId, x => x.UserId!);
        return output;
    }
}