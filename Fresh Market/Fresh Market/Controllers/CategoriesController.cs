using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarketApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get([FromQuery] CategoryResourceParameters categoryResourceParameters)
        {
            var categories = _categoryService.GetCategories(categoryResourceParameters);

            var metaData = GetPaginationMetaData(categories);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public ActionResult<ProductDto> GetProductsByCategoryId(
            int id,
            ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            _categoryService.CreateCategory(category);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            _categoryService.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<CategoryDto> categoryDtOs)
        {
            return new PaginationMetaData
            {
                Totalcount = categoryDtOs.TotalCount,
                PageSize = categoryDtOs.PageSize,
                CurrentPage = categoryDtOs.CurrentPage,
                TotalPages = categoryDtOs.TotalPage,
            };
        }
    }
}
