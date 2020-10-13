using ConsoleIntro;
using ConsoleIntro.Interfaces;
using Moq;
using NUnit.Framework;

namespace NUnitTestProjectIntro
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIntro()
        {
            //arrange
            var mock = new Mock<IAnimal>();
            mock.Setup(animal => animal.MakesSound()).Returns("woof");

            var animal = new Dog();

            //act
            var sound = animal.MakesSound(); 

            //assert
            Assert.AreEqual(sound, "Woof");
        }
    }
}