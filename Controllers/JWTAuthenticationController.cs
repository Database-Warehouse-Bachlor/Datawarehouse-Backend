
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
using Microsoft.EntityFrameworkCore;

/*
*This controller is mainly for authenticating users and registering new users.
*/ 
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
        /*
        * A function to find the correct User based on email.
        */
        private User findUserByMail(string email){
            var user = _db.Users
            .Where(e => e.Email == email)
            .FirstOrDefault<User>();
            return user;
        }

        /*
        * A function to find the correct Tennant based on it's ID.
        */
        private Tennant findTennantById(long tennantId) {
            var tennant = _warehousedb.Tennants
            .Where(o => o.id == tennantId)
            .FirstOrDefault<Tennant>();
            return tennant;
        }


        [HttpPost("login")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult login([FromForm] string email, [FromForm] string pwd)
        {
            User loginUser = findUserByMail(email);

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
        * This register-call is for the businesses to add additional users to their business.
        * To use this function, the tennant need to allready have a user connected to the tennant.
        */

        [Authorize(Roles = "User")]
        [HttpPost("register")]
        public IActionResult register([FromForm] string email, [FromForm] string pwd)
        {
            IActionResult response;

            User userCheck = findUserByMail(email);
            Tennant tennant = findTennantById(getTennantId());

            if (userCheck == null && tennant != null)
            {
                // Hashing password with a generated salt.
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwd);

                User newUser = new User();
                newUser.tennant = tennant;
                newUser.Email = email;
                newUser.password = hashedPassword;
                newUser.role = Role.User;

                // Adds and saves changes to the database
                _db.Entry(newUser).State = EntityState.Added;
                _db.SaveChanges();
                response = Ok("User created");
            }
            else
            {
                response = BadRequest("User already exist");
            }
            return response;
        }

        /*
        * This registration-call is for the administration to create the first user for the business.
        * This sets up the first connection between tennant and user.
        * After this connection is set up, the business can use the other registration-call to add additional users to it's business.
        */

       // TODO: [Authorize(Roles = "Admin")]
        [HttpPost("initregister")]
        public IActionResult initRegister([FromForm] string email, [FromForm] string pwd)
        {
            IActionResult response;

            User userCheck = findUserByMail(email);
            Tennant tennant = findTennantById(getTennantId());

            if (userCheck == null && tennant != null)
            {
                // Hashing password with a generated salt.
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwd);

                User initUser = new User();
                initUser.tennant = tennant;
                initUser.Email = email;
                initUser.password = hashedPassword;

                // Adds and saves changes to the database
                _db.Users.Add(initUser);
                _db.SaveChanges();
                response = Ok("User created");
            }
            else
            {
                response = BadRequest("User already exist");
            }
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public List<User> getAllUsers()
        {
            var users = _db.Users.ToList();
            return users;
            // TODO: Passord må ikke vises
            // TODO: Hvis vi gjør om Users til en relasjon av Organisations/Tennants
            //var org = _db.organisations
            //.include(u => u.User)
            //.ToList();
        }

         private long getTennantId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            return tennantId;
        }
        
    }

}