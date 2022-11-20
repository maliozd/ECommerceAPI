using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.FileEntities;

namespace ECommerceAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<ProductImageFile>? ProductImageFiles { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}
