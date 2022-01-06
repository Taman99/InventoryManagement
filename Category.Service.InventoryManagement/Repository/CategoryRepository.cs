using Category.Service.Context;
using Category.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Category.Service.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context;
        }

        // Get categories from DB
        public IEnumerable<TblCategory> GetCategories()
        {
            var categories =  _context.Categories.ToList();
            return categories;
        }

        // Get category by id from DB
        public TblCategory GetCategoriesById( int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (category != null)
            {
                return category;              
            }
            throw new NullReferenceException();
        }

        // Create new category in DB
        public bool CreateCategory(TblCategory category)
        {
            _context.Categories.Add(category);
            return Commit();
        }

        // Update category in DB
        public bool UpdateCategory(TblCategory category)
        {
            _context.Entry(category).State = EntityState.Modified;
            return Commit();
        }

        // Delete category from DB
        public bool DeleteCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            return Commit();
        }

        // Category exists or not
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(e => e.CategoryId == categoryId);
        }

        // Save changes to DB
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
