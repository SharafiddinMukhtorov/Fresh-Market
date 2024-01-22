using AutoMapper;
using FreshMarket.Domain.DTOs.Customer;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.Extensions.Logging;

namespace FreshMarket.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IMapper mapper, FreshMarketDbContext context, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<CustomerDto> GetCustomers(CustomerResurceParameters customerResurceParameters)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(customerResurceParameters.SearchString))
            {
                query = query.Where(x => x.FirstName.Contains(customerResurceParameters.SearchString)
                || x.LastName.Contains(customerResurceParameters.SearchString));
            }
            if (customerResurceParameters.OrderBy is not null)
            {
                switch (customerResurceParameters.OrderBy)
                {
                    case "firstname":
                        query = query.OrderBy(x => x.FirstName); break;
                    case "firstnamedesc":
                        query = query.OrderByDescending(x => x.FirstName); break;
                    case "lastname":
                        query = query.OrderBy(x => x.LastName); break;
                    case "lastnamedesc":
                        query = query.OrderByDescending(x => x.LastName); break;
                }
            }
            var customers = query.ToPaginatedList(customerResurceParameters.PageSize, customerResurceParameters.PageNumber);
            var customersDto = _mapper.Map<List<CustomerDto>>(customers);

            return new PaginatedList<CustomerDto>(customersDto, customers.TotalCount, customers.CurrentPage, customers.PageSize);
        }

        public CustomerDto? GetCustomerById(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate)
        {
            var customerEntity = _mapper.Map<Customer>(customerToCreate);

            _context.Customers.Add(customerEntity);
            _context.SaveChanges();

            var customerDto = _mapper.Map<CustomerDto>(customerEntity);

            return customerDto;
        }

        public void UpdateCustomer(CustomerForUpdateDto customerToUpdate)
        {
            var customerEntity = _mapper.Map<Customer>(customerToUpdate);

            _context.Customers.Update(customerEntity);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer is not null)
            {
                _context.Customers.Remove(customer);
            }
            _context.SaveChanges();
        }
    }
}
