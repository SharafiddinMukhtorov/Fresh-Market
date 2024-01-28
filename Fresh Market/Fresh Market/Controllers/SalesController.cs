using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarket.Controllers
{
    [Route("api/sales")]
    [ApiController]
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly ISaleItemService _saleItemService;
        public SalesController(ISaleService saleService, ISaleItemService saleItemService)
        {
            _saleService = saleService;
            _saleItemService = saleItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleDto>> Get([FromQuery] SaleResourceParameters saleResourceParameters)
        {

            var sales = _saleService.GetSales(saleResourceParameters);

            var metaData = GetPaginationMetaData(sales);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(sales);
        }

        [HttpGet("{id}", Name = "GetSaleById")]
        public ActionResult<SaleDto> Get(int id)
        {
            var sale = _saleService.GetSaleById(id);

            return Ok(sale);
        }
        [HttpPost]
        public ActionResult Post([FromBody] SaleForCreateDto sale)
        {
            _saleService.CreateSale(sale);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleForUpdateDto sale)
        {
            _saleService.UpdateSale(sale);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            _saleService.DeleteSale(id);

            return NoContent();
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<SaleDto> salesDtOs)
        {
            return new PaginationMetaData
            {
                Totalcount = salesDtOs.TotalCount,
                PageSize = salesDtOs.PageSize,
                CurrentPage = salesDtOs.CurrentPage,
                TotalPages = salesDtOs.TotalPage,
            };
        }
    }
}
