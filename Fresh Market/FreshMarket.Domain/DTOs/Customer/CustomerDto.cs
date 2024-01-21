using FreshMarket.Domain.DTOs.Sale;

namespace FreshMarket.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        ICollection<SaleDto> Sales);
}
