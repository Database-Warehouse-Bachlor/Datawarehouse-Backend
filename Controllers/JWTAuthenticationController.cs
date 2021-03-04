
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
using Microsoft.AspNetCore.Identity;

namespace Datawarehouse_Backend.Controllers
{
 [Route("api/login")]
 [ApiController]

 public class JWTAuthenticationController : ControllerBase
 {
     private readonly UserManager<User> userManager;
     private readonly RoleManager<User> roleManager;
     private readonly IConfiguration _config;

     public JWTAuthenticationController(IConfiguration config, UserManager<User> userManager, RoleManager<User> roleManager)
     {
         this.userManager = userManager;
         this.roleManager = roleManager;
         _config = config;
     }
    
     [HttpPost]
     public IActionResult login(string orgNum, string pass)
     {
        User login = new User();
        JwtTokenGenerate jwtTokenGenerate = new JwtTokenGenerate();
        login.orgNr = orgNum;
        login.password = pass;
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
 }

}