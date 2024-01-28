using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Pagination.PaginatedList;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        GetCategoriesResponse GetCategories(CategoryResourceParameters categoryResourceParameters);
        CategoryDto? GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate);
        void UpdateCategory(CategoryForUpdateDto categoryToUpdate);
        void DeleteCategory(int id);
    }
}
