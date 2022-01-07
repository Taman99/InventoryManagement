using CategoryService.Controllers;
using CategoryService.Entities;
using CategoryService.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System;
using Moq;

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
    }
}