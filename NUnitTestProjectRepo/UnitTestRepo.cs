using DAL;
using Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProjectRepo
{
    public class Tests
    {
        private List<Dive> _MockLstDives;
        private Mock<IDiveRepository> _DiveMock;
        private IDiveRepository _DiveRepo;

        [SetUp]
        public void Setup()
        {

            Dive Diveone = new Dive(1, "Cyprus", 15, 30);
            Dive Divetwo = new Dive(2, "Crete", 17, 100);

            _MockLstDives = new List<Dive>();

            _MockLstDives.Add(Diveone);
            _MockLstDives.Add(Divetwo);

            _DiveMock = new Mock<IDiveRepository>();

            //All Dives
            _DiveMock.Setup(m => m.GetAll()).Returns(_MockLstDives);

            ///GetByID
            _DiveMock.Setup(m =>
                m.GetSingle(It.Is<int>(i => i == 1 || i == 2 || i == 3 || i == 4))).Returns<int>(r => new Dive
                   {
                       Id = r,
                       Location = string.Format("Fake Dive {0}", r)
                   });

            //Add Dive
            _DiveMock.Setup(m => m.Add(It.IsAny<Dive>())).Callback(new Action<Dive>(
                        x =>
                        {
                            _MockLstDives.Add(x);
                        }
                    ));


            //Delete Dive
            _DiveMock.Setup(x => x.Delete(It.IsAny<Dive>())).Callback(new Action<Dive>(x =>
            {
                _MockLstDives.RemoveAll(d => d.Id == x.Id);
            }
            ));


            //Update Dive
            _DiveMock.Setup(x => x.Update(It.IsAny<Dive>())).Callback(new Action<Dive>(x =>
                {

                    var found = _MockLstDives.Find(c => c.Id == x.Id);
                    found.Location = x.Location;
                }));

            _DiveRepo = _DiveMock.Object;
        }

        [Test]
        public void GetAll_Should_Return_All_MockLstDives()
        {
            //Arrange 

            //Act
            var testDives = (IList<Dive>)_DiveRepo.GetAll();

            //Assert
            Assert.AreEqual(2, testDives.Count);
        }

        [Test]
        public void GetById_Should_Return_Correct_Dive()
        {
            // Arrange

            //Act
            Dive testDive = _DiveRepo.GetSingle(1);

            //Assert
            Assert.AreEqual(1, testDive.Id);
        }

        [Test]
        public void Insert_Should_Return_Increased_MockLstDives()
        {
            // Arrange
            Dive testDive = new Dive(3, "TestDive", 25, 300);

            //Act
            _DiveRepo.Add(testDive);
           var after = (IList<Dive>)_DiveRepo.GetAll();

            //Assert
            Assert.AreEqual(3, after.Count);
        }


        [Test]
        public void Delete_Should_Return_Decreased_MockLstDives()
        {
            // Arrange
            Dive testDive = _MockLstDives.First(i => i.Id == 1);

            //Act
            _DiveRepo.Delete(testDive);

            //Assert
            Assert.AreEqual(1, _MockLstDives.Count);
        }

        [Test]
        public void Update_Should_ChangeDive()
        {
            // Arrange
            Dive testDive = new Dive(2, "Update", 12, 12);

            //Act
            _DiveRepo.Update(testDive);

            //Assert
            Assert.AreEqual("Update", _MockLstDives[1].Location);
        }


        [TearDown]
        public void TestCleanUp()
        {
            _DiveMock = null;
            _MockLstDives = null;
            _DiveRepo = null;
        }
    }
}