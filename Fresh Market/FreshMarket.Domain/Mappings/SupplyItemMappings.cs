using AutoMapper;
using FreshMarket.Domain.DTOs.SupplyItem;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    public class SupplyItemMappings : Profile
    {
        public SupplyItemMappings() 
        {
            CreateMap<SupplyItemDto, SupplyItem>();
            CreateMap<SupplyItem, SupplyItemDto>();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
