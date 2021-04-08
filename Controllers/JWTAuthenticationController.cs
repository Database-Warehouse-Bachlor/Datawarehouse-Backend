
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Datawarehouse_Backend.Controllers
{
 [Route("api/[controller]")]
 [ApiController]

 public class JWTAuthenticationController : ControllerBase
 {
     private IConfiguration _config;

     public JWTAuthenticationController(IConfiguration config)
     {
         _config = config;
     }

     [HttpPost]
     public IActionResult login(string orgNum, string pass)
     {
        UserModel login = new UserModel();
        JwtTokenGenerate jwtTokenGenerate = new JwtTokenGenerate();
        login.OrgNum = orgNum;
        login.Password = pass;
        IActionResult response = Unauthorized();


        var user = jwtTokenGenerate.authenticateUser(login);

            IActionResult response;
            try
            {
                if (loginUser.Email != null && BCrypt.Net.BCrypt.Verify(pwd, loginUser.password))
                {
                    JwtTokenGenerate jwtTokenGenerate = new JwtTokenGenerate();
                    var tokenStr = jwtTokenGenerate.generateJSONWebToken(loginUser, _config).ToString();
                    response = Ok(tokenStr);
                }
                else
                {
                    response = Unauthorized();
                }

                //Sets response to Unauthorized if the user is not registered in the database
            }
            catch (NullReferenceException)
            {
                response = Unauthorized();
            }


            return response;
        }

        /*
        This function registers users based on orgnr, email and pwd.  First it checks if there's allready an user with this email, if there is one it wont register a new.
        If there's no users with this email, it will find the tennant based on the orgnr given, then create a new user with the given email and connect it to the tennant based on the orgnr.
        Then hashes and salts the password and stores it in the DB. 
        */
        // TODO: Authorize must be implemented at some point
        [Authorize]
        [HttpPost("register")]
        public IActionResult register([FromForm] string businessId, [FromForm] string email, [FromForm] string pwd)
        {
            var tokenStr = jwtTokenGenerate.generateJSONWebToken(user,_config).ToString();
            response = Ok(tokenStr);
        }
        return response;
     }


    [Authorize]
    [HttpPost("Post")]
     public string post()
     {
         var identity = HttpContext.User.Identity as ClaimsIdentity;
         IList<Claim> claim = identity.Claims.ToList();
         var orgNum = claim[0].Value;
         return "Welcome to: " + orgNum;
     }

    [Authorize]
    [HttpGet ("GetValue")]
     public ActionResult<IEnumerable<String>> Get()
         {
             return new string[] { "Value1","value2","value" };
         }
     

 }

}