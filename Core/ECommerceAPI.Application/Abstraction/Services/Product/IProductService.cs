using ECommerceAPI.Application.Dtos.Product;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstraction.Services.Product
{
    public interface IProductService
    {
        public Task<bool> CreateProductAsync(CreateProductDto productDto);
        public Task<bool> UpdateProductAsync(UpdateProductDto productDto);
        public Task<bool> DeleteProductAsync(int id);
        public Task<bool> UploadProductImageFileAsync(int productId, IFormFileCollection files);
        public Task<bool> DeleteProductImageFileAsync(int productId, int imageId);
        public Task<bool> ChangeProductImageShowcaseImageAsync(int productId,int imageId);
    }
}
