using FreshMarket.Domain.DTOs.Sale;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Pagination.PaginatedList;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        GetSalesReponse GetSales(SaleResourceParameters saleResourceParameters);
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
