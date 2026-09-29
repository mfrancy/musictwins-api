namespace musictwins_api.Models;

public class SpotifySearchResponse
{
    public SpotifyArtists Artists { get; set; } = new();
}

public class SpotifyArtists
{
    public List<SpotifyArtist> Items { get; set; } = [];
}

public class SpotifyArtist
{
    public string Name { get; set; } = string.Empty;

    public List<SpotifyArtistImage> Images { get; set; } = [];
}

public class SpotifyArtistImage
{
    public string Url { get; set; } = string.Empty;
}
