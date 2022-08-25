using RushRoyale.Application.Features.Player.Profile.Models;
using RushRoyale.Infrastructure.Repositories;

namespace RushRoyale.Application.Features.Player.Profile;

public class ProfileService
{
    private readonly GenericRepository _repository;

    public ProfileService(GenericRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PlayerProfile?> GetProfileAsync(string userId)
    {
        var output = await _repository.GetItemAsync(new PlayerProfile
        {
            UserId = userId
        }, x => x.UserId, 
            x => x.UserId);

        return output;
    }
    
    public async Task CreateOrUpdateProfileAsync(PlayerProfile profile)
    {
        await _repository.CreateOrUpdateItemAsync(profile, x => x.UserId);
    }
    
    public async Task DeleteProfileAsync(string userId)
    {
        await _repository.DeleteItemAsync(new PlayerProfile
        {
            UserId = userId
        }, x => x.UserId);
    }
}