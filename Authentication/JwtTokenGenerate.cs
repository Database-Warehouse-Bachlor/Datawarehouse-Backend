
using System.IdentityModel.Tokens.Jwt;
using Datawarehouse_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Datawarehouse_Backend.Authentication
{
     public class JwtTokenGenerate
 {

     public JwtTokenGenerate()
     {

     }

    public string generateJSONWebToken(UserModel userinfo, IConfiguration config)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
             new Claim(JwtRegisteredClaimNames.Sub,userinfo.OrgNum),
             new Claim(JwtRegisteredClaimNames.Email,userinfo.EmailAdress),
             new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
         };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );

        var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodeToken;

    }
    public UserModel authenticateUser(UserModel login)
    {
        UserModel user = null;
        if (login.OrgNum == "1234" && login.Password == "admin")
        {
            user = new UserModel { OrgNum = "1234", EmailAdress = "tenant@mail.com", Password = "admin" };
        }
        return user;
    }
}
}
