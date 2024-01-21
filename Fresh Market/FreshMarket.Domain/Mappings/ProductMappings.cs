using AutoMapper;
using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings() 
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}
