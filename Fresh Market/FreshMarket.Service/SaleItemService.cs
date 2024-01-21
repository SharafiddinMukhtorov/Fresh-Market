using AutoMapper;
using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.DTOs.SaleItem;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<SaleItemService> _logger;

        public SaleItemService(IMapper mapper, FreshMarketDbContext context, ILogger<SaleItemService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<SaleItemDto> GetSaleItems(SaleItemResourceParameters saleItemResourceParameters)
        {
            var query = _context.SaleItems.AsQueryable();

            if (saleItemResourceParameters.ProductId is not null)
            {
                query = query.Where(x => x.ProductId == saleItemResourceParameters.ProductId);
            }
            if (saleItemResourceParameters.SaleId is not null)
            {
                query = query.Where(x => x.SaleId == saleItemResourceParameters.SaleId);
            }

            if (saleItemResourceParameters.UnitPrice is not null)
            {
                query = query.Where(x => x.UnitPrice == saleItemResourceParameters.UnitPrice);
            }

            if (saleItemResourceParameters.UnitPriceLessThan is not null)
            {
                query = query.Where(x => x.UnitPrice < saleItemResourceParameters.UnitPriceLessThan);
            }

            if (saleItemResourceParameters.Quantity is not null)
            {
                query = query.Where(x => x.Quantity == saleItemResourceParameters.Quantity);
            }

            if (saleItemResourceParameters.UnitPriceGreaterThan is not null)
            {
                query = query.Where(x => x.UnitPrice > saleItemResourceParameters.UnitPriceGreaterThan);
            }

            if (saleItemResourceParameters.QuantityLessThan is not null)
            {
                query = query.Where(x => x.Quantity < saleItemResourceParameters.QuantityLessThan);
            }

            if (saleItemResourceParameters.QuantityGreaterThan is not null)
            {
                query = query.Where(x => x.Quantity > saleItemResourceParameters.QuantityGreaterThan);
            }
            if (saleItemResourceParameters.OrderBy is not null)
            {
                switch (saleItemResourceParameters.OrderBy)
                {
                    case "quantity":
                        query = query.OrderBy(x => x.Quantity); break;
                    case "quantitydesc":
                        query = query.OrderByDescending(x => x.Quantity); break;
                    case "unitprice":
                        query = query.OrderBy(x => x.UnitPrice); break;
                    case "unitpricedesc":
                        query = query.OrderByDescending(x => x.UnitPrice); break;
                    case "productid":
                        query = query.OrderBy(x => x.ProductId); break;
                    case "productiddesc":
                        query = query.OrderByDescending(x => x.ProductId); break;
                    case "saleid":
                        query = query.OrderBy(x => x.SaleId); break;
                    case "saleiddesc":
                        query = query.OrderByDescending(x => x.SaleId); break;
                }
            }

            var saleitems = query.ToPaginatedList(saleItemResourceParameters.PageSize, saleItemResourceParameters.PageNumber);
            //var saleItems = query.ToList();
            var saleitemDto = _mapper.Map<List<SaleItemDto>>(saleitems);

            return new PaginatedList<SaleItemDto>(saleitemDto, saleitems.TotalCount, saleitems.CurrentPage, saleitems.PageSize);
        }

        public SaleItemDto? GetSaleItemById(int id)
        {
            var saleItem = _context.SaleItems.FirstOrDefault(x => x.Id == id);

            var saleItemDto = _mapper.Map<SaleItemDto>(saleItem);

            return saleItemDto;
        }

        public SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate)
        {
            var saleItemEntity = _mapper.Map<SaleItem>(saleItemToCreate);

            _context.SaleItems.Add(saleItemEntity);
            _context.SaveChanges();

            var saleItemDto = _mapper.Map<SaleItemDto>(saleItemEntity);

            return saleItemDto;
        }

        public void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate)
        {
            var saleItemEntity = _mapper.Map<SaleItem>(saleItemToUpdate);

            _context.SaleItems.Update(saleItemEntity);
            _context.SaveChanges();
        }

        public void DeleteSaleItem(int id)
        {
            var saleItem = _context.SaleItems.FirstOrDefault(x => x.Id == id);
            if (saleItem is not null)
            {
                _context.SaleItems.Remove(saleItem);
            }
            _context.SaveChanges();
        }
    }
}
