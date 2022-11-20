using ECommerceAPI.Application.Dtos.Product;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstraction.Services.Product
{
    public interface IProductService
    {
        public Task<bool> CreateProductAsync(CreateProductDto productDto);
        public Task<bool> UpdateProductAsync(UpdateProductDto productDto);
        public Task<bool> DeleteProductAsync(string id);
        public Task<bool> UploadProductImageFileAsync(string productId, IFormFileCollection files);
        public Task<bool> DeleteProductImageFileAsync(string productId, string imageId);
        public Task<bool> ChangeProductImageShowcaseImageAsync(string productId,string imageId);
    }
}
