using IMServices.Entities;

namespace IMServices.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories( string userId);

        Category GetCategoryById(int categoryId);

        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);

        bool DeleteCategory(int categoryId);

        bool CategoryExists(int categoryId);
    }
}
