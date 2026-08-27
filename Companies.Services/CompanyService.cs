using AutoMapper;
using Domain.Contracts;
using Service.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public CompanyService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }
}
