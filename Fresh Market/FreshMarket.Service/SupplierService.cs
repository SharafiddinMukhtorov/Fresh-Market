using AutoMapper;
using FreshMarket.Domain.DTOs.Supplier;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.Extensions.Logging;

namespace FreshMarket.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(IMapper mapper, FreshMarketDbContext context, ILogger<SupplierService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<SupplierDto> GetSuppliers(SupplierResourceParameters supplierResourceParameters)
        {
            var query = _context.Suppliers.AsQueryable();


            if (!string.IsNullOrWhiteSpace(supplierResourceParameters.SearchString))
            {
                query = query.Where(x => x.FirstName.Contains(supplierResourceParameters.SearchString)
                || x.LastName.Contains(supplierResourceParameters.SearchString)
                || x.PhoneNumber.Contains(supplierResourceParameters.SearchString)
                || x.Company.Contains(supplierResourceParameters.SearchString));
            }

            if (supplierResourceParameters.OrderBy is not null)
            {
                switch (supplierResourceParameters.OrderBy)
                {
                    case "firstname":
                        query = query.OrderBy(x => x.FirstName); break;
                    case "firstnamedesc":
                        query = query.OrderByDescending(x => x.FirstName); break;
                    case "lastname":
                        query = query.OrderBy(x => x.LastName); break;
                    case "lastnamedesc":
                        query = query.OrderByDescending(x => x.LastName); break;
                    case "company":
                        query = query.OrderBy(x => x.Company); break;
                    case "companydesc":
                        query = query.OrderByDescending(x => x.Company); break;
                    case "phonenumber":
                        query = query.OrderBy(x => x.PhoneNumber); break;
                    case "phonenumberdesc":
                        query = query.OrderByDescending(x => x.PhoneNumber); break;
                }
            }

            var suppliers = query.ToPaginatedList(supplierResourceParameters.PageSize, supplierResourceParameters.PageNumber);

            var supplierDtos = _mapper.Map<List<SupplierDto>>(suppliers);

            return new PaginatedList<SupplierDto>(supplierDtos, suppliers.TotalCount, suppliers.CurrentPage, suppliers.PageSize);
        }

        public SupplierDto? GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);

            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            return supplierDto;
        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

            _context.Suppliers.Add(supplierEntity);
            _context.SaveChanges();

            var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

            return supplierDto;
        }

        public void UpdateSupplier(SupplierForUpdateDto supplierToUpdate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

            _context.Suppliers.Update(supplierEntity);
            _context.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
            }
            _context.SaveChanges();
        }
    }
}
