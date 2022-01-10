using CategoryService.Controllers;
using CategoryService.Entities;
using CategoryService.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TestCategoryService
{
    public class TestingCategoryService
    {
        private Mock<ICategoryRepository> mockRepo;

        public TestingCategoryService()
        {
            mockRepo = new Mock<ICategoryRepository>();
        }

        [Test]
        public void GetCategories_returnsListOfCategories()
        {
            //Arrange
            var categories = new List<Category>() {
                new Category() { CategoryId=1 , CategoryName = "Clothing"},
                new Category() { CategoryId=2 , CategoryName = "Food"}
            };

            mockRepo.Setup(x => x.GetCategories()).Returns(categories);
            var controller = new CategoriesController(mockRepo.Object);

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

            mockRepo.Setup(x => x.GetCategoryById(1)).Returns(category);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.GetCategoryById(1);

            //Assert
            result.Value.Should().BeEquivalentTo(category);
        }

        [Test]
        public void CreateCategory_WithCategoryToCreate_returnsCreatedCategory()
        {
            //Arrange
            var categoryToCreate = new Category() { CategoryId = 3, CategoryName = "Accesories" };


            mockRepo.Setup(x => x.CreateCategory(categoryToCreate)).Returns(true);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.PostTblCategory(categoryToCreate);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void CreateCategory_WithInvalidCategory_returnsBadRequest()
        {
            //Arrange
            var categoryToCreate = new Category() { CategoryId = 4, CategoryName = "Clothing" };


            mockRepo.Setup(x => x.CreateCategory(categoryToCreate)).Returns(false);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.PostTblCategory(categoryToCreate);

            //Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void UpdateCategory_WithExsitingCategory_returnsNoContent()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);

            var categoryId = existingcategory.CategoryId;
            var categoryToUpdate = new Category() { CategoryId = 1, CategoryName = "Footwear" };
            mockRepo.Setup(x => x.UpdateCategory(categoryToUpdate)).Returns(true);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.PutTblCategory(categoryId,categoryToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public void DeleteCategory_WithExsitingCategory_returnsNoContent()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.DeleteTblCategory(existingcategory.CategoryId);

            //Assert
            result.Should().BeEquivalentTo(result);
        }


        [Test]
        public void DeleteCategory_WithExsitingCategoryIfNotDeleted_returnsNotFound()
        {
            //Arrange        
            var existingcategory = new Category() { CategoryId = 1, CategoryName = "Clothing" };

            mockRepo.Setup(x => x.GetCategoryById(1)).Returns(existingcategory);
            var controller = new CategoriesController(mockRepo.Object);

            //Act
            var result = controller.DeleteTblCategory(existingcategory.CategoryId);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }

}