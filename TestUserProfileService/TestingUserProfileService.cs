using UserProfileService.Repository;
using Moq;
using NUnit.Framework;
using UserProfileService.Entities;
using UserProfileService.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;

namespace TestUserProfileService
{
    public class TestingUserProfileService
    {
        private Mock<IUserProfileRepository> _mockRepo;
        private DefaultHttpContext _httpContext;
        private string _userId = "google-oauth2|105207773114799152868";
        private string _email = "abc@gmail.com";


        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IUserProfileRepository>();
            var httpContext = new DefaultHttpContext();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim("https://example.com/email", "abc@gmail.com"),
                                   }, "TestingAPI"));

            httpContext.Request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlpJS1dOc3RJVDNrVU03UDE2d3JOSyJ9.eyJodHRwczovL2V4YW1wbGUuY29tL2VtYWlsIjoidHNpbmdoMTUyMDFAZ21haWwuY29tIiwiaXNzIjoiaHR0cHM6Ly9kZXYtdGFwcC51cy5hdXRoMC5jb20vIiwic3ViIjoiZ29vZ2xlLW9hdXRoMnwxMDUyMDc3NzMxMTQ3OTkxNTI4NjgiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI2MSIsImh0dHBzOi8vZGV2LXRhcHAudXMuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTY0MjQzNTc5OSwiZXhwIjoxNjQyNDQyOTk5LCJhenAiOiJhVHRyNU5GY1FGSGdRQzNVUThRb3Y1ZUtmUzFsb1Y3eSIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.NGxwHFVYQ1KetbCgtTxpBbZ3FHrh_7_m5FgRqdQ3mxlpzKdAuCLn5xIELfEGaJn2iUjWBw9eQpLXJrylAuiFL37usnVkXB9uL3KxfXSEFDY8bbeWj0uXUepM2SByMvVqIm0wZyOTI7_gMu3_oD5lcMR0-SH5rFRmwnU-61D_Q9W24GmlabRANxjqImq6-Gae1enI729a9De8XzocajEB4K9f67u_sh00AIbFle227JPDzkjY1IU10xWMZ9eHZpe4LYfRef_0TfCuO9x5OuhKvTaX1HJNWA6dRic7D5q86CjXtb2vj0KV7SGnJ_ekZHfCm-rqonZr7kIIjI2bcn6BFg");
            httpContext.User = user;
            _httpContext = httpContext;
        }

        [Test]
        public void GetUserProfile_WithUserProfile_ReturnsUserProfile()
        { 
            //Arrange
            var userProfile = new UserProfile()
            {
                UserId = "abc"
            };
            _mockRepo.Setup(x => x.GetUserProfileByUserId(_userId, _email)).Returns(userProfile);

            var controller = new UserProfilesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.GetUserProfile();

            //Assert
            result.Value.Should().BeEquivalentTo(userProfile);
        }

        [Test]
        public void UpdateUserProfile_WithUserProfile_ReturnsOkStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = _userId
            };
            _mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Returns(true);

            var controller = new UserProfilesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.UpdateUserProfile(newUserProfile);

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void UpdateUserProfile_WithInvalidUpdate_ReturnsBadRequestStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = _userId
            };
            _mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Returns(false);

            var controller = new UserProfilesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.UpdateUserProfile(newUserProfile);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void UpdateUserProfile_WithException_ReturnsConflictStatus()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = _userId
            };
            _mockRepo.Setup(x => x.UpdateUserProfile(newUserProfile)).Throws(new Exception());

            var controller = new UserProfilesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.UpdateUserProfile(newUserProfile);

            //Assert
            result.Should().BeOfType<ConflictResult>();
        }

        [Test]
        public void CreateUserProfile_WithNewProfile_ReturnsCreatedProfile()
        {
            //Arrange

            var newUserProfile = new UserProfile()
            {
                UserId = _userId
            };
            _mockRepo.Setup(x => x.CreateUserProfile(_userId, _email,newUserProfile)).Returns(true);
            _mockRepo.Setup(x => x.GetUserProfileByUserId(_userId,_email)).Returns(newUserProfile);


            var controller = new UserProfilesController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            //Act
            var result = controller.CreateUserProfile(newUserProfile);

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }
    }
}