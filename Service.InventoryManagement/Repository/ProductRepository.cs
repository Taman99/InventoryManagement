using ProductService.Context;
using ProductService.Entities;using Microsoft.EntityFrameworkCore;

namespace ProductService.Repository
{
    //Implementing Interface on Repository
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagementContext _context;


        //Context Declaration
        public ProductRepository(InventoryManagementContext context)
        {
            _context = context;
        }

        //Function for Getting product according to userId
        public IEnumerable<Product> GetProducts(string userId)
        {
            var products = _context.Products.Where(p => p.MerchantId == userId);
            return products;
        }

        //Function for Creating product
        public bool CreateProduct(Product product, string userId)
        {
            product.MerchantId = userId;
            product.ProductId = Guid.NewGuid().ToString();
            _context.Products.Add(product);
            return Commit();
        }

        //Function for Deleting Products
        public bool DeleteProduct(string productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            return Commit();
        }

        //Function for Getting product according to product ID
        public Product GetProductById(string productId)
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

        //Function to check if product Exists 
        public bool ProductExists(string productId)
        {
            return _context.Products.Any(product => product.ProductId == productId);
        }

        //Function for updating products details
        public bool UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return Commit();
        }

        //Function for returning Commit
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
