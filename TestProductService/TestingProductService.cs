using Moq;
using NUnit.Framework;
using FluentAssertions;
using ProductService.Repository;
using System.Collections.Generic;
using ProductService.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ProductService.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestProductService
{
    public class TestingProductService
    {
        private Mock<IProductRepository> _mockRepo;
        private DefaultHttpContext _httpContext;
        private string _userId = "google-oauth2|105207773114799152868";
        private string _email = "abc@gmail.com";

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            var httpContext = new DefaultHttpContext();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim("https://example.com/email", "abc@gmail.com"),
                                   }, "TestingAPI"));

            httpContext.Request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlpJS1dOc3RJVDNrVU03UDE2d3JOSyJ9.eyJodHRwczovL2V4YW1wbGUuY29tL2VtYWlsIjoidHNpbmdoMTUyMDFAZ21haWwuY29tIiwiaXNzIjoiaHR0cHM6Ly9kZXYtdGFwcC51cy5hdXRoMC5jb20vIiwic3ViIjoiZ29vZ2xlLW9hdXRoMnwxMDUyMDc3NzMxMTQ3OTkxNTI4NjgiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI2MSIsImh0dHBzOi8vZGV2LXRhcHAudXMuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTY0MjQzNTc5OSwiZXhwIjoxNjQyNDQyOTk5LCJhenAiOiJhVHRyNU5GY1FGSGdRQzNVUThRb3Y1ZUtmUzFsb1Y3eSIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.NGxwHFVYQ1KetbCgtTxpBbZ3FHrh_7_m5FgRqdQ3mxlpzKdAuCLn5xIELfEGaJn2iUjWBw9eQpLXJrylAuiFL37usnVkXB9uL3KxfXSEFDY8bbeWj0uXUepM2SByMvVqIm0wZyOTI7_gMu3_oD5lcMR0-SH5rFRmwnU-61D_Q9W24GmlabRANxjqImq6-Gae1enI729a9De8XzocajEB4K9f67u_sh00AIbFle227JPDzkjY1IU10xWMZ9eHZpe4LYfRef_0TfCuO9x5OuhKvTaX1HJNWA6dRic7D5q86CjXtb2vj0KV7SGnJ_ekZHfCm-rqonZr7kIIjI2bcn6BFg");
            httpContext.User = user;
            _httpContext = httpContext;
        }

        [Test]
        public void GetCategories_returnsListOfProducts()
        {
            //Arrange
            var products = new List<Product>() {
                new Product() { ProductId="Guid1" , ProductName = "Tshirt" , CategoryId=1 },
                new Product() { ProductId="Guid2" , ProductName = "cheese" , CategoryId=2 }
            };

            _mockRepo.Setup(x => x.GetProducts(_userId)).Returns(products);
            var controller = new ProductsController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.GetProducts();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public void CreateProduct_WithProductToCreate_returnsCreatedProduct()
        {
            //Arrange
            var ProductToCreate = new Product() { ProductId = "Guid1", ProductName = "Tshirt", CategoryId = 1 };


            _mockRepo.Setup(x => x.CreateProduct(ProductToCreate, _userId)).Returns(true);
            var controller = new ProductsController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.PostProduct(ProductToCreate);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void UpdateProduct_WithExsitingProduct_returnsNoContent()
        {
            //Arrange        
            var existingProduct = new Product() { ProductId = "Guid1", ProductName = "Tshirt", CategoryId = 1 };

            _mockRepo.Setup(x => x.GetProductById("Guid1")).Returns(existingProduct);

            var categoryId = existingProduct.ProductId;
            var productToUpdate = new Product() { ProductId = "Guid1", ProductName = "cheese", CategoryId = 2 };
            _mockRepo.Setup(x => x.UpdateProduct(productToUpdate)).Returns(true);
            var controller = new ProductsController(_mockRepo.Object);

            //Act
            var result = controller.PutProduct(categoryId, productToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public void DeleteProduct_WithExsitingProduct_returnsNoContent()
        {
            //Arrange        
            var existingProduct = new Product() { ProductId = "Guid1", ProductName = "cheese", CategoryId = 2 };

            _mockRepo.Setup(x => x.GetProductById("Guid1")).Returns(existingProduct);
            var controller = new ProductsController(_mockRepo.Object);

            //Act
            var result = controller.DeleteProduct(existingProduct.ProductId);

            //Assert
            result.Should().BeEquivalentTo(result);
        }

      
    }
}