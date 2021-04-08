
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Claims;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Datawarehouse_Backend.Controllers
{
    [Route("web/")]
    [ApiController]

    public class WebController : ControllerBase
    {

        //SecurityContext security;

        private readonly IConfiguration config;
        private readonly WarehouseContext _warehouseDb;
        private readonly LoginDatabaseContext _db;

        public WebController(IConfiguration config, WarehouseContext warehouseDb, LoginDatabaseContext logindb)
        {
            this.config = config;
            this._warehouseDb = warehouseDb;
            this._db = logindb;
        }

        //[Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getInboundInvoice([FromForm] string businessId)
        [Authorize]
        [HttpGet("absence")]
        public List<AbsenceRegister> getAbsenceRegister([FromForm] string businessId)
        {
            var tennant = _warehouseDb.Tennants
            .Where(t => t.businessId == businessId)
            .FirstOrDefault<Tennant>();
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantId == tennant.id)
            .ToList();
            return inboundInvoices;
            
            

            var tennant = warehouseDb.Tennants.
            Where(t => t.businessId == businessId)
            .FirstOrDefault<Tennant>();
            var absence = warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennant == tennant)
            .ToList();
            return absence;

        }

    }



}