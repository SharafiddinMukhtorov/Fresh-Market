using AutoMapper;
using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    public class SaleMappings : Profile
    {
        public SaleMappings() 
        {
            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
