using AutoMapper;
using FreshMarket.Domain.DTOs.Supply;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    public class SupplyMappings : Profile
    {
        public SupplyMappings() 
        {
            CreateMap<SupplyDto, Supply>();
            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
