namespace ECommerceAPI.Domain.Entities.FileEntities
{
    public class ProductImageFile : BaseFile
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
