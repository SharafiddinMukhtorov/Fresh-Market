using AutoMapper;
using FreshMarket.Domain.DTOs.Customer;
using FreshMarket.Domain.Entities;

namespace FreshMarket.Domain.Mappings
{
    internal class CustomerMappings : Profile
    {
        public CustomerMappings() 
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerForCreateDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
        }
    }
}
