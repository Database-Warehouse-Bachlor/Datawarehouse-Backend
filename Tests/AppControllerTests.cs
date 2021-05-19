using System;
using System.Collections.Generic;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


/*
*   This is a testclass for testing methods in the AppController class.
*   It is unit tests written using Xunit. To be able to create mock-data
*   the Moq framework has been used. 
*    
*   Methods applied with [Fact] above is the actual tests, while the other methods are used to
*   Create testobjects and data to use in these tests. 
*   This class is adapted from  
*       - https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api
*       - https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0
*/
namespace Datawarehouse_Backend.Tests
{
    public class AppControllerTest
    {
        /*
        *   A test that checks that the response is correct
        */
        [Fact]
        public void getNumberOfTennantsAndErrorsAsJson()
        {
            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
        
            var controller = new AppController(mockData.Object);

            // Result is the method that is being tested
            var result = controller.getNumberOfTennantsAndErrorsAsJson();
            
            var viewResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, viewResult.StatusCode);
        }

        /*
        *   A test that checks that the correct number is returned
        */

        /*[Fact]
        public void getNumberOfTennants_ShouldReturnNumberOfTennants()
        {
            //Given
            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getNumberOfTennants()).Returns(TestDataWhenIntExpected());

            var controller = new AppController(mockData.Object);

            //When
            // Result is the method that is being tested
            var result = controller.getNumberOfTennants();

            //Then
            var viewResult = Assert.IsType<int>(result);
            Assert.Equal(2, viewResult);
            Assert.NotEqual(1, viewResult);
        */

        /*
        *   A test that checks that the correct number is returned
        */
        /*
        [Fact]
        public void getNumberOfErrorsLastTwentyFour_ShouldReturnInt()
        {
            //Given
            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(e => e.getNumberOfErrorsLastTwentyFour()).Returns(TestDataWhenIntExpected());

            var controller = new AppController(mockData.Object);

            //When
            // Result is the method that is being tested
            var result = controller.getNumberOfErrorsLastTwentyFour();

            //Then
            var viewResult = Assert.IsType<int>(result);
            Assert.Equal(2, viewResult);
        }
        */

        /*
        *   A test that checks if the list is as long as it should and that it contains the correct information
        */
        [Fact]
        public void GetAllTennantsAsList_ShouldCountListAndRetrieveCorrectListInfo()
        {
            // Expected result    
            var expected = tennant();

            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getAllTennants()).Returns(TestDataForAllTennants());

            var controller = new AppController(mockData.Object);
            // Result is the method that is being tested
            var result = controller.getAllTennants();
            var viewResult = Assert.IsType<List<Tennant>>(result);

            Assert.Equal(expected.ToString(), viewResult[0].ToString());
            Assert.Equal(4, viewResult.Count);
        }

        /*
        *   A test that checks errors last 24 hours. Checkks that it contains the correct information
        */
        [Fact]
        public void getLatestErrors_ShouldGetCorrectListTest()
        {
            //Given
            DateTime datetimeNow = DateTime.Now;
            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(e => e.getLatestErrors()).Returns(TestDataErrors());

            var controller = new AppController(mockData.Object);
            //When
            // Result is the method that is being tested
            var result = controller.getLatestErrors();

            //Then
            var viewResult = Assert.IsType<List<ErrorLog>>(result);

            Assert.InRange(viewResult[1].timeOfError, DateTime.Now.AddHours(-24), DateTime.Now);
        }

        /*
        *   A test that checks if the list contains the correct information
        */
        [Fact]
        public void getAllErrors_ShouldReturnList()
        {
             // Expected result
            var expected = errorLog();

            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockData = new Mock<IWarehouseContext>();
            mockData.Setup(t => t.getAllErrors()).Returns(TestDataErrors());

            var controller = new AppController(mockData.Object);
            
            // Result is the method that is being tested
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