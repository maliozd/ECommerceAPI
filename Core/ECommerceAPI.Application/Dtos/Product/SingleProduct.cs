using ECommerceAPI.Domain.Entities.FileEntities;

namespace ECommerceAPI.Application.Dtos.Product
{
    public class SingleProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public string Category { get; set; }
    }
}
