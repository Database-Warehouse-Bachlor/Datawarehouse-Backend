
using System.IdentityModel.Tokens.Jwt;
using Datawarehouse_Backend.Models;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


/*
*   Generates a JWT token with different claims. Claims gives information about the current user.
*   This class is adapter from c-sharpcorner, and customized the claims for our use.
*    - https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
*/
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
             new Claim(JwtRegisteredClaimNames.Sub,userinfo.tennantFK.ToString()),
             new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
             new Claim(ClaimTypes.Role, userinfo.role),
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
}
}
