using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.DTOs.Supply;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Pagination.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        PaginatedList<SupplyDto> GetSupplies(SupplyResourceParameters supplyResourceParameters);
        SupplyDto? GetSupplyById(int id);
        SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate);
        void UpdateSupply(SupplyForUpdateDto supplyToUpdate);
        void DeleteSupply(int id);
    }
}
