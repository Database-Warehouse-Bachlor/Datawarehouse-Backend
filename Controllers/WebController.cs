
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

        SecurityContext security;

        private readonly IConfiguration config;
        private readonly WarehouseContext warehouseDb;
        private readonly LoginDatabaseContext logindb;

        public WebController(IConfiguration config, WarehouseContext warehouseDb, LoginDatabaseContext logindb)
        {
            this.config = config;
            this.warehouseDb = warehouseDb;
            this.logindb = logindb;
        }

        [Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getInboundInvoice()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var currentUser = claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            Console.WriteLine(currentUser);
            var inboundInvoices = warehouseDb.InvoiceInbounds.ToList();
            return inboundInvoices;

        }

    }



}