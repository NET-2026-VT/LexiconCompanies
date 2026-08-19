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
            opt => opt.MapFrom(src => $"{src.Address.StreetAddress}, " +
                                      $"{src.Address.City}" +
                                      $"{(string.IsNullOrEmpty(src.Address.Country) ? string.Empty : ", ")}" +
                                      $"{src.Address.Country}"));


        CreateMap<CreateCompanyDto, Address>();

        CreateMap<CreateCompanyDto, Company>()
            .ForMember(
             dest => dest.Address,
             opt => opt.MapFrom(src => src));

    }
}
