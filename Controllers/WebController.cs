
using Microsoft.AspNetCore.Mvc;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.Data;
using System.Collections.Generic;

namespace Datawarehouse_Backend.Controllers
{
    //Hardcoded url, can change to "api/[controller]  - This makes the URL to whatever is infront of Controller
    // In the first case :  WebController -> It would become "api/web" etc.
    [Route("api/login")]
    [ApiController]
    /*
    Controller = A base class for an MVC controller with view support (Inheritance from ControllerBase)
    ControllerBase = A base class for an MVC controller without view support
    So controllers are for handling web pages, not web API requests
    If we want to use same controller for handling views and web APIs, then derive it from Controller.
    */
    public class WebController : ControllerBase 
    {
        private readonly IUserRepo _repository;

        public WebController(IUserRepo repository)
        {
            _repository = repository;           
        }
        
        // Get api/commands
        [HttpGet]
        public ActionResult <IEnumerable<User>> getAllUsers()
        {
            var users = _repository.GetAllUsers();
            return Ok(users);
        }
        
        // Get api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <User> getUserById(int id)
        {
            var user = _repository.GetUserById(id);
            return Ok(user);
        }
    }
}