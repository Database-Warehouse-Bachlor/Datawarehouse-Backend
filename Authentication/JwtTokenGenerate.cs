
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

    public string generateJSONWebToken(User userinfo, IConfiguration config)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
             new Claim(JwtRegisteredClaimNames.Sub,userinfo.orgNr),
             new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
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
    public User authenticateUser(User login)
    {
        User user = null;
        if (login.orgNr == "1234" && login.password == "admin")
        {
            user = new User { orgNr = "1234", Email = "tenant@mail.com", password = "admin" };
        }
        return user;
    }
}
}
