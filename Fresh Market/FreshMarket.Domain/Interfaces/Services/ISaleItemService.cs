using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.DTOs.SaleItem;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Pagination.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        GetSaleItemsResponse GetSaleItems(SaleItemResourceParameters saleItemResourceParameters);
        SaleItemDto? GetSaleItemById(int id);
        SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate);
        void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate);
        void DeleteSaleItem(int id);
    }
}
