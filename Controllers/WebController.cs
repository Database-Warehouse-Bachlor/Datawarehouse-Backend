using Datawarehouse_Backend.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Datawarehouse_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Datawarehouse_Backend.Controllers
{
    [Route("warehouse/")]
    [ApiController]
    
    public class WebController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly WarehouseContext _db;

        public WebController(IConfiguration config, WarehouseContext db)
        {
            _config = config;
            _db = db; 
        }

        //[Authorize]
        [HttpGet("absence30")]
        public IQueryable<AbsenceRegister> getLastMonthAbsence() 
        {
            var firstDay = DateTime.Today.AddDays(-30);

            IQueryable<AbsenceRegister> absences = _db.AbsenceRegisters
            .Where(x => x.fromDate >= firstDay);

            return absences.AsQueryable();
        }
    }
}