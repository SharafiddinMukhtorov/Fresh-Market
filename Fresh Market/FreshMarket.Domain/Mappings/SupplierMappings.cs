using AutoMapper;
using FreshMarket.Domain.DTOs.Supplier;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings() 
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
