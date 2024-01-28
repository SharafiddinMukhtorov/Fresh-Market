using AutoMapper;
using FreshMarket.Domain.DTOs.Category;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Exceptions;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.ResourceParameters;
using FreshMarket.Domain.Responses;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using Microsoft.Extensions.Logging;

namespace FreshMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper,
            ILogger<CategoryService> logger,
            FreshMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public GetCategoriesResponse GetCategories(CategoryResourceParameters categoryResourceParameters)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryResourceParameters.SearchString))
            {
                query = query.Where(x => x.Name.Contains(categoryResourceParameters.SearchString));
            }
            if (categoryResourceParameters.OrderBy is not null)
            {
                switch (categoryResourceParameters.OrderBy)
                {
                    case "name":
                        query = query.OrderBy(x => x.Name); break;
                    case "namedesc":
                        query = query.OrderByDescending(x => x.Name); break;
                }
            }
            var categories = query.ToPaginatedList(categoryResourceParameters.PageSize, categoryResourceParameters.PageNumber);

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            var paginatedResult = new PaginatedList<CategoryDto>(categoriesDto, categories.TotalCount, categories.CurrentPage, categories.PageSize);

            var result = new GetCategoriesResponse()
            {
                Data = paginatedResult.ToList(),
                HasNextPage = paginatedResult.NextPage,
                HasPreviousPage = paginatedResult.PreviosPage,
                PageNumber = paginatedResult.CurrentPage,
                PageSize = paginatedResult.PageSize,
                TotalPages = paginatedResult.TotalPage
            };
            return result;
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
            {
                throw new EntityNotFoundException($"Category with id: {id} not found");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToCreate);

            _context.Categories.Add(categoryEntity);

            _context.SaveChanges();

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public void UpdateCategory(CategoryForUpdateDto categoryToUpdate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToUpdate);

            _context.Categories.Update(categoryEntity);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category is not null)
            {
                _context.Categories.Remove(category);
            }

            _context.SaveChanges();
        }
    }
}
