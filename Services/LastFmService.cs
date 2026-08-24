using Microsoft.AspNetCore.WebUtilities;
using musictwins_api.DTOs;
namespace musictwins_api.Services;

public class LastFmService
{
    private readonly HttpClient _httpclient;
    private readonly IConfiguration _configuration;

    public LastFmService(HttpClient httpcliente, IConfiguration configuration)
    {
        _httpclient = httpcliente;
        _configuration = configuration;
    }



    public async Task<UserProfileDto> GetUserInfoAsync(string username)
    {
        var apiKey = _configuration["LastFm:ApiKey"];
        var baseUrl = _configuration["LastFm:BaseUrl"];

        if (username is null)
        {
            throw new InvalidOperationException("API Key não foi configurada");
        };

        var url = QueryHelpers.AddQueryString(
            "https://ws.audioscrobbler.com/2.0/")
    }
}
