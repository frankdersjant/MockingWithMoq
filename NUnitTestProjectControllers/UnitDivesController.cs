using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceLayer;
using System.Collections.Generic;
using System.Linq;
using WebApplMoqIntro.Controllers;

namespace NUnitTestProjectControllers
{
    public class Tests
    {
        private List<Dive> _MockLstDives;
        private Mock<IDivingService> _MockDivingService;

        [SetUp]
        public void Setup()
        {
            _MockDivingService = new Mock<IDivingService>();

            _MockLstDives = new List<Dive>();

            Dive Diveone = new Dive(1, "Cyprus", 15, 30);
            Dive Divetwo = new Dive(2, "Crete", 17, 100);
            _MockLstDives.Add(Diveone);
            _MockLstDives.Add(Divetwo);

            _MockDivingService.Setup(r => r.GetAll()).Returns(_MockLstDives);

            _MockDivingService.Setup(g => g.GetDive(It.Is<int>(i => i == 1))).Returns(_MockLstDives.FirstOrDefault(c => c.Id == 1));
        }

        [Test]
        public void GetDives_WhenCalled_Should_ReturnTypeOfDives()
        {
            //Arrange 
            DiversController DivesController = new DiversController(_MockDivingService.Object);

            // Act
            var resultDives = DivesController.GetDives();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Dive>>(resultDives);
            Assert.IsNotNull(resultDives);
            CollectionAssert.AreEqual(resultDives, _MockLstDives);
        }

        [Test]
        public void GetDivesPart2_WhenCalled_Should_Return_TypeOf_Of_OkResult()
        {
            //Arrange 
             DiversController DivesController = new DiversController(_MockDivingService.Object);

            // Act
            var resultDives = DivesController.GetDivesPart2();
            var OkResult = resultDives as OkObjectResult;


            // Assert
            Assert.IsNotNull(resultDives);
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(OkResult.Value, _MockLstDives);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void GetDive_Should_Return_Found_Dive()
        {
            //Arrange
            DiversController DivesController = new DiversController(_MockDivingService.Object);

            //Act
            var resultDives = DivesController.GetDive(1);
            var OkResult = resultDives as OkObjectResult;

            Assert.IsNotNull(resultDives);
            Assert.AreEqual(200, OkResult.StatusCode);
            Assert.AreEqual(OkResult.Value, _MockLstDives[0]);
        }

        [Test]
        public void GetDive_Should_Return_NotFound_Dive_With_Invalid_Id()
        {
            //Arrange
            DiversController _MockLstDivesController = new DiversController(_MockDivingService.Object);

            //Act
            var resultDives = _MockLstDivesController.GetDive(3);
            var NotOkResult = resultDives as NotFoundResult;

            Assert.AreEqual(404, NotOkResult.StatusCode);
        }

        [Test]
        public void Post_ValidDive_Should_Return_CreatedResponse()
        {
            // Arrange
            Dive testItem = new Dive(3, "Karpathos", 25, 200);
            DiversController DivesController = new DiversController(_MockDivingService.Object);

            // Act
            IActionResult createdResponse = DivesController.Post(testItem);
            var OkCreated = createdResponse as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(createdResponse);
            Assert.AreEqual(201, OkCreated.StatusCode);
            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }

        [Test]
        public void Post_InValidDive_Should_Return_BadRequest()
        {
            // Arrange
            Dive testItem = new Dive(3, "Karpathos", -25, 200);
            DiversController DivesController = new DiversController(_MockDivingService.Object);
            DivesController.ModelState.AddModelError("divedepth", "Cannot have a negative value for depth");

            // Act
            IActionResult createdResponse = DivesController.Post(testItem);
            var NotOkCreated = createdResponse as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(createdResponse);
            Assert.AreEqual(400, NotOkCreated.StatusCode);
            Assert.IsInstanceOf<BadRequestObjectResult>(createdResponse);
        }

        [TearDown]
        public void TestCleanUp()
        {
            _MockDivingService = null; 
        }
    }
}
