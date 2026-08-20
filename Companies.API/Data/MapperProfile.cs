using AutoMapper;
using Companies.API.Models.DTOs.CompanyDtos;
using Companies.API.Models.DTOs.EmployeeDtos;
using Companies.API.Models.Entities;

namespace Companies.API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        //Company mappings
        CreateMap<Company, CompanyDto>()
            .ForMember(
            dest => dest.Address,
            opt => opt.MapFrom(src => $"{src.Address.StreetAddress}, " +
                                      $"{src.Address.City}" +
                                      $"{(string.IsNullOrEmpty(src.Address.Country) ? string.Empty : ", ")}" +
                                      $"{src.Address.Country}"));


        //CreateMap<CreateCompanyDto, Address>();

        //CreateMap<CreateCompanyDto, Company>()
        //    .ForMember(
        //     dest => dest.Address,
        //     opt => opt.MapFrom(src => src));

        CreateMap<Company, CreateCompanyDto>().ReverseMap();
        CreateMap<UpdateCompanyDto, Company>();
                 //.ForMember(
                 //   dest => dest.Id,
                 //   opt => opt.Ignore());


        //Employee mappings
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateEmployeeDto, Employee>();

    }
}
