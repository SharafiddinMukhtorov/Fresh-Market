using AutoMapper;
using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.Pagination;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace FreshMarket.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<SaleService> _logger;

        public SaleService(IMapper mapper, FreshMarketDbContext context, ILogger<SaleService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<SaleDto> GetSales(SaleResourceParameters saleResourceParameters)
        {
            var query = _context.Sales.AsQueryable();

            if (saleResourceParameters.CustomerId is not null)
            {
                query = query.Where(x => x.CustomerId == saleResourceParameters.CustomerId);
            }
            if (saleResourceParameters.OrderBy is not null)
            {
                switch (saleResourceParameters.OrderBy)
                {
                    case "saledate":
                        query = query.OrderBy(x => x.SaleDate); break;
                    case "saledatedesc":
                        query = query.OrderByDescending(x => x.SaleDate); break;
                    case "customerid":
                        query = query.OrderBy(x => x.CustomerId); break;
                    case "customeriddesc":
                        query = query.OrderByDescending(x => x.CustomerId); break;
                }
            }

            // var sales = query.ToPaginatedList(saleResourceParameters.PageSize, saleResourceParameters.PageNumber);
            var sales = query.ToList();

            var salesDto = _mapper.Map<List<SaleDto>>(sales);

            return new PaginatedList<SaleDto>(salesDto, 0, 0, 0);
        }

        public SaleDto? GetSaleById(int id)
        {
            try
            {
                var sale = _context.Sales.FirstOrDefault(x => x.Id == id);

                var saleDto = _mapper.Map<SaleDto>(sale);

                return saleDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching sale with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching sale with id: {id}.", ex.Message);
                throw;
            }
        }

        public SaleDto CreateSale(SaleForCreateDto saleToCreate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToCreate);

                _context.Sales.Add(saleEntity);
                _context.SaveChanges();

                var saleDto = _mapper.Map<SaleDto>(saleEntity);

                return saleDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new sale.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new sale.", ex.Message);
                throw;
            }
        }

        public void UpdateSale(SaleForUpdateDto saleToUpdate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToUpdate);

                _context.Sales.Update(saleEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Sale and SaleDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating sale.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating sale.", ex.Message);
                throw;
            }
        }

        public void DeleteSale(int id)
        {
            try
            {
                var sale = _context.Sales.FirstOrDefault(x => x.Id == id);
                if (sale is not null)
                {
                    _context.Sales.Remove(sale);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting sale with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting sale with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
