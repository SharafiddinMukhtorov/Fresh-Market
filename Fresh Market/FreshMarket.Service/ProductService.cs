﻿using AutoMapper;
using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Domain.Responses;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Pagination;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;
using Microsoft.Extensions.Logging;

namespace FreshMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly FreshMarketDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IMapper mapper, FreshMarketDbContext context, ILogger<ProductService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public GetProductsResponse GetProducts(ProductResourceParameters productResourceParameters)
        {
            var query = _context.Products.AsQueryable();

            if (productResourceParameters.CategoryId is not null)
            {
                query = query.Where(x => x.CategoryId == productResourceParameters.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(productResourceParameters.SearchString))
            {
                query = query.Where(x => x.Name.Contains(productResourceParameters.SearchString)
                || x.Description.Contains(productResourceParameters.SearchString));
            }

            if (productResourceParameters.Price is not null)
            {
                query = query.Where(x => x.Price == productResourceParameters.Price);
            }

            if (productResourceParameters.PriceLessThan is not null)
            {
                query = query.Where(x => x.Price < productResourceParameters.PriceLessThan);
            }

            if (productResourceParameters.PriceGreaterThan is not null)
            {
                query = query.Where(x => x.Price > productResourceParameters.PriceGreaterThan);
            }
            if (productResourceParameters.OrderBy is not null)
            {
                switch (productResourceParameters.OrderBy)
                {
                    case "name":
                        query = query.OrderBy(x => x.Name); break;
                    case "namedesc":
                        query = query.OrderByDescending(x => x.Name); break;
                    case "price":
                        query = query.OrderBy(x => x.Price); break;
                    case "pricedesc":
                        query = query.OrderByDescending(x => x.Price); break;
                    case "description":
                        query = query.OrderBy(x => x.Description); break;
                    case "descriptiondesc":
                        query = query.OrderByDescending(x => x.Description); break;
                }
            }

            var products = query.ToPaginatedList(productResourceParameters.PageSize, productResourceParameters.PageNumber);

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var paginatedResult =  new PaginatedList<ProductDto>(productDtos, products.TotalCount, products.CurrentPage, products.PageSize);

            var result = new GetProductsResponse()
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

        public ProductDto? GetProductById(int id)
        {

            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public ProductDto CreateProduct(ProductForCreateDto productToCreate)
        {
            var productEntity = _mapper.Map<Product>(productToCreate);

            _context.Products.Add(productEntity);
            _context.SaveChanges();

            var productDto = _mapper.Map<ProductDto>(productEntity);


            return productDto;
        }

        public void UpdateProduct(ProductForUpdateDto productToUpdate)
        {
            var productEntity = _mapper.Map<Product>(productToUpdate);

            _context.Products.Update(productEntity);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product is not null)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();
        }
    }
}
