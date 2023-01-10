using ECommerceAPI.Application.Dtos.Product.ProductImage;

namespace ECommerceAPI.Application.Dtos.Product
{
    public class SingleProductWithImageDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public ProductImageDto ProductImage { get; set; }
    }
}
