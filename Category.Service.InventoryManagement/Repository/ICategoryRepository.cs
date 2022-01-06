using CategoryService.Entities;

namespace CategoryService.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoriesById(int categoryId);

        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);

        bool DeleteCategory(int categoryId);

        bool CategoryExists(int categoryId);
    }
}
