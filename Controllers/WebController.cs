using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Datawarehouse_Backend.Controllers
{
    //Hardcoded url, can change to "api/[controller]  - This makes the URL to whatever is infront of Controller
    // In the first case :  WebController -> It would become "api/web" etc.
    [Route("api/login")]
    [ApiController]
    public class WebController : ControllerBase 
    {
        public ActionResult <IEnumerable<User>> getAllUsers()
        {
            
        }
    }
}