using AutoMapper;
using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.DTOs.EmployeeDtos;
using Domain.Models.Entities;

namespace Companies.Infrastructure.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //-------------------
        // Destination = en property
        // .ForMember(
        //-------------------

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



        //-----------------
        // Destination = property inuti property
        // .ForPath(
        //-----------------

        //Common
        CreateMap<CompanyManipulationDto, Company>()
                .ForPath(
                    dest => dest.Address.StreetAddress,
                    opt => opt.MapFrom(src => src.StreetAddress))
                .ForPath(
                    dest => dest.Address.City,
                    opt => opt.MapFrom(src => src.City))
                .ForPath(
                    dest => dest.Address.Country,
                    opt => opt.MapFrom(src => src.Country));

        //Create
        CreateMap<CreateCompanyDto, Company>()
                .IncludeBase<CompanyManipulationDto, Company>();

        //Update
        CreateMap<UpdateCompanyDto, Company>()
                .IncludeBase<CompanyManipulationDto, Company>()
                 .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

        // CreateMap<Company, CreateCompanyDto>(); //ReverseMap
        // CreateMap<UpdateCompanyDto, Company>();
        //.ForMember(
        //   dest => dest.Id,
        //   opt => opt.Ignore());


        //Employee mappings
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();

    }
}
