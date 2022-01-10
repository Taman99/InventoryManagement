using UserProfileService.Repository;
using Moq;
using NUnit.Framework;
using UserProfileService.Entities;
using UserProfileService.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TestUserProfileService
{
    public class TestingUserProfileService
    {
        private Mock<IUserProfileRepository> mockRepo;

        [SetUp]
        public void Setup()
        {
            mockRepo = new Mock<IUserProfileRepository>();
        }

        [Test]
        public void GetUserProfile_WithUserProfile_ReturnsUserProfile()
        {
            //Arrange
            
            var userProfile = new UserProfile()
            {
                UserId = "abc"
            };
            mockRepo.Setup(x => x.GetUserProfileByUserId("abc")).Returns(userProfile);

            var controller = new UserProfilesController(mockRepo.Object);

            //Act
            var result = controller.GetUserProfile("abc");

            //Assert
            result.Value.Should().BeEquivalentTo(userProfile);
        }

        [Test]
        public void UpdateUserProfile_WithUserProfile_ReturnsOkStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = "abc"
            };
            mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Returns(true);

            var controller = new UserProfilesController(mockRepo.Object);

            //Act
            var result = controller.UpdateUserProfile("abc", newUserProfile);

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void UpdateUserProfile_WithInvalidUpdate_ReturnsBadRequestStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = "abc"
            };
            mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Returns(false);

            var controller = new UserProfilesController(mockRepo.Object);

            //Act
            var result = controller.UpdateUserProfile("abc", newUserProfile);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void UpdateUserProfile_WithException_ReturnsConflictStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = "abc"
            };
            mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Throws(new Exception());

            var controller = new UserProfilesController(mockRepo.Object);

            //Act
            var result = controller.UpdateUserProfile("abc", newUserProfile);

            //Assert
            result.Should().BeOfType<ConflictResult>();
        }

        [Test]
        public void CreateUserProfile_WithNewProfile_ReturnsCreatedProfile()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = "abc"
            };
            mockRepo.Setup(x => x.CreateUserProfile(newUserProfile)).Returns(true);
            mockRepo.Setup(x => x.GetUserProfileByUserId("abc")).Returns(newUserProfile);


            var controller = new UserProfilesController(mockRepo.Object);

            //Act
            var result = controller.CreateUserProfile(newUserProfile);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }
    }
}