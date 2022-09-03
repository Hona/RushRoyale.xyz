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
    
    public async Task StoreClanAsync(RegisteredClan registeredClan)
    {
        var user = _currentUserService.GetDiscordUser();
        var guilds = await _discordService.GetUserGuildsAsync(user.Id);

        var guild = guilds.First(x => x.Id == registeredClan.GuildId);

        var guildUser = guild.GetUser(user.Id);

        if (!guildUser.Roles.Any(x => x.Name.Contains("officer", StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception("You must be an officer!");
        }

        await _genericRepository.CreateOrUpdateItemAsync(registeredClan,
            x => x.UserId!);
    }

    public async Task<IEnumerable<RegisteredClan>> GetRegisteredClansAsync(string userId)
    {
        var output =  await _genericRepository.GetItemListAsync<RegisteredClan>(userId, x => x.UserId!);
        return output;
    }

    public async Task<RegisteredClan> GetClan(string userId, ulong guildId, ulong roleId)
    {
        var output = await _genericRepository.GetItemAsync(new RegisteredClan
        {
            UserId = userId,
            GuildId = guildId,
            RoleId = roleId
        }, x => x.Id,
            x => x.UserId!);

        ArgumentNullException.ThrowIfNull(output);
        
        return output;
    }

    public async Task DeleteClanAsync(string userId, ulong guildId, ulong roleId)
    {
        await _genericRepository.DeleteItemAsync(new RegisteredClan
        {
            UserId = userId,
            GuildId = guildId,
            RoleId = roleId
        }, x => x.UserId!);
    }

    public async Task WarnUserAsync(string currentUserId, ulong guildId, ulong roleId, ulong userId)
    {
        var clan = await GetClan(currentUserId, guildId, roleId);

        var currentUser = _currentUserService.GetDiscordUser();

        var description = clan.SandalsWarningMessage ?? "👋 Hi!\n\n\n ⚠️ Just a warning Tournament reset is nearing and you haven't used all your Sandals yet!\n\n🗣️ If you are unable to use all of your sandals today, please message your clan leadership team";

        var embed = new EmbedBuilder()
            .WithTitle(clan.DisplayName)
            .WithDescription(description)
            .WithAuthor(currentUser)
            .WithCurrentTimestamp()
            .WithColor(Color.DarkOrange)
            .Build();

        await _discordService.MessageUserAsync(userId, embed: embed);
    }
}