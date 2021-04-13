using System;
using System.Linq;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
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

        // [Authorize]
        [HttpGet("homeinfo")]
        public IActionResult getNumberOfTennantsAndErrorsAsJson()
        {
            IActionResult response;

            AppInfoHomeMenu appInfoHomeMenu = new AppInfoHomeMenu();

            appInfoHomeMenu.numberOfTennants = getNumberOfTennants();
            appInfoHomeMenu.numberOfErrors = getNumberOfErrors(); 
            
            var dataToJson = JsonConvert.SerializeObject(appInfoHomeMenu);
            
            response = Ok(dataToJson);

            return response;
        }

        public int getNumberOfTennants()
        {
            var numberOfTennants = _warehouseDb.Tennants
            .Count();
            return numberOfTennants;
        }
        public int getNumberOfErrors()
        {
            var numberOfErrors = _warehouseDb.ErrorLogs
            .Count();
            return numberOfErrors;
        }

    }
}