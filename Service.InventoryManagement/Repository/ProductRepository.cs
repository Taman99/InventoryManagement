using ProductService.Context;
using ProductService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagementContext _context;

        public ProductRepository(InventoryManagementContext context)
        {
            _context = context;
        }


        public IEnumerable<Product> GetProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }


        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return Commit();
        }
        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            return Commit();
        }

        public Product GetProductById(int productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(product => product.ProductId == productId);
        }

        public bool UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return Commit();
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
