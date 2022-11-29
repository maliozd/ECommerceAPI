using ECommerceAPI.Application.Dtos.Category;
using ECommerceAPI.Domain.Entities.FileEntities;

namespace ECommerceAPI.Application.Dtos.Product
{
    public class SingleProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public CategoryIdNameDto Category { get; set; }
    }
}
