using System;
using System.Security.Claims;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;


/*
*   This is a testclass for testing methods in the JWTAuthenticationController class.
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
    public class JWTAuthenticationControllerTest
    {
        /*
        *   This is a test that checks that the response is the wanted response with the login method
        */
        [Fact]
        public void loginReturnsExpectedResponse()
        {
            // Values needed for the class to be tested
            string email = "someemail@mail.no";
            string password = "somePassword";
            var token = Guid.NewGuid().ToString();

            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns(token);

            var mockLoginDb = new Mock<ILoginDatabaseContext>();
            mockLoginDb.Setup(l => l.findUserByMail(email)).Returns(TestUser());

            // Creates a controller-object to test, warehousedb set to null because it's not used in this method. 
            var controller = new JWTAuthenticationController(mockConfig.Object, mockLoginDb.Object, null);
            // Sets result to the method that is being tested
            var resultOk = controller.login(email, password);

            // changes tennantId to be able to retrieve unauthorized result
            password = "someNewPassword";
            var resultUnAuth = controller.login(email, password);

            //Verifies that the object is of a given type
            var viewResultOk = Assert.IsType<OkObjectResult>(resultOk);
            var viewResultUnAuth = Assert.IsType<UnauthorizedResult>(resultUnAuth);

            // Verifies that two objects are equal. This checks if the responsecode is 200.
            Assert.Equal(200, viewResultOk.StatusCode);
            Assert.Equal(401, viewResultUnAuth.StatusCode);
        }
        
        /*
        *   This is a test that checks that the response is the wanted response with the register method
        *   Claims are adapted from and is used to claim an identity from a token:
        *       - https://gunnarpeipman.com/aspnet-core-test-controller-fake-user/ 
        */
        [Fact]
        public void registerReturnsExpectedResponse()
        {
            // Values needed for the class to be tested
            string email = "someEmail@mail.no";
            string password = "somePassword";
            long tennantFK = 1;

            // Defines a claim that is used when register is trying get a tennant
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim [] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            // Creates Mockdata and sets it to return wanted outcome used for testing.            
            var mockLogin = new Mock<ILoginDatabaseContext>();
            mockLogin.Setup(l => l.Users.ToString()).Returns(TestUser().ToString());
                 
            var mockWarehouse = new Mock<IWarehouseContext>();
            mockWarehouse.Setup(w => w.findTennantById(tennantFK)).Returns(TestTennant());
            
            // Creates a controller-object to test, warehousedb set to null because it's not used in this method. 
            var controller = new JWTAuthenticationController(null, mockLogin.Object, mockWarehouse.Object);
            
            // A default context provider in unit testing
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext {User = user};
            
            // Sets result to the method that is being tested
            var result = controller.register(email, password);
           
            //Verifies that the object is of a given type
            var viewResult = Assert.IsType<OkObjectResult>(result);
            // Verifies that two objects are equal. This checks if the responsecode is 200.
            Assert.Equal(200, viewResult.StatusCode);

        }

        /*
        *   This is a test that checks that the response is the wanted response with the initregister method
        */
        [Fact]
        public void initRegisterReturnsExpectedResponse()
        {

            // Values needed for the class to be tested
            string email = "someEmail@mail.no";
            string password = "somePassword";
            long tennantId = 1;

            // Creates Mockdata and sets it to return wanted outcome used for testing.
            var mockLogin = new Mock<ILoginDatabaseContext>();
            mockLogin.Setup(l => l.Users.ToString()).Returns(TestUser().ToString());

            var mockWarehouse = new Mock<IWarehouseContext>();
            mockWarehouse.Setup(w => w.findTennantById(tennantId)).Returns(TestTennant());

            var controller = new JWTAuthenticationController(null, mockLogin.Object, mockWarehouse.Object);

            // Sets result to the method that is being tested
            var resultOk = controller.initRegister(email, password, tennantId);

            // changes tennantId to be able to retrieve a bad request.
            tennantId = 2;
            var resultBadRequest = controller.initRegister(email, password, tennantId);

            //Verifies that the object is of a given type
            var viewResultOk = Assert.IsType<OkObjectResult>(resultOk);
            var viewResultBadRequest = Assert.IsType<BadRequestObjectResult>(resultBadRequest);
            // Verifies that two objects are equal. This checks if the responsecode is 200 and 400.
            Assert.Equal(200, viewResultOk.StatusCode);
            Assert.Equal(400, viewResultBadRequest.StatusCode);
        }




        /*
        *   Returns a user as dummydata for testing
        */
        private User TestUser()
        {
            var user = new User
            {
                id = 1,
                Email = "someemail@mail.com",
                password = BCrypt.Net.BCrypt.HashPassword("somePassword"),
                tennant = TestTennant(),
                tennantFK = 1,
                role = "User"
            };
            return user;
        }

        /*
        *   Returns a tennant as dummydata for testing
        */
        private Tennant TestTennant()
        {
            var tennant = new Tennant
            {
                id = 1,
                tennantName = "someTennant",
                businessId = "someId",
                apiKey = "someApiKey"
            };
            return tennant;
        }

        private long TestTennantId()
        {
            int number = 1;
            return number;
        }

    }


}