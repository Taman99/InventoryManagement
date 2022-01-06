using CategoryService.Entities;

namespace CategoryService.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int categoryId);

        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);

        bool DeleteCategory(int categoryId);

        bool CategoryExists(int categoryId);
    }
}
