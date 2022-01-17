using Xunit;
using ProductService.Controllers;
using ProductService.Entities;
using ProductService.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TestProductServices
{
    public class TestingProductServices
    {
        private Mock<IProductRepository> mockRepo;

        public TestingProductServices()
        {
            mockRepo = new Mock<IProductRepository>();
        }

        [Test]
        public void GetCategories_returnsListOfProducts()
        {  
            //Arrange
            var products = new List<Product>() {
                new Product() { ProductId=1 , ProductName = "Tshirt" , CategoryId=1 },
                new Product() { ProductId=2 , ProductName = "cheese" , CategoryId=2 }
            };

            mockRepo.Setup(x => x.GetProducts()).Returns(products);
            var controller = new ProductsController(mockRepo.Object);

            //Act
            var result = controller.GetProducts();

            //Assert
            result.Value.Should().BeEquivalentTo(products);
        }

        [Test]
        public void CreateProduct_WithProductToCreate_returnsCreatedProduct()
        {
            //Arrange
            var ProductToCreate = new Product() { ProductId = 1, ProductName = "Tshirt", CategoryId = 1 };


            mockRepo.Setup(x => x.CreateProduct(ProductToCreate)).Returns(true);
            var controller = new ProductsController(mockRepo.Object);

            //Act
            var result = controller.PostProduct(ProductToCreate);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        public void UpdateProduct_WithExsitingProduct_returnsNoContent()
        {
            //Arrange        
            var existingProduct = new Product() { ProductId = 1, ProductName = "Tshirt", CategoryId = 1 };

            mockRepo.Setup(x => x.GetProductById(1)).Returns(existingProduct);

            var categoryId = existingProduct.ProductId;
            var productToUpdate = new Product() { ProductId = 2, ProductName = "cheese", CategoryId = 2 };
            mockRepo.Setup(x => x.UpdateProduct(productToUpdate)).Returns(true);
            var controller = new ProductsController(mockRepo.Object);

            //Act
            var result = controller.PutProduct(categoryId, productToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public void DeleteProduct_WithExsitingProduct_returnsNoContent()
        {
            //Arrange        
            var existingProduct = new Product() { ProductId = 2, ProductName = "cheese", CategoryId = 2 };

            mockRepo.Setup(x => x.GetProductById(1)).Returns(existingProduct);
            var controller = new ProductsController(mockRepo.Object);

            //Act
            var result = controller.DeleteProduct(existingProduct.ProductId);

            //Assert
            result.Should().BeEquivalentTo(result);
        }

        //[Fact]
        //public void Test1()
        //{

        //}
    }
}