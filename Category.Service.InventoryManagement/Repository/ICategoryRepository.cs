using Category.Service.Entities;

namespace Category.Service.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<TblCategory> GetCategories();

        TblCategory GetCategoriesById(int categoryId);

        bool CreateCategory(TblCategory category);

        bool UpdateCategory(TblCategory category);

        bool DeleteCategory(int categoryId);

        bool CategoryExists(int categoryId);
    }
}
