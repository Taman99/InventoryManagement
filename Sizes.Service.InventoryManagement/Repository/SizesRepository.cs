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

        public bool DeleteSize(int sizeId)
        {
            var size = _context.Sizes.First(size => size.SizeId == sizeId);
            if (size != null)
            {
                _context.Sizes.Remove(size);
            }
            return Commit();

        }

        //only one record will get deleted
        //different approach must be used to delete multiple records having same product id
        public bool DeleteSizesByProductId(int productId)
        {
            var size = _context.Sizes.FirstOrDefault(size => size.ProductId == productId);
            if (size != null)
            {
                _context.Sizes.Remove(size);
            }
            return Commit();
        }

        //this will also show one record
        public Size GetSizeByProductId(int productId)
        {
            var size = _context.Sizes.FirstOrDefault(size => size.ProductId == productId);
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
