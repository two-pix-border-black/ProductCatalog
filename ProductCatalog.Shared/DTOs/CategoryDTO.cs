namespace ProductCatalog.Shared.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ChildCategoryDTO> ChildCategories { get; set; }
    }

    public class ChildCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ChildCategoryDTO> ChildCategories { get; set; }
    }

    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    public class CategoryContentDTO
    {
        public string CategoryName { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
