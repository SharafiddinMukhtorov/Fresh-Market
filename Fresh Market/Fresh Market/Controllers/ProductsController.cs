using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshMarketApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    // [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProductsAsync(
            [FromQuery] ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);


            var metaData = GetPaginationMetaData(products);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductDto> Get(int id)
        {
            var product = _productService.GetProductById(id);

            return Ok(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductForCreateDto product)
        {
            _productService.CreateProduct(product);

            return StatusCode(201);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductForUpdateDto product)
        {
            _productService.UpdateProduct(product);

            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<ProductDto> productDtOs)
        {
            return new PaginationMetaData
            {
                Totalcount = productDtOs.TotalCount,
                PageSize = productDtOs.PageSize,
                CurrentPage = productDtOs.CurrentPage,
                TotalPages = productDtOs.TotalPage,
            };
        }
    }
    public class ProductParams
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }
}
