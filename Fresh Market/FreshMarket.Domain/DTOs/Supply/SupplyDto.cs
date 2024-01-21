using FreshMarket.Domain.DTOs.SupplyItem;

namespace FreshMarket.Domain.DTOs.Supply
{
    public record SupplyDto(
        int Id,
        DateTime SupplyDate,
        int SupplierId,
        ICollection<SupplyItemDto> SupplyItems);
}
