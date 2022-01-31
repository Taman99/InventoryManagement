using IMServices.Entities;


namespace IMServices.Repository
{
    //Repository Pattern Interface for API Implementation
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string userId);

        Product GetProductById(string productId);

        bool CreateProduct(Product product, string userId);

        bool UpdateProduct(Product product);

        bool DeleteProduct(string productId);

        bool ProductExists(string productId);

    }
}
