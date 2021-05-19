using System.Collections.Generic;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



/*
*   This controller is for the app to give information to users. 
*/
namespace Datawarehouse_Backend.Controllers
{

    [Route("app/")]
    [ApiController]
    public class AppController : ControllerBase
    {

        public readonly IWarehouseContext _warehouseDb;

        public AppController(IWarehouseContext warehouseDb)
        {
            _warehouseDb = warehouseDb;
        }

        /*
        *   Returns number of tennants and errors last 24 hours as json.
        */
        // [Authorize(Roles = "Admin")]
        [HttpPost("homeinfo")]
        public IActionResult getNumberOfTennantsAndErrorsAsJson()
        {
            IActionResult response;

            AppInfoHomeMenu appInfoHomeMenu = new AppInfoHomeMenu();

            appInfoHomeMenu.numberOfTennants = getNumberOfTennants();
            appInfoHomeMenu.numberOfErrors = getNumberOfErrorsLastTwentyFour(); 
            
            var dataToJson = JsonConvert.SerializeObject(appInfoHomeMenu);

            response = Ok(dataToJson);

            return response;
        }

        /*
        *   Returns a list of all errors
        */
        //[Authorize(Roles = "Admin")]
        [HttpPost("errors")]
        public List<ErrorLog> getAllErrors() {
           return _warehouseDb.getAllErrors();
        }

        /*
        *   Returns a list of all errors in the last 24 hours
        */
        //[Authorize(Roles = "Admin")]
        [HttpPost("lasterrors")]
        public List<ErrorLog> getLatestErrors() {
            return _warehouseDb.getLatestErrors();
        }

        /*
        *   Returns a list of all tennants registered in the database
        */
        //[Authorize(Roles = "Admin")]
        [HttpPost("tennants")]
        public List<Tennant> getAllTennants() {
            return _warehouseDb.getAllTennants();
        }


        /*
        *   Returns number of all tennants
        */
        public int getNumberOfTennants()
        {
            return _warehouseDb.getNumberOfTennants();
        }

        /*
        *   Returns number of errors the last 24 hours
        */
        public int getNumberOfErrorsLastTwentyFour()
        {
            return _warehouseDb.getNumberOfErrorsLastTwentyFour();
        }
    }
}