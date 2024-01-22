using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.DTOs.SaleItem;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarket.Controllers
{
    [Route("api/saleItems")]
    [ApiController]
    [Authorize]
    public class SaleItemsController : Controller
    {
        private readonly ISaleItemService _saleItemService;
        public SaleItemsController(ISaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleItemDto>> GetSaleItems([FromQuery] SaleItemResourceParameters saleItemResourceParameters)
        {
            var saleItems = _saleItemService.GetSaleItems(saleItemResourceParameters);

            var metaData = GetPaginationMetaData(saleItems);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));


            return Ok(saleItems);
        }

        [HttpGet("{id}", Name = "GetSaleItemById")]
        public ActionResult<SaleItemDto> Get(int id)
        {
            var saleItem = _saleItemService.GetSaleItemById(id);

            return Ok(saleItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleItemForCreateDto saleItem)
        {
            _saleItemService.CreateSaleItem(saleItem);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleItemForUpdateDto saleItem)
        {
            _saleItemService.UpdateSaleItem(saleItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _saleItemService.DeleteSaleItem(id);

            return NoContent();
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<SaleItemDto> categoryDtOs)
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
