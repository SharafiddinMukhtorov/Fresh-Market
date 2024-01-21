using FreshMarket.Domain.DTOs.Supply;

namespace FreshMarket.Domain.DTOs.Supplier
{
    public record SupplierDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Company,
        ICollection<SupplyDto> Supplies);
}
