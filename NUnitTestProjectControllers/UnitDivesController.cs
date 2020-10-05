using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceLayer;
using WebApplMoqIntro.Controllers;

namespace NUnitTestProjectControllers
{
    public class Tests
    {
        private DiversController _divesController;

        [SetUp]
        public void Setup()
        {
            var MockDivingService = new Mock<IDivingService>();
            _divesController = new DiversController(MockDivingService.Object);
        }

        [Test]
        public void GetDives_WhenCalled_Should_ReturnsOkResult()
        {
            // Act
            var okResult = _divesController.GetDives();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(okResult);
          //  Assert.IsInstanceOf<OkResult>(okResult);
        }

        [Test]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Dive testItem = new Dive(3, "Karpathos", 25, 200);

            // Act
            var createdResponse = _divesController.Post(testItem);

            // Assert
            Assert.IsInstanceOf<CreatedResult>(createdResponse);
        }


      
            [TearDown]
        public void TestCleanUp()
        {
            _divesController  = null;
        }
    }
}
