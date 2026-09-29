using Microsoft.AspNetCore.Mvc;
using musictwins_api.Services;

namespace musictwins_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpotifyController : ControllerBase
{
    private readonly SpotifyService _spotifyService;

    public SpotifyController(SpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetArtistImage(string artistName)
    {
        if (string.IsNullOrWhiteSpace(artistName))
        {
            return BadRequest("Artista é obrigatório.");
        }

        var imageUrl = await _spotifyService.GetArtistImageAsync(artistName);
        
        if (imageUrl is null)
        {
            return NotFound("Imagem não encontrada");
        }

        return Ok(imageUrl);
    }
}
