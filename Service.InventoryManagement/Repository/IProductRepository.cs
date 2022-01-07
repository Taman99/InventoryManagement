using ProductService.Entities;


namespace ProductService.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProductById(int productId);

        bool CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int productId);


        bool ProductExists(int productId);


    }
}
