using System;
using System.Linq;
using Datawarehouse_Backend.Context;
using Microsoft.AspNetCore.Mvc;

namespace Datawarehouse_Backend.Controllers {
    
    [Route("app/")]
    [ApiController]
    public class AppController : ControllerBase {

        public readonly WarehouseContext _warehouseDb;

        public AppController(WarehouseContext warehouseDb)
        {
            _warehouseDb = warehouseDb;
        }

        [HttpGet("homeinfo")]
        public IActionResult getNumberOfTennantsAndLog() {
            IActionResult response; 

            response = Ok();
            return response;
        } 

        public int getNumberOfTennants() {
            var numberOfTennants = _warehouseDb.Tennants
            .Count();
            Console.WriteLine(numberOfTennants);
            return numberOfTennants;
        }
        public int getNumOfErrors() {
            var numberOfErrors = _warehouseDb.ErrorLogs
            .Count();
            Console.WriteLine(numberOfErrors);
            return numberOfErrors;
        }
    
    }
}