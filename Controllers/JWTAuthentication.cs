
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Datawarehouse_Backend.Controllers
{
 [Route("api/[controller")]
 [ApiController]

 public class JWTAuthentication : ControllerBase
 {
     private IConfiguration _config;

     public LoginController(IConfiguration config)
     {
         _config = config;
     }

     [HttpGet]
     public IActionResult Login(string username, string pass)
     {
        UserModel login = new UserModel();
        login.OrgNum = username;
        login.Password = pass;
        IActionResult response = Unauthorized();

        var user = AuthenticateUser(login);

        if (user != null)
        {
            var tokenStr = GenerateJSONWebToken(user);
            response = Ok(new { tokern = tokenStr});
            
        }
     }

        private object GenerateJSONWebToken(UserModel user)
        {
            throw new System.NotImplementedException();
        }

        private UserModel AuthenticateUser(UserModel login)
     {
         UserModel user = null;
         if(login.OrgNum=="1234" && login.Password=="admin")
         {
             user=new UserModel { OrgNum="1234",EmailAdress="tenant@mail.com",Password="admin"}
         }
         return user;
     }



 }

}