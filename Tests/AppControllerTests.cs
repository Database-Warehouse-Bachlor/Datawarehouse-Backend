using System;
using System.Collections.Generic;
using System.Linq;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Datawarehouse_Backend.Tests
{
    public class AppControllerTest
    {

        [Fact]
        public void getNumberOfTennants_ShouldReturnNumberOfTennants()
        {
            //Given
            var mockupDb = new Mock<IWarehouseContext>();
            mockupDb.Setup(t => t.getNumberOfTennants()).Returns(TestDataForNumberOfTennants());

            var controller = new AppController(mockupDb.Object);
            
            //When
            var result = controller.getNumberOfTennants();

            //Then
            var viewResult = Assert.IsType<int>(result);
            Assert.Equal(2, viewResult);
            Assert.NotEqual(1, viewResult);
        }

         [Fact]
          public void GetAllTennantsAsList()
          {
              var mockupDb = new Mock<IWarehouseContext>();
              mockupDb.Setup(t => t.getAllTennants()).Returns(TestDataForAllTennants());

              var controller = new AppController(mockupDb.Object);

              var result = controller._warehouseDb.getAllTennants();
              var viewResult = Assert.IsType<List<Tennant>>(result);

              Assert.Equal(4, viewResult.Count);
          }

        private List<Tennant> TestDataForAllTennants()
        {
            var tennants = new List<Tennant> {
                new Tennant {
                    id = 1,
                    tennantName = "someOne",
                    businessId = "someId",
                    apiKey = "someApiKey"
                },
                         new Tennant {
                    id = 2,
                    tennantName = "someTwo",
                    businessId = "someIdTwo",
                    apiKey = "someApiKeyTwo"
                },
                         new Tennant {
                    id = 3,
                    tennantName = "someThree",
                    businessId = "someIdThree",
                    apiKey = "someApiKeyThree"
                },
                new Tennant {
                    id = 4,
                    tennantName = "someFour",
                    businessId = "someIdFour",
                    apiKey = "someApiKeyFour"
                },
            };
            return tennants;
        }

        private int TestDataForNumberOfTennants()
        {
            int someNumber = 2;
            return someNumber;
        }
    }
}