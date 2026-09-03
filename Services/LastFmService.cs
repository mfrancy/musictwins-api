using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using musictwins_api.DTOs;
namespace musictwins_api.Services;

using AutoMapper;
using musictwins_api.Models;
using System.Text.Json;

public class LastFmService
{
    private readonly HttpClient _httpclient;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public LastFmService(HttpClient httpcliente, IConfiguration configuration, IMapper mapper)
    {
        _httpclient = httpcliente;
        _configuration = configuration;
        _mapper = mapper;
    }



    public async Task<UserProfileDto?> GetUserInfoAsync(string username)
    {
        var apiKey = _configuration["LastFm:ApiKey"];
        var baseUrl = _configuration["LastFm:BaseUrl"];

        if (apiKey is null)
        {
            throw new InvalidOperationException("API Key não foi configurada");
        }
        ;

        var url = QueryHelpers.AddQueryString(
            baseUrl, new Dictionary<string, string?>
            {
                ["method"] = "user.getInfo",
                ["user"] = username,
                ["api_key"] = apiKey,
                ["format"] = "json",

            }
            );

        var response = await _httpclient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var lastFmResponse = JsonSerializer.Deserialize<LastFmResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (lastFmResponse is null)
            {
                throw new InvalidOperationException("Não foi possível interpretar a resposta da Last.fm.");
            }

            return _mapper.Map<UserProfileDto>(lastFmResponse);
        }
        else
        {
            return null;
        }
    }

    public async Task<List<TopArtistsDto>?> GetTopArtistsAsync(string username)
    {
        var apiKey = _configuration["LastFm:ApiKey"];
        var baseUrl = _configuration["LastFm:BaseUrl"];

        if (apiKey is null)
        {
            throw new InvalidOperationException("API Key não foi configurada");
        };

        var url = QueryHelpers.AddQueryString(
            baseUrl, new Dictionary<string, string?>
            {
                ["method"] = "user.gettopartists",
                ["user"] = username,
                ["api_key"] = apiKey,
                ["format"] = "json",

            });

        var response = await _httpclient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var lastFmResponse = JsonSerializer.Deserialize<LastFmTopArtistsResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (lastFmResponse is null)
            {
                throw new InvalidOperationException("Não foi possível interpretar a resposta da Last.fm.");
            }

            return _mapper.Map<List<TopArtistsDto>>(lastFmResponse.TopArtists.Artists);
        }
        else
        {
            return null;
        }

    }
}
