using System;
using System.Text;
using Datawarehouse_Backend.Authentication;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Controllers;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;


/*
    This is a testclass for testing methods in the JWTAuthenticationController class.
    It is unit tests written using Xunit. To be able to create mock-data
    the Moq framework has been used. 
    
    Methods applied with [Fact] above is the actual tests, while the other methods are used to
    Create testobjects and data to use in these tests. 
*/
namespace Datawarehouse_Backend.Tests
{
    public class JWTAuthenticationControllerTest
    {

        /*
        * This is a test that checks that the response is the wanted response, when login is succsessful
        */
        [Fact]
        public void loginReturnsTheCorrectResponse()
        {
            // Values needed for the class to be tested
            string email = "someemail@mail.no";
            string password = "somePassword";

            var token = Guid.NewGuid().ToString();
            // Creates Mockdata for the specified class.
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns(token);

            // Creates Mockdata for the specified class.
            var mockLoginDb = new Mock<ILoginDatabaseContext>();
            mockLoginDb.Setup(l => l.findUserByMail(email)).Returns(TestUser());

            // Creates a controller-object to test, warehousedb set to null because it's not used in this method. 
            var controller = new JWTAuthenticationController(mockConfig.Object, mockLoginDb.Object, null);
            // Sets result to the method that is being tested
            var result = controller.login(email, password);

            //Verifies that the object is of a given type
            var viewResult = Assert.IsType<OkObjectResult>(result);
            
            // Verifies that two objects are equal. This checks if the responsecode is 200.
            Assert.Equal(200, viewResult.StatusCode);
        }
        /*
        * Returns a user as dummydata for testing
        */
        private User TestUser()
        {
            var user = new User
            {
                id = 1,
                Email = "someemail@mail.com",
                password = BCrypt.Net.BCrypt.HashPassword("somePassword"),
                tennantFK = 1,
                role = "User"
            };
            return user;
        }

    }


}