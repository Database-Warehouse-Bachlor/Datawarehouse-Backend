using System;
using System.Collections.Generic;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


/*
    This is a testclass for testing methods in the AppController class.
    It is unit tests written using Xunit. To be able to create mock-data
    the Moq framework has been used. 
    
    Methods applied with [Fact] above is the actual tests, while the other methods are used to
    Create testobjects and data to use in these tests. 
*/
namespace Datawarehouse_Backend.Tests
{
    public class AppControllerTest
    {

        [Fact]
        public void getNumberOfTennantsAndErrorsAsJson()
        {
            var mockData = new Mock<IWarehouseContext>();
        
            var controller = new AppController(mockData.Object);

            var result = controller.getNumberOfTennantsAndErrorsAsJson();
            
            var viewResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, viewResult.StatusCode);
        }

        [Fact]
        public void getNumberOfTennants_ShouldReturnNumberOfTennants()
        {
            //Given
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getNumberOfTennants()).Returns(TestDataWhenIntExpected());

            var controller = new AppController(mockData.Object);

            //When
            var result = controller.getNumberOfTennants();

            //Then
            var viewResult = Assert.IsType<int>(result);
            Assert.Equal(2, viewResult);
            Assert.NotEqual(1, viewResult);
        }

        /*
            Test for getNumberOfErrorsLastTwentyFour();
        */
        [Fact]
        public void getNumberOfErrorsLastTwentyFour_ShouldReturnInt()
        {
            //Given
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(e => e.getNumberOfErrorsLastTwentyFour()).Returns(TestDataWhenIntExpected());

            var controller = new AppController(mockData.Object);

            //When
            var result = controller.getNumberOfErrorsLastTwentyFour();

            //Then
            var viewResult = Assert.IsType<int>(result);
            Assert.Equal(2, viewResult);
        }

        [Fact]
        public void GetAllTennantsAsList_ShouldCountListAndRetrieveCorrectListInfo()
        {

            var expected = tennant();
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getAllTennants()).Returns(TestDataForAllTennants());

            var controller = new AppController(mockData.Object);

            var result = controller.getAllTennants();
            var viewResult = Assert.IsType<List<Tennant>>(result);

            Assert.Equal(expected.ToString(), viewResult[0].ToString());
            Assert.Equal(4, viewResult.Count);
        }

        [Fact]
        public void getLatestErrors_ShouldGetCorrectListTest()
        {
            //Given
            DateTime datetimeNow = DateTime.Now;
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(e => e.getLatestErrors()).Returns(TestDataErrors());

            var controller = new AppController(mockData.Object);
            //When
            var result = controller.getLatestErrors();

            //Then
            var viewResult = Assert.IsType<List<ErrorLog>>(result);

            Assert.InRange(viewResult[1].timeOfError, DateTime.Now.AddHours(-24), DateTime.Now);
        }

        [Fact]
        public void getAllErrors_ShouldReturnList()
        {
            var expected = errorLog();
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getAllErrors()).Returns(TestDataErrors());

            var controller = new AppController(mockData.Object);

            var result = controller.getAllErrors();
            var viewResult = Assert.IsType<List<ErrorLog>>(result);

            Assert.Equal(expected.ToString(), viewResult[0].ToString());
            Assert.Equal(2, viewResult.Count);
        }


        /*
           Returns a list of Errors as dummydata
        */
        private List<ErrorLog> TestDataErrors()
        {
            var errors = new List<ErrorLog> {
                new ErrorLog {
                    id = 1,
                    errorMessage = "someErrorOne",
                    errorType = "someErrorTypeOne",
                    timeOfError = DateTime.Now.Date.AddDays(-1)
                },
                new ErrorLog {
                    id = 2,
                    errorMessage = "someErrorTwo",
                    errorType = "someErrorTypeTwo",
                    timeOfError = DateTime.Now.AddHours(-6)
                },
            };
            return errors;
        }

        /*
         Returns a list of tennants as dummydata
        */
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

        /*
        Creates a Errorlog object and returns it
        */
        private ErrorLog errorLog()
        {
            ErrorLog errorLog = new ErrorLog
            {
                id = 1,
                errorMessage = "someErrorOne",
                errorType = "someErrorTypeOne",
                timeOfError = DateTime.Now.Date.AddDays(-1)
            };
            return errorLog;
        }

        /*
         Creates a tennant object and returns it
        */
        private Tennant tennant()
        {
            Tennant tennant = new Tennant
            {
                id = 1,
                tennantName = "someOne",
                businessId = "someId",
                apiKey = "someApiKey"
            };
            return tennant;
        }

        /*
           Returns an int that is used to test methods which needs a returning int. 
        */
        private int TestDataWhenIntExpected()
        {
            int someNumber = 2;
            return someNumber;
        }
    }
}