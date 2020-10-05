using DAL;
using Domain;
using Moq;
using NUnit.Framework;
using ServiceLayer;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProjectServiceLayer
{
    public class Tests
    {
        private Mock<IDiveRepository> _diverepositoryMock;
        private DivingService _divingService;
        private List<Dive> _Dives;
        [SetUp]
        public void Setup()
        {
            _diverepositoryMock = new Mock<IDiveRepository>();
            _divingService = new DivingService(_diverepositoryMock.Object);

            _Dives = new List<Dive>();
            Dive Diveone = new Dive(1, "Cyprus", 15, 30);
            Dive Divetwo = new Dive(2, "Crete", 17, 100);

            _Dives = new List<Dive>();

            _Dives.Add(Diveone);
            _Dives.Add(Divetwo);
        }

        [Test]
        public void Calling_GetAll_ON_ServiceLayer_Should_Call_DiveRepo_and_Return_all_dives()
        {
            //Arrange
            _diverepositoryMock.Setup(m => m.GetAll()).Returns(_Dives);

            //act
            IEnumerable<Dive> result = _divingService.GetAll();
           

            //Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<Dive>))); 

            //Check that the GetAll method was called once
            _diverepositoryMock.Verify(c => c.GetAll(), Times.Once);
        }

        [TearDown]
        public void TestCleanUp()
        {
            _diverepositoryMock = null;
            _Dives = null;
        }
    }
}