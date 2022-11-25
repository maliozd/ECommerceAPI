namespace ECommerceAPI.Application.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string CategoryId { get; set; }
    }
}
