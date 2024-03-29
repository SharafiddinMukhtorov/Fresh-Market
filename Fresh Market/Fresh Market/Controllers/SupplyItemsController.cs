﻿using FreshMarket.Domain.DTOs.SupplyItem;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshMarket.Controllers
{
    [Route("api/supplyItems")]
    [ApiController]
    [Authorize]
    public class SupplyItemsController : Controller
    {
        private readonly ISupplyItemService _supplyItemService;
        public SupplyItemsController(ISupplyItemService supplyItemService)
        {
            _supplyItemService = supplyItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplyItemDto>> Get([FromQuery] SupplyItemResourceParameters supplyItemResourceParameters)
        {
            var supplyItems = _supplyItemService.GetSupplyItems(supplyItemResourceParameters);

            return Ok(supplyItems);
        }

        [HttpGet("{id}", Name = "GetSupplyItemById")]
        public ActionResult<SupplyItemDto> Get(int id)
        {
            var supplyItem = _supplyItemService.GetSupplyItemById(id);

            return Ok(supplyItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyItemForCreateDto supplyItem)
        {
            _supplyItemService.CreateSupplyItem(supplyItem);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyItemForUpdateDto supplyItem)
        {
            _supplyItemService.UpdateSupplyItem(supplyItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyItemService.DeleteSupplyItem(id);

            return NoContent();
        }
    }
}
