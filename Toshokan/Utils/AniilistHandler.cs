using System.Net.Http;
using System.Web;
using AniListNet.Objects;
using Newtonsoft.Json;

namespace Toshokan.Utils;

public sealed class AniilistHandler
{
    private static readonly Lazy<AniilistHandler> lazy = new Lazy<AniilistHandler>(() => new AniilistHandler());
    public AniClient _aniClient;
    public static AniilistHandler Instance
    {
        get
        {
            return lazy.Value;
        }
    }

    public AniilistHandler()
    {
        _aniClient = new AniClient();
    }

    public async Task<User> GetUserInfo()
    {
        var user = await _aniClient.GetAuthenticatedUserAsync();
        return user;
    }

    public async void DoAuth(string token)
    {
        await _aniClient.TryAuthenticateAsync(token);
    }

}