using AutoMapper;
using musictwins_api.DTOs;
using musictwins_api.Models;

namespace musictwins_api.Mapping;

public class TopArtistsMapper : Profile
{
    public TopArtistsMapper()
    {
        CreateMap<LastFmArtists, TopArtistsDto>().
            ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
            dest => dest.PlayCount,
            opt => opt.MapFrom(src => src.Playcount)
            )
            .ForMember(
                dest => dest.Image,
                opt => opt.MapFrom(src =>
                    src.Image.FirstOrDefault(x => x.Size == "extralarge")!.Text
                )
            )
            .ForMember(
            dest => dest.Rank,
            opt => opt.MapFrom(src => src.Attr.Rank));
    }
}
