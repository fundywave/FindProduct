using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FindProduct.Data.Enums;
using FindProduct.Data.Models;
using FindProduct.Data.Response;
using FindProduct.Services;
using FindProduct.Services.Interfaces;
using FindProduct.Services.Warehouses;
using Moq;

namespace ProductSearch.Tests
{
    public class ServiceTests
    {
        IWarehouseService _warehouseService;
        Mock<IDataProvider<Warehouse>> _providerMock;
        Mock<IWarehouseHttpClient<ProductSearchResponse>> _httpclientMock;

        [SetUp]
        public void Setup()
        {
            _httpclientMock = new Mock<IWarehouseHttpClient<ProductSearchResponse>>();
            _providerMock = new Mock<IDataProvider<Warehouse>>();
            _warehouseService = new WarehouseService(_providerMock.Object, _httpclientMock.Object);
        }

        [Test]
        public async Task Should_Get_BadRequest_From_Service()
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

            _providerMock.Setup(x => x.Get()).Returns(warehouses);

            //act
            var expected = await _warehouseService.FindProduct(It.IsAny<string>());

            //assert
            Assert.AreEqual(expected.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Get_Correct_Result_From_Service()
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
            _httpclientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.Run(() =>
                new ProductSearchResponse
                {
                    Product = new Product
                    {
                        Name = "Test",
                        DeliveryMethods = new List<DeliveryMethods>
                        {
                            DeliveryMethods.Car,
                            DeliveryMethods.Plane
                        }
                    },
                    WarehouseName = "WarehouseTest"
                }
            ));
            _providerMock.Setup(x => x.Get()).Returns(warehouses);

            //act
            var expected = await _warehouseService.FindProduct("test");

            //assert
            Assert.AreEqual(expected.Result.Product.Name, "Test");
            Assert.AreEqual(expected.Result.WarehouseName, "WarehouseTest");
            Assert.AreEqual(expected.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_Get_ProductNotFound_Result_From_Service()
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
            _httpclientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.Run(() =>
                new ProductSearchResponse
                {
                    Product = null,
                    WarehouseName = string.Empty
                }
            ));
            _providerMock.Setup(x => x.Get()).Returns(warehouses);

            //act
            var expected = await _warehouseService.FindProduct("test");

            //assert
            Assert.IsNull(expected.Result.Product);
            Assert.AreEqual(expected.StatusCode, HttpStatusCode.NotFound);
        }
    }
}