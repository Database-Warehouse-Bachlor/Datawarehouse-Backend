
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

        if (user != null)
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