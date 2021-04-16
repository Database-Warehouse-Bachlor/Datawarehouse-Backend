using System;
using System.Collections.Generic;
using System.Linq;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Datawarehouse_Backend.Controllers
{

    [Route("app/")]
    [ApiController]
    public class AppController : ControllerBase
    {

        public readonly WarehouseContext _warehouseDb;

        public AppController(WarehouseContext warehouseDb)
        {
            _warehouseDb = warehouseDb;
        }

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

        //[Authorize(Roles = "Admin")]
        [HttpPost("tennants")]
        public List<Tennant> getAllTennants() {
            var tennants = _warehouseDb.Tennants
            .ToList();
            return tennants;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("errors")]
        public List<ErrorLog> getAllErrors() {
            var errors = _warehouseDb.ErrorLogs
            .ToList();
            return errors;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("lasterrors")]
        public List<ErrorLog> getLatestErrors() {
            DateTime currentTime = DateTime.Now;
            var errors = _warehouseDb.ErrorLogs
            .Where(d => d.timeOfError >= currentTime.AddHours(-24))
            .ToList();
            return errors;
        }

        // Returns a list of all tennants.
        public int getNumberOfTennants()
        {
            var numberOfTennants = _warehouseDb.Tennants
            .Count();
            return numberOfTennants;
        }

        // Returns number of errors the last 24 hours
        public int getNumberOfErrorsLastTwentyFour()
        {
            DateTime currentTime = DateTime.Now;
            var numberOfErrors = _warehouseDb.ErrorLogs
            .Where(d => d.timeOfError >= currentTime.AddHours(-24))
            .Count();
            return numberOfErrors;
        }

    }
}