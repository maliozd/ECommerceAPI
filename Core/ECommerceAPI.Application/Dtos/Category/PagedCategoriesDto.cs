namespace ECommerceAPI.Application.Dtos.Category
{
    public class PagedCategoriesDto
    {
        public int TotalCount { get; set; }
        public ICollection<SingleCategoryDto> Categories { get; set; }
    }
}
