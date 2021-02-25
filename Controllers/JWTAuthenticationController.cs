
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Datawarehouse_Backend.Models;
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
     public IActionResult Login(string orgNum, string pass)
     {
        UserModel login = new UserModel();
        login.OrgNum = orgNum;
        login.Password = pass;
        IActionResult response = Unauthorized();

        var user = AuthenticateUser(login);

        if (user != null)
        {
            var tokenStr = GenerateJSONWebToken(user).ToString();
            response = Ok(tokenStr);
        }
        return response;
     }

        private UserModel AuthenticateUser(UserModel login)
     {
         UserModel user = null;
         if(login.OrgNum=="1234" && login.Password=="admin")
         {
             user=new UserModel { OrgNum="1234",EmailAdress="tenant@mail.com",Password="admin"};
         }
         return user;
     }

     private string GenerateJSONWebToken(UserModel userinfo)
     {
         var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
         var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

         var claims = new[]
         {
             new Claim(JwtRegisteredClaimNames.Sub,userinfo.OrgNum),
             new Claim(JwtRegisteredClaimNames.Email,userinfo.EmailAdress),
             new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
         };
         
         var token=new JwtSecurityToken(
             issuer:_config["Jwt:Issuer"],
             audience: _config["Jwt:Issuer"],
             claims,
             expires: DateTime.Now.AddMinutes(120),
             signingCredentials: credentials
         );

         var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
         return encodeToken;
       
     }

    [Authorize]
    [HttpPost("Post")]
     public string Post()
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