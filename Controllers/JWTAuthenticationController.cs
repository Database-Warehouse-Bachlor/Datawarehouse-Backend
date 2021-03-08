
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
using Datawarehouse_Backend.Context;

namespace Datawarehouse_Backend.Controllers
{
 [Route("api/login")]
 [ApiController]

 public class JWTAuthenticationController : ControllerBase
 {

     private readonly IConfiguration _config;
     private readonly LoginDatabaseContext _db;

     public JWTAuthenticationController(IConfiguration config, LoginDatabaseContext db)
     {

         _config = config;
         _db = db;
     }
    
     [HttpPost]
     public IActionResult login(string email, string pass)
     {
        
        var loginUser = _db.users
        .Where(e => e.Email == email)
        .FirstOrDefault<User>();
        
        Console.WriteLine("USER:" + loginUser.Email);
        IActionResult response;

        if(loginUser.Email != null && loginUser.password == pass) {
        JwtTokenGenerate jwtTokenGenerate = new JwtTokenGenerate();
        var tokenStr = jwtTokenGenerate.generateJSONWebToken(loginUser,_config).ToString();
        response = Ok(tokenStr);
        } else {
         response = Unauthorized();
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