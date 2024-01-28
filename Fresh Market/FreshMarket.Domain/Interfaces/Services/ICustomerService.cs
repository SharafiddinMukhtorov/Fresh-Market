using FreshMarket.Domain.DTOs.Customer;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Pagination.PaginatedList;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        GetCustomersResponse GetCustomers(CustomerResurceParameters customerResourceParameters);
        CustomerDto? GetCustomerById(int id);
        CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate);
        void UpdateCustomer(CustomerForUpdateDto customerToUpdate);
        void DeleteCustomer(int id);
    }
}
