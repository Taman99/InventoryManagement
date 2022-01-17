using SizesService.Context;
using SizesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace SizesService.Repository
{
    public class SizesRepository : ISizesRepository
    {
        private readonly InventoryManagementContext _context;

        public SizesRepository(InventoryManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Size> GetSizes()
        {
            var sizes = _context.Sizes.ToList();
            return sizes;
        }

        public bool CreateSize(Size size)
        {
            _context.Add(size);
            return Commit();
        }

        public bool UpdateSize(Size size)
        {
            _context.Entry(size).State = EntityState.Modified;
            return Commit();
        }

        //This method only deletes one record i.e size
        public bool DeleteSizeBySizeId(int sizeId)
        {
            var size = _context.Sizes.FirstOrDefault(size => size.SizeId == sizeId);
            if (size != null)
            {
                _context.Sizes.Remove(size);
            }
            return Commit();

        }

        //This method deletes multiple sizes records having same product id
        public bool DeleteSizesByProductId(string productId)
        {
            _context.Sizes.RemoveRange(_context.Sizes.Where(size => size.ProductId == productId));
          
            return Commit();
        }

        //This method will return one product having multiple sizes
        public IEnumerable<Size> GetSizeByProductId(string productId)
        {
            var size = _context.Sizes.FromSqlRaw($"SELECT * FROM Sizes WHERE ProductId={productId}").ToList();

            if (size != null)
            {
                return size;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public bool SizeExists(int sizeId)
        {
            return _context.Sizes.Any(size => size.SizeId == sizeId);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
