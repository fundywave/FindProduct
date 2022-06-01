using System.Collections.Generic;
using System.Linq;
using FindProduct.Services.Interfaces;
using FindProduct.Services.Warehouses;
using Moq;
using NUnit.Framework;

namespace FindProduct.Tests
{
    public class ProviderTests
    {
        Mock<IDataProvider<IWarehouse>> _providerMock;

        [SetUp]
        public void Setup()
        {
            _providerMock = new Mock<IDataProvider<IWarehouse>>();
        }
        [Test]
        public void Should_Get_Correct_Result_From_Provider()
        {
            //arrange
            var warehouses = new List<Warehouse>()
            {
                new Warehouse()
                {
                    Url = "url1",
                    QueryParameter = "Param1",
                    DistanceKm = 5
                },
                new Warehouse()
                {
                    Url = "url2",
                    QueryParameter = "Param2",
                    DistanceKm = 10
                }
            };

            //act
            _providerMock.Setup(x => x.Get()).Returns(warehouses);
            var result = _providerMock.Object.Get();
            //assert
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.First().QueryParameter, warehouses.First().QueryParameter);
            Assert.AreEqual(result.First().Url, warehouses.First().Url);
            Assert.Greater(result.Last().DistanceKm, warehouses.First().DistanceKm);

            Assert.AreEqual(result, warehouses);
        }
    }
    
}
