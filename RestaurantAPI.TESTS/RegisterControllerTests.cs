using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantAPI.Controllers;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.TESTS
{
    public class RegisterControllerTests
    {
        private AccountController _controller;
        private Mock<IAccountService> _accountServiceMock;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);
        }

        [Test]
        public void Login_ReturnsOkResult_WhenGivenValidCredentials()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "password" };
            var token = "some_token";
            _accountServiceMock.Setup(s => s.GenerateJwt(loginDto)).Returns(token);

            // Act
            var result = _controller.Login(loginDto);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.InstanceOf<string>());
            string? resultToken = okResult.Value as string;
            Assert.That(resultToken, Is.EqualTo(token));
        }
    }
}