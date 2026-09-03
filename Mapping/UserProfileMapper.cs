using AutoMapper;
using musictwins_api.DTOs;
using musictwins_api.Models;

namespace musictwins_api.Mapping;

public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<LastFmResponse, UserProfileDto>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.User.Name)
            )
            .ForMember(
                dest => dest.Realname,
                opt => opt.MapFrom(src => src.User.RealName)
            )
            .ForMember(
                dest => dest.TrackCount,
                opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.User.TrackCount)
                    ? 0
                    : int.Parse(src.User.TrackCount)
                )
            )
            .ForMember(
                dest => dest.ArtistCount,
               opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.User.ArtistCount)
                    ? 0
                    : int.Parse(src.User.ArtistCount)
                )
            )
            .ForMember(
                dest => dest.PlayCount,
                opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.User.PlayCount)
                    ? 0
                    : int.Parse(src.User.PlayCount)
                )
            )
            .ForMember(
                dest => dest.Image,
                opt => opt.MapFrom(src =>
                    src.User.Image.FirstOrDefault(x => x.Size == "extralarge")!.Text
                )
            );
    }
}