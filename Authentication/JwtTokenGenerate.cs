
using System.IdentityModel.Tokens.Jwt;
using Datawarehouse_Backend.Models;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


/*
*   Generates a JWT token with different claims. Claims gives information about the current user.
*   TODO: This is currently adapted from, but we're not sure about the real origin of this.
*       - https://stackoverflow.com/questions/61296262/c-sharp-jwt-token-persist-claims-after-update
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
