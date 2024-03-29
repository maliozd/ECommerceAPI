﻿using ECommerceAPI.Application.Abstraction.Hubs;
using ECommerceAPI.Application.Abstraction.Services.Category;
using ECommerceAPI.Application.Abstraction.Services.Product;
using ECommerceAPI.Application.Abstraction.Storage;
using ECommerceAPI.Application.Dtos.Category;
using ECommerceAPI.Application.Dtos.Product;
using ECommerceAPI.Application.Dtos.Product.ProductImage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using ECommerceAPI.Domain.Entities.FileEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services.Product
{
    public class ProductService : IProductService
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IStorageService _storageService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductHubService _productHubService;
        readonly ICategoryService _categoryService;

        public ProductService(IProductWriteRepository productWriteRepository, IProductHubService productHubService, IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, ICategoryService categoryService, IProductImageFileReadRepository productImageFileReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _categoryService = categoryService;
            _productImageFileReadRepository = productImageFileReadRepository;
        }

        public async Task<SingleProductDto> GetProductByIdAsync(string productId)
        {
            var product = await _productReadRepository.GetByIdAsync(Guid.Parse(productId));
            var productCategory = await _categoryService.GetCategoryIdNameByIdAsync(product.CategoryId.ToString());
            return new()
            {
                Id = product.Id.ToString(),
                Stock = product.Stock,
                Price = product.Price,
                Name = product.Name,
                Category = productCategory,
            };
        }
        public async Task<PagedProductsDto> GetAllProductsPagedAsync(int page, int size)
        {
            return new()
            {
                TotalCount = _productReadRepository.GetAll().Count(),
                Products = _productReadRepository.GetAll().Include(x => x.Category).Skip(page * size).Take(size).Select(p => new SingleProductDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Stock = p.Stock,
                    CreatedDate = p.CreatedDate,
                    Category = new CategoryIdNameDto
                    {
                        Id = p.Category.Id.ToString(),
                        Name = p.Category.Name
                    },
                    ProductImage = p.ProductImageFiles.Where(x => x.Showcase == true).Select(pi => new ProductImageDto { Id = pi.Id.ToString(), FileName = pi.FileName, Showcase = pi.Showcase, Path = pi.Path, }).FirstOrDefault(),
                    Price = p.Price,
                }).ToList()
            };
        }
        public async Task<bool> CreateProductAsync(CreateProductDto productDto)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Stock = productDto.Stock,
                Price = productDto.Price,
                CategoryId = Guid.Parse(productDto.CategoryId)
            });
            var response = await _productWriteRepository.SaveAsync();
            if (response > 0)
            {
                await _productHubService.ProductAddedMessageAsync($"{productDto.Name} successfully added. --Sent by SignalR--");
                return true;
            }
            else
                return false;
        }
        public async Task<bool> DeleteProductAsync(string id)
        {
            await _productWriteRepository.Remove(id);
            var productName = (await _productReadRepository.GetByIdAsync(Guid.Parse(id))).Name;

            var response = await _productWriteRepository.SaveAsync();
            if (response > 0)
            {
                await _productHubService.ProductRemovedMessageAsync($"{productName} successfully removed from database. --Sent by SignalR--");
                return true;
            }
            else
                return false;
        }
        public async Task<bool> UpdateProductAsync(UpdateProductDto productDto) //TODO refactor this and repository update func
        {
            var productToUpdate = await _productReadRepository.GetByIdAsync(Guid.Parse(productDto.Id));
            productToUpdate.Name = productDto.Name;
            productToUpdate.Stock = productDto.Stock;
            productToUpdate.Price = productDto.Price;
            productToUpdate.CategoryId = Guid.Parse(productDto.CategoryId);
            var response = await _productWriteRepository.SaveAsync();
            return response > 0 ? true : false;
        }
        public async Task<bool> UploadProductImageFileAsync(string productId, IFormFileCollection files)
        {
            List<(string fileName, string pathOrContainer)> result = await _storageService.UploadAsync("photo-images", files);
            var product = await _productReadRepository.GetByIdAsync(Guid.Parse(productId));
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(p => new Domain.Entities.FileEntities.ProductImageFile
            {
                FileName = p.fileName,
                Path = p.pathOrContainer,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>()
                {
                    product
                }
            }).ToList());
            var response = await _productImageFileWriteRepository.SaveAsync();
            return response > 0 ? true : false;
        }
        public async Task<bool> DeleteProductImageFileAsync(string productId, string imageId)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(productId));
            ProductImageFile? productImageFile = product?.ProductImageFiles?.FirstOrDefault(pif => pif.Id == Guid.Parse(imageId));
            if (productImageFile != null)
            {
                product.ProductImageFiles.Remove(productImageFile);
                _productImageFileWriteRepository.Remove(productImageFile);
                await _productWriteRepository.SaveAsync();
                return true;
            }
            else
                return false;
        }
        public async Task<bool> ChangeProductImageShowcaseImageAsync(string productId, string imageId)
        {
            var product = await _productReadRepository.Table.Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(productId));
            var images = product?.ProductImageFiles;
            foreach (var image in images)
            {
                if (image.Showcase == true)
                    image.Showcase = false;
                if (image.Id == Guid.Parse(imageId))
                    image.Showcase = true;
            }
            await _productImageFileWriteRepository.SaveAsync();
            return true;
        }


    }
}
