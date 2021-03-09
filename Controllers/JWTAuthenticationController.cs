
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
 [Route("api/")]
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
    
        
     [HttpPost("login")]
     [Consumes("application/x-www-form-urlencoded")] //funker også uten,
     public IActionResult login([FromForm]string email, [FromForm]string pwd)
     {
        
        var loginUser = _db.users
        .Where(e => e.Email == email)
        .FirstOrDefault<User>();
        
        IActionResult response;

        try {
            if(loginUser.Email != null && loginUser.password == pwd) {
        JwtTokenGenerate jwtTokenGenerate = new JwtTokenGenerate();
        var tokenStr = jwtTokenGenerate.generateJSONWebToken(loginUser,_config).ToString();
        response = Ok(tokenStr);
        } else {
         response = Unauthorized();
        }

            //Sets response to Unauthorized if the user is not registered in the database
        } catch(NullReferenceException) {
            response = Unauthorized();
        }

        
        return response;
     }

    /* [Authorize]
    [HttpPut("AddUser")]
    public IActionResult register(string orgnr, string email, string pw){
        
        var userCheck = _db.users
        .Where(e => e.Email == email)
        .FirstOrDefault<User>();
        if(userCheck == null) {
            User newUser = new User(orgnr, email, pw);


        }
        IActionResult response;

        return response;
    } */


    
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