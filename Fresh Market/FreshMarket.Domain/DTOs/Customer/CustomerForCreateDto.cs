﻿namespace FreshMarket.Domain.DTOs.Customer
{
    public record CustomerForCreateDto(
        string FirstName,
        string LastName,
        string PhoneNumber);
}
