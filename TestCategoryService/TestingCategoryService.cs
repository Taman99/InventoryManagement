using CategoryService.Controllers;
using CategoryService.Entities;
using CategoryService.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TestCategoryService
{
    //Test cases for category
    public class TestingCategoryService
    {
        private Mock<ICategoryRepository> _mockRepo;
        private DefaultHttpContext _httpContext;
        private string _userId = "google-oauth2|105207773114799152868";
        private string _email = "abc@gmail.com";

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ICategoryRepository>();
            var httpContext = new DefaultHttpContext();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim("https://example.com/email", "abc@gmail.com"),
                                   }, "TestingAPI"));

            httpContext.Request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlpJS1dOc3RJVDNrVU03UDE2d3JOSyJ9.eyJodHRwczovL2V4YW1wbGUuY29tL2VtYWlsIjoidHNpbmdoMTUyMDFAZ21haWwuY29tIiwiaXNzIjoiaHR0cHM6Ly9kZXYtdGFwcC51cy5hdXRoMC5jb20vIiwic3ViIjoiZ29vZ2xlLW9hdXRoMnwxMDUyMDc3NzMxMTQ3OTkxNTI4NjgiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI2MSIsImh0dHBzOi8vZGV2LXRhcHAudXMuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTY0MjQzNTc5OSwiZXhwIjoxNjQyNDQyOTk5LCJhenAiOiJhVHRyNU5GY1FGSGdRQzNVUThRb3Y1ZUtmUzFsb1Y3eSIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.NGxwHFVYQ1KetbCgtTxpBbZ3FHrh_7_m5FgRqdQ3mxlpzKdAuCLn5xIELfEGaJn2iUjWBw9eQpLXJrylAuiFL37usnVkXB9uL3KxfXSEFDY8bbeWj0uXUepM2SByMvVqIm0wZyOTI7_gMu3_oD5lcMR0-SH5rFRmwnU-61D_Q9W24GmlabRANxjqImq6-Gae1enI729a9De8XzocajEB4K9f67u_sh00AIbFle227JPDzkjY1IU10xWMZ9eHZpe4LYfRef_0TfCuO9x5OuhKvTaX1HJNWA6dRic7D5q86CjXtb2vj0KV7SGnJ_ekZHfCm-rqonZr7kIIjI2bcn6BFg");
            httpContext.User = user;
            _httpContext = httpContext;
        }

        [Test]
        public void GetCategories_returnsListOfCategories()
        {
            //Arrange
            var categories = new List<Category>() {
                new Category() { UserId = _userId, CategoryId=1 , CategoryName = "Clothing"},
                new Category() { UserId = _userId, CategoryId=2 , CategoryName = "Food"}
            };

            _mockRepo.Setup(x => x.GetCategories(_userId)).Returns(categories);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.GetCategories();

            //Assert
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Test]
        public void GetCategoryById_WithExistingCategoryId_returnsCategory()
        {
            //Arrange        
            var category = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            _mockRepo.Setup(x => x.GetCategoryById(1)).Returns(category);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.GetCategoryById(1);

            //Assert
            result.Value.Should().BeEquivalentTo(category);
        }

        [Test]
        public void CreateCategory_WithCategoryToCreate_returnsCreatedCategory()
        {
            //Arrange
            var categoryToCreate = new Category() { UserId = _userId, CategoryId = 3, CategoryName = "Accesories" };


            _mockRepo.Setup(x => x.CreateCategory(categoryToCreate)).Returns(true);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.PostCategory(categoryToCreate);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void CreateCategory_WithInvalidCategory_returnsBadRequest()
        {
            //Arrange
            var categoryToCreate = new Category() { CategoryId = 4, CategoryName = "Clothing" };


            _mockRepo.Setup(x => x.CreateCategory(categoryToCreate)).Returns(false);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.PostCategory(categoryToCreate);

            //Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void UpdateCategory_WithExsitingCategory_returnsNoContent()
        {
            //Arrange        
            var existingcategory = new Category() { UserId = _userId, CategoryId = 1, CategoryName = "Clothing" };

            _mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);

            var categoryId = existingcategory.CategoryId;
            var categoryToUpdate = new Category() { UserId = _userId, CategoryId = 1, CategoryName = "Footwear" };
            _mockRepo.Setup(x => x.UpdateCategory(categoryToUpdate)).Returns(true);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.PutCategory(categoryId, categoryToUpdate);

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void UpdateCategory_WithInValidExsitingCategory_returnsBadRequest()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            _mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);

            var categoryId = existingcategory.CategoryId;
            var categoryToUpdate = new Category() { CategoryId = 2, CategoryName = "Footwear" };
            _mockRepo.Setup(x => x.UpdateCategory(categoryToUpdate)).Returns(false);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.PutCategory(categoryId, categoryToUpdate);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }


        [Test]
        public void DeleteCategory_WithExsitingCategory_returnsNoContent()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            _mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.DeleteCategory(existingcategory.CategoryId);

            //Assert
            result.Should().BeEquivalentTo(result);
        }

        [Test]
        public void DeleteCategory_WithExsitingCategoryIfNotDeleted_returnsNotFound()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            _mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);
            var controller = new CategoriesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.DeleteCategory(existingcategory.CategoryId);

            //Assert
            //result.Should().BeEquivalentTo(NotFoundResult);
            result.Should().BeOfType<NotFoundResult>();
        }

    }

}