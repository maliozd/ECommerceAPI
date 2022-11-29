using ECommerceAPI.Application.Dtos.Product;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstraction.Services.Product
{
    public interface IProductService
    {
        Task<PagedProductsDto> GetAllProductsPagedAsync(int page, int size);

        Task<SingleProductDto> GetProductByIdAsync(string productId);
        Task<bool> CreateProductAsync(CreateProductDto productDto);
        Task<bool> UpdateProductAsync(UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(string id);
        Task<bool> UploadProductImageFileAsync(string productId, IFormFileCollection files);
        Task<bool> DeleteProductImageFileAsync(string productId, string imageId);
        Task<bool> ChangeProductImageShowcaseImageAsync(string productId, string imageId);
    }
}
