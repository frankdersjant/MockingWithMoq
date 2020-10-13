using ConsoleAppDALMoq.DAL;
using Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProjectDAL
{
    public class Tests
    {
        private List<Customer> _MockLstCustomer;
        private Customer _Findcustomer;
        private int _falseId = 3332;

        [SetUp]
        public void Setup()
        {
            _MockLstCustomer = new List<Customer>();
            Customer customer = new Customer();
            customer.Id = 1;
            customer.LN = "Pieterse";


            _MockLstCustomer.Add(customer);

            _Findcustomer = new Customer();
            _Findcustomer.Id = 2;
            _Findcustomer.LN = "van den Berg";

            _MockLstCustomer.Add(_Findcustomer);
        }

        [Test]
        public void GetAll_Should_Return_List_Customers()
        {
            //arrange
            var dalMock = new Mock<IDAL>();
            dalMock.Setup(x => x.GetAll()).Returns(_MockLstCustomer);

            //act
            var mockedDal = dalMock.Object;
            var list = mockedDal.GetAll();

            //assert
            Assert.AreEqual(2, _MockLstCustomer.Count);
        }

        [Test]
        public void FindByID_Should_Return_a_Customer()
        {
            //arrange
            var dalMock = new Mock<IDAL>();
            dalMock.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int s) => _MockLstCustomer.Where(x => x.Id == s).Single());

            //act
            var mockedDal = dalMock.Object;
            Customer customer = mockedDal.FindById(2);

            //assert
            Assert.IsNotNull(customer);
            Assert.IsInstanceOf(typeof(Customer), customer);
            Assert.AreEqual(2, customer.Id);
        }

        [Test]
        public void FindByID_With_InvalidID_Should_Return_a_NULL()
        {
            //arrange
            var dalMock = new Mock<IDAL>();
            dalMock.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int s) => _MockLstCustomer.Find(x => x.Id == s));

            //act
            var mockedDal = dalMock.Object;
            Customer customer = mockedDal.FindById(_falseId);

            //assert
            Assert.IsNull(customer);
        }

        [TearDown]
        public void TestCleanUp()
        {
            _MockLstCustomer = null;
            _Findcustomer = null;
        }
    }
}