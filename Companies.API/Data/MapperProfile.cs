using AutoMapper;
using Companies.API.Models.DTOs.CompanyDtos;
using Companies.API.Models.Entities;

namespace Companies.API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(
             dest => dest.Address,
             opt => opt.MapFrom(src => $"{src.Address.StreetAddress}, {src.Address.City}"));
            
    }
}
