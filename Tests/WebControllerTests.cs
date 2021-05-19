using System;
using System.Collections.Generic;
using System.Security.Claims;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;



/*
*   This is a testclass for testing methods in the WebController class.
*   It is unit tests written using Xunit. To be able to create mock-data
*   the Moq framework has been used. 
*   
*   Methods applied with [Fact] above is the actual tests, while the other methods are used to
*   Create testobjects and data to use in these tests.
*
*   This class is adapted from  
*        - https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api
*        - https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0
*/
namespace Datawarehouse_Backend.Tests
{
    public class WebControllerTests
    {
        /*
        *   This is a test that checks that the method returns expected values.
        *   Claims are adapted from and is used to claim an identity from a token:
        *       - https://gunnarpeipman.com/aspnet-core-test-controller-fake-user/
        */
        [Fact]
        public void getAbsenceRegisterReturnsCorrectInformation()
        {
            //Given values used for testing
            string filter = "lastSevenDays";
            long tennantId = 1;
            DateTime date = DateTime.Now.Date.AddDays(-7);
            
            // Defines a claim that is used when register is trying get a tennant
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim [] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            
            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockWarehouse = new Mock<IWarehouseContext>();
            mockWarehouse.Setup(w => w.getAllAbsenceFromDate(tennantId, date)).Returns(absenceRegistersTestData());
    
            // Creates a WebController object to test
            var controller = new WebController(null, mockWarehouse.Object);

            // A default context provider in unit testing
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext {User = user};
        
            //When
            // Sets result to the method that is being tested
            var result = controller.getAbsenceRegister(filter);
            
            //Then
            // Verifies that the correct type is returned. Variable result is of type AbsenceView and not AbsenceRegister
            Assert.NotEqual(absenceRegistersTestData().ToString(), result.ToString());
            // Verifies that the object recieved by the method is the expected 
            Assert.Equal(absenceViewsTestData().ToString(), result.ToString());
            
        }

        /*
        *   Returns a list of absence dummydata
        */
        private List<AbsenceRegister> absenceRegistersTestData()
        {
            var absenceRegisters = new List<AbsenceRegister> {
                new AbsenceRegister {
                    id = 1,
                    fromDate = DateTime.Now,
                    toDate = DateTime.Now,
                    duration = 2,
                    employeeFK = 1
                },
                new AbsenceRegister {
                    id = 2,
                    fromDate = DateTime.Now,
                    toDate = DateTime.Now,
                    duration = 4,
                    employeeFK = 2

                }
            };
            return absenceRegisters;
        }

        /*
        *   Returns a list of absenceview test data
        */
        private IList<AbsenceView> absenceViewsTestData()
        {
            var absence = new List<AbsenceView> {
                new AbsenceView {
                    year = DateTime.Now.Year,
                    month = DateTime.Now.Month,
                    day = DateTime.Now.Day,
                    weekDay = DateTime.Now.DayOfWeek.ToString(),
                    totalDuration = 6
                },
            };
            return absence;
        }

        /*
        *   Returns a list of absenceview test data for notequal testing purposes
        */
        private IList<AbsenceView> absenceViewsWrongTestData()
        {
            var absence = new List<AbsenceView> {
                new AbsenceView {
                    year = 2018,
                    month = 12,
                    day = 10,
                    weekDay = "Monday",
                    totalDuration = 1
                },
            };
            return absence;
        } 
    }
}