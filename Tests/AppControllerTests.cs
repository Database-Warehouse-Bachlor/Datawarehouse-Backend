using System;
using System.Collections.Generic;
using System.Linq;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Datawarehouse_Backend.Tests
{
    public class AppControllerTest
    {

        // A test method to check if the getNumberOfTennants() method is doing what it is supposed to do.
        [Fact]
        public void getTennantsTest()
        {

            // Arrange
            var mockData = new Mock<IWarehouseContext>();
            int expected = 4;
            mockData.Setup(mdata => mdata.Tennants.Count()).Returns(getTestData());
            var controller = new AppController(mockData.Object);

            // Act
            var result = controller.getNumberOfTennants();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Tennant>>(viewResult.ViewData.Model);
            Assert.Equal(expected, model.Count());
        }









        // Creates dummydata used for testing purposes
        public int getTestData()
        {
            var tennants = new List<Tennant>() {
            new Tennant {
                id = 1,
                tennantName = "Test One",
                businessId = "123123123",
                apiKey = "923hfna9fhawf"
            },
            new Tennant {
                id = 2,
                tennantName = "Test Two",
                businessId = "345345345",
                apiKey = "4oiugshbjg"
            },
            new Tennant {
                id = 1,
                tennantName = "Test Three",
                businessId = "456456456",
                apiKey = "aeo8hfuebf873"
            },
            new Tennant {
                id = 1,
                tennantName = "Test Four",
                businessId = "567567567",
                apiKey = "3q98fhqufiawhf"
            },
            };
            return tennants.Count();

        }
    }
}