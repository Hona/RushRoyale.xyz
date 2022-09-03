using Discord;
using RushRoyale.Application.Features.Player.Clans.Models;
using RushRoyale.Application.Services;
using RushRoyale.Discord.Services;
using RushRoyale.Infrastructure.Repositories;

namespace RushRoyale.Application.Features.Player.Clans;

public class ClanService
{
    private readonly GenericRepository _genericRepository;
    private readonly DiscordService _discordService;
    private readonly CurrentUserService _currentUserService;

    public ClanService(GenericRepository genericRepository, DiscordService discordService, CurrentUserService currentUserService)
    {
        _genericRepository = genericRepository;
        _discordService = discordService;
        _currentUserService = currentUserService;
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

    public async Task<RegisteredClan> GetClan(string userId, ulong guildId, ulong roleId)
    {
        var output = await _genericRepository.GetItemAsync<RegisteredClan>(new RegisteredClan
        {
            UserId = userId,
            GuildId = guildId,
            RoleId = roleId
        }, x => x.Id,
            x => x.UserId!);

        ArgumentNullException.ThrowIfNull(output);
        
        return output;
    }

    public async Task WarnUserAsync(string currentUserId, ulong guildId, ulong roleId, ulong userId)
    {
        var clan = await GetClan(currentUserId, guildId, roleId);

        var currentUser = _currentUserService.GetDiscordUser();

        var embed = new EmbedBuilder()
            .WithTitle(clan.DisplayName)
            .WithDescription("👋 Hi!\n\n\n ⚠️ Just a warning Tournament reset is nearing and you haven't used all your Sandals yet!")
            .WithAuthor(currentUser)
            .WithCurrentTimestamp()
            .WithColor(Color.DarkOrange)
            .Build();

        await _discordService.MessageUserAsync(userId, embed: embed);
    }
}