using ProductService.Context;
using ProductService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagementContext _context;

        public ProductRepository(InventoryManagementContext context)
        {
            _context = context;
        }


        public IEnumerable<Product> GetProducts(string userId)
        {
            var products = _context.Products.Where(p => p.MerchantId == userId);
            return products;
        }


        public bool CreateProduct(Product product, string userId)
        {
            product.MerchantId = userId;
            product.ProductId = Guid.NewGuid().ToString();
            _context.Products.Add(product);
            return Commit();
        }
        public bool DeleteProduct(string productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            return Commit();
        }

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

        public bool ProductExists(string productId)
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
