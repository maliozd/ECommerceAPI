namespace ECommerceAPI.Application.Dtos.Category
{
    public class SingleCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<string>? ChildCategories { get; set; }
        public string? ParentCategory { get; set; }
        public bool IsParentCategory { get; set; }
    }
}
