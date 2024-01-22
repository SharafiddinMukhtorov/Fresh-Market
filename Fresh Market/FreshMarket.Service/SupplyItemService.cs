using AutoMapper;
using FreshMarket.Domain.DTOs.SupplyItem;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.Extensions.Logging;

namespace FreshMarket.Services
{
    public class SupplyItemService : ISupplyItemService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<SupplyItemService> _logger;

        public SupplyItemService(IMapper mapper, FreshMarketDbContext context, ILogger<SupplyItemService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<SupplyItemDto> GetSupplyItems(SupplyItemResourceParameters supplyItemResourceParameters)
        {
            var query = _context.SupplyItems.AsQueryable();

            if (supplyItemResourceParameters.ProductId is not null)
            {
                query = query.Where(x => x.ProductId == supplyItemResourceParameters.ProductId);
            }
            if (supplyItemResourceParameters.SupplyId is not null)
            {
                query = query.Where(x => x.SupplyId == supplyItemResourceParameters.SupplyId);
            }

            if (supplyItemResourceParameters.UnitPrice is not null)
            {
                query = query.Where(x => x.UnitPrice == supplyItemResourceParameters.UnitPrice);
            }

            if (supplyItemResourceParameters.UnitPriceLessThan is not null)
            {
                query = query.Where(x => x.UnitPrice < supplyItemResourceParameters.UnitPriceLessThan);
            }

            if (supplyItemResourceParameters.Quantity is not null)
            {
                query = query.Where(x => x.Quantity == supplyItemResourceParameters.Quantity);
            }

            if (supplyItemResourceParameters.UnitPriceGreaterThan is not null)
            {
                query = query.Where(x => x.UnitPrice > supplyItemResourceParameters.UnitPriceGreaterThan);
            }

            if (supplyItemResourceParameters.QuantityLessThan is not null)
            {
                query = query.Where(x => x.Quantity < supplyItemResourceParameters.QuantityLessThan);
            }

            if (supplyItemResourceParameters.QuantityGreaterThan is not null)
            {
                query = query.Where(x => x.Quantity > supplyItemResourceParameters.QuantityGreaterThan);
            }
            if (supplyItemResourceParameters.OrderBy is not null)
            {
                switch (supplyItemResourceParameters.OrderBy)
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
                    case "supplyid":
                        query = query.OrderBy(x => x.SupplyId); break;
                    case "supplyiddesc":
                        query = query.OrderByDescending(x => x.SupplyId); break;
                }
            }

            var supplyItems = query.ToPaginatedList(supplyItemResourceParameters.PageSize, supplyItemResourceParameters.PageNumber);

            var supplyItemDto = _mapper.Map<List<SupplyItemDto>>(supplyItems);

            return new PaginatedList<SupplyItemDto>(supplyItemDto, supplyItems.TotalCount, supplyItems.CurrentPage, supplyItems.PageSize);
        }

        public SupplyItemDto? GetSupplyItemById(int id)
        {
            var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);

            var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItem);

            return supplyItemDto;
        }

        public SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate)
        {
            var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToCreate);

            _context.SupplyItems.Add(supplyItemEntity);
            _context.SaveChanges();

            var supplyItemDto = _mapper.Map<SupplyItemDto>(supplyItemEntity);

            return supplyItemDto;
        }

        public void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate)
        {

            var supplyItemEntity = _mapper.Map<SupplyItem>(supplyItemToUpdate);

            _context.SupplyItems.Update(supplyItemEntity);
            _context.SaveChanges();
        }

        public void DeleteSupplyItem(int id)
        {
            var supplyItem = _context.SupplyItems.FirstOrDefault(x => x.Id == id);
            if (supplyItem is not null)
            {
                _context.SupplyItems.Remove(supplyItem);
            }
            _context.SaveChanges();
        }
    }
}
