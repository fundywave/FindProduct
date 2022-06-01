using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using FindProduct.API;
using FindProduct.API.Controllers;
using FindProduct.Data.Enums;
using FindProduct.Data.Models;
using FindProduct.Data.Response;
using FindProduct.Services;
using FindProduct.Services.Interfaces;
using FindProduct.Services.Provider;
using FindProduct.Services.Warehouses;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProductSearch.Tests
{
    public class Tests
    {
        ProductController _controller;
        Mock<IWarehouseService> _warehouseServiceMock;
        [SetUp]
        public void Setup()
        {
            _warehouseServiceMock=new Mock<IWarehouseService>();
            _controller = new ProductController(_warehouseServiceMock.Object);
        }

        [Test]
        public void Should_Get_Correct_Object()
        {
                //arrange
                var response = new CustomApiResponse<ProductSearchResponse>()
                {
                    Result = new ProductSearchResponse
                    {
                        Product = new Product
                        {
                            Name = "Test",
                            DeliveryMethods = new List<DeliveryMethods>
                            {
                                DeliveryMethods.Car, DeliveryMethods.Plane
                            }
                        },
                        WarehouseName = "Warehouse1"
                    },
                    StatusCode = HttpStatusCode.OK,
                    Message = "Test1"
                };
                _warehouseServiceMock.Setup(x => x.FindProduct("Test")).Returns(Task.Run(()=>response));
                //act
                var mockResult = JsonConvert.SerializeObject(response, new StringEnumConverter());
                var expected =((ContentResult)new ProductController(_warehouseServiceMock.Object).Get("Test")).Content;

                //assert
                Assert.AreEqual(mockResult, expected);
        }
    }
}