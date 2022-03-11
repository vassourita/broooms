namespace Broooms.Catalog.Infrastructure.Data;

using AutoMapper;
using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        this.CreateMap<Product, ProductCreateDto>().ReverseMap();
        this.CreateMap<Product, ProductUpdateDto>().ReverseMap();
    }
}
