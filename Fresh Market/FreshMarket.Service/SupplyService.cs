using AutoMapper;
using FreshMarket.Domain.DTOs.Supply;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;

namespace FreshMarket.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;

        public SupplyService(IMapper mapper, FreshMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetSuppliesResponse GetSupplies(SupplyResourceParameters supplyResourceParameters)
        {
            var query = _context.Supplies.AsQueryable();

            if (supplyResourceParameters.DateTime is not null)
            {
                query = query.Where(x => x.SupplyDate == supplyResourceParameters.DateTime);
            }

            if (supplyResourceParameters.SupplierId is not null)
            {
                query = query.Where(x => x.SupplierId == supplyResourceParameters.SupplierId);
            }

            if (supplyResourceParameters.OrderBy is not null)
            {
                switch (supplyResourceParameters.OrderBy)
                {
                    case "supplydate":
                        query = query.OrderBy(x => x.SupplyDate); break;
                    case "supplydatedesc":
                        query = query.OrderByDescending(x => x.SupplyDate); break;
                    case "supplierid":
                        query = query.OrderBy(x => x.SupplierId); break;
                    case "supplieriddesc":
                        query = query.OrderByDescending(x => x.SupplierId); break;
                }
            }

            var supplies = query.ToPaginatedList(supplyResourceParameters.PageSize, supplyResourceParameters.PageNumber);

            var supplyDtos = _mapper.Map<List<SupplyDto>>(supplies);

            var paginatedResult = new PaginatedList<SupplyDto>(supplyDtos, supplies.TotalCount, supplies.CurrentPage, supplies.PageSize);

            var result = new GetSuppliesResponse()
            {
                Data = paginatedResult.ToList(),
                HasNextPage = paginatedResult.NextPage,
                HasPreviousPage = paginatedResult.PreviosPage,
                PageNumber = paginatedResult.CurrentPage,
                PageSize = paginatedResult.PageSize,
                TotalPages = paginatedResult.TotalPage
            };
            return result;
        }

        public SupplyDto? GetSupplyById(int id)
        {
            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);

            var supplyDto = _mapper.Map<SupplyDto>(supply);

            return supplyDto;
        }

        public SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate)
        {
            var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

            _context.Supplies.Add(supplyEntity);
            _context.SaveChanges();

            var supplyDto = _mapper.Map<SupplyDto>(supplyEntity);

            return supplyDto;
        }

        public void UpdateSupply(SupplyForUpdateDto supplyToUpdate)
        {
            var supplyEntity = _mapper.Map<Supply>(supplyToUpdate);

            _context.Supplies.Update(supplyEntity);
            _context.SaveChanges();
        }

        public void DeleteSupply(int id)
        {
            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);
            if (supply is not null)
            {
                _context.Supplies.Remove(supply);
            }
            _context.SaveChanges();
        }
    }
}
