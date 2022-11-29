namespace ECommerceAPI.Application.Dtos.Product
{
    public class PagedProductsDto
    {
        public int TotalCount { get; set; }
        public List<SingleProductDto> Products { get; set; }
    }
}
