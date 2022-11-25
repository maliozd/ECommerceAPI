namespace ECommerceAPI.Application.Dtos.Product
{
    public class PagedProducts
    {
        public int TotalCount { get; set; }
        public List<SingleProduct> Products { get; set; }
    }
}
