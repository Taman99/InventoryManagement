using ProductService.Entities;


namespace ProductService.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string userId);

        Product GetProductById(int productId);

        bool CreateProduct(Product product, string userId);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int productId);


        bool ProductExists(int productId);


    }
}
