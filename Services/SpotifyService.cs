using Microsoft.AspNetCore.WebUtilities;
using musictwins_api.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace musictwins_api.Services;

public class SpotifyService
{
    private readonly HttpClient _httpclient;
    private readonly IConfiguration _configuration;

    public SpotifyService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpclient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var clientId = _configuration["Spotify:ClientID"];
        var clientSecret = _configuration["Spotify:ClientSecret"];

        if (clientId is null || clientSecret is null)
        {
            throw new InvalidOperationException("Usuário não autenticado");
        }

        var credentials = $"{clientId}:{clientSecret}";

        var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://accounts.spotify.com/api/token"
        );

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Basic", encodedCredentials);

        request.Content = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            }
        );

        var response = await _httpclient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(content);

        var token = json.RootElement
            .GetProperty("access_token")
            .GetString();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException(
                "Spotify não retornou um access token."
            );
        }

        return token;
    }

    public async Task<string?> GetArtistImageAsync(string artistName)
    {
        var acessToken = await GetAccessTokenAsync();
        var baseUrl = "https://api.spotify.com/v1/search";

        var url = QueryHelpers.AddQueryString(baseUrl, new Dictionary<string, string?>
        {
            ["q"] = artistName,
            ["type"] = "artist"
        }
        );

        _httpclient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", acessToken);

        var response = await _httpclient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var spotifyResponse = JsonSerializer.Deserialize<SpotifySearchResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var artist = spotifyResponse?.Artists.Items.FirstOrDefault();
            return artist?.Images.FirstOrDefault()?.Url;
        }

        return null;
    }
}
