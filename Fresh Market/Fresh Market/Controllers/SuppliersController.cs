﻿using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.DTOs.Supplier;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Pagination;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarket.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplierDto>> Get([FromQuery] SupplierResourceParameters supplierResourceParameters)
        {
            var suppliers = _supplierService.GetSuppliers(supplierResourceParameters);

            var metaData = GetPaginationMetaData(suppliers);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(suppliers);
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<SupplierDto> Get(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            return Ok(supplier);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplierForCreateDto supplier)
        {
            _supplierService.CreateSupplier(supplier);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierForUpdateDto supplier)
        {
            _supplierService.UpdateSupplier(supplier);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplierService.DeleteSupplier(id);

            return NoContent();
        }
        private PaginationMetaData GetPaginationMetaData(PaginatedList<SupplierDto> supplierDtOs)
        {
            return new PaginationMetaData
            {
                Totalcount = supplierDtOs.TotalCount,
                PageSize = supplierDtOs.PageSize,
                CurrentPage = supplierDtOs.CurrentPage,
                TotalPages = supplierDtOs.TotalPage,
            };
        }
    }
}
