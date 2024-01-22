using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.DTOs.Customer;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.Services;
using FreshMarketApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarket.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> Get([FromQuery] CustomerResurceParameters customerResurceParameters)
        {
            var customers = _customerService.GetCustomers(customerResurceParameters);

            var metaData = GetPaginationMetaData(customers);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
            _customerService.CreateCustomer(customer);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            _customerService.UpdateCustomer(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);

            return NoContent();
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<CustomerDto> customerDtOs)
        {
            return new PaginationMetaData
            {
                Totalcount = customerDtOs.TotalCount,
                PageSize = customerDtOs.PageSize,
                CurrentPage = customerDtOs.CurrentPage,
                TotalPages = customerDtOs.TotalPage,
            };
        }
    }
}
