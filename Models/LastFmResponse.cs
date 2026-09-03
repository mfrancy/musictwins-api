using System.Text.Json.Serialization;

namespace musictwins_api.Models;

public class LastFmResponse
{
    public LastFmUser User { get; set; } = new();

}

public class LastFmUser
{
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("realname")]
    public string RealName { get; set; } = string.Empty;

    [JsonPropertyName("track_count")]
    public string TrackCount { get; set; } = string.Empty;

    [JsonPropertyName("artist_count")]
    public string ArtistCount { get; set; } = string.Empty;

    [JsonPropertyName("playcount")]
    public string PlayCount { get; set; } = string.Empty;

    public List<LastFmImage> Image { get; set; } = [];
}

public class LastFmImage
{
    [JsonPropertyName("#text")]
    public string Text { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;
}

public class LastFmTopArtistsResponse
{
    [JsonPropertyName("topartists")]
    public LastFmTopArtists TopArtists { get; set; } = new();
}

public class LastFmTopArtists
{
    [JsonPropertyName("artist")]
    public List<LastFmArtists> Artists { get; set; } = [];
}

public class LastFmArtists
{
    [JsonPropertyName("streamable")]
    public string Streamable { get; set; } = String.Empty;

    [JsonPropertyName("image")]
    public List<LastFmImage> Image { get; set; } = [];

    [JsonPropertyName("mbid")]
    public string Mbid { get; set; } = String.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = String.Empty;

    [JsonPropertyName("playcount")]
    public string Playcount { get; set; } = String.Empty;

    [JsonPropertyName("@attr")]
    public LastFmAttr Attr { get; set; } = new();

    [JsonPropertyName("name")]
    public string Name { get; set; } = String.Empty;
}

public class LastFmAttr
{
    public string Rank { get; set; } = String.Empty;
}

