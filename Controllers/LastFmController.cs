using Microsoft.AspNetCore.Mvc;
using musictwins_api.Services;

namespace musictwins_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LastFmController : ControllerBase
{
    private readonly LastFmService _lastFmService;

    public LastFmController(LastFmService lastFmService)
    {
        _lastFmService = lastFmService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfo(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return BadRequest("Username é obrigatório.");
        }

        var userInfo = await _lastFmService.GetUserInfoAsync(username);

        if (userInfo is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(userInfo);
    }

    [HttpGet("top-artists")]
    public async Task<IActionResult> GetTopArtists(string username)
    {
        
        if (string.IsNullOrWhiteSpace(username))
        {
            return BadRequest("Username é obrigatório.");
        }

        var topArtists = await _lastFmService.GetTopArtistsAsync(username);


        if (topArtists is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(topArtists);
    } 
}
