using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Dtos.Product
{
    public class SingleProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public CategoryIdNameDto Category { get; set; }
    }
}
