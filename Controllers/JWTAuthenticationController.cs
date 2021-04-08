
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
using System.Security.Cryptography;

namespace Datawarehouse_Backend.Controllers
{
    [Route("auth/")]
    [ApiController]

    public class JWTAuthenticationController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly LoginDatabaseContext _db;
        private readonly WarehouseContext _warehousedb;

        public JWTAuthenticationController(IConfiguration config, LoginDatabaseContext db, WarehouseContext warehousedb)
        {
            _config = config;
            _db = db;
            _warehousedb = warehousedb;
        }


        [HttpPost("login")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult login([FromForm] string email, [FromForm] string pwd)
        {
            var loginUser = _db.users
            .Where(e => e.Email == email)
            .FirstOrDefault<User>();

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
        //[Authorize]
        [HttpPost("register")]
        public IActionResult register([FromForm] string businessId, [FromForm] string email, [FromForm] string pwd)
        {
            IActionResult response;
            var userCheck = _db.users
            .Where(e => e.Email == email)
            .FirstOrDefault<User>();

            var tennant = _warehousedb.Tennants
            .Where(o => o.businessId == businessId)
            .FirstOrDefault<Tennant>();
            if (userCheck == null && tennant != null)
            {
                // Hashing password with a generated salt.
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwd);

                User newUser = new User();
                Console.WriteLine("Tennant: " + tennant);
                newUser.tennant = tennant;
                newUser.Email = email;
                newUser.password = hashedPassword;

                // Adds and saves changes to the database
                _db.users.Add(newUser);
                _db.SaveChanges();
                response = Ok("User created");
            }
            else
            {
                response = BadRequest("User already exist");
            }
            return response;
        }


        [Authorize]
        [HttpGet("users")]
        public List<User> getAllUsers()
        {
            var users = _db.users.ToList();
            return users;
            // TODO: Passord må ikke vises
            // TODO: Hvis vi gjør om Users til en relasjon av Organisations/Tennants
            //var org = _db.organisations
            //.include(u => u.User)
            //.ToList();
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