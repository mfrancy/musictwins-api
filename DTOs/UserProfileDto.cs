namespace musictwins_api.DTOs;

public class UserProfileDto
{
    public string Username { set; get; } = String.Empty;
    public string Realname { get; set; } = String.Empty;
    public string Image { get; set; } = String.Empty;
    public int PlayCount { get; set; }
    public int ArtistCount { get; set; }
    public int TrackCount { get; set; }



}
