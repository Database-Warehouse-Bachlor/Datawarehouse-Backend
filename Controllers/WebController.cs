
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
using Newtonsoft.Json;

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

        [Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getAllInboundInvoice([FromForm] long tennantId)
        {
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantId == tennantId)
            .ToList();
            return inboundInvoices;
        }


        [Authorize]
        [HttpGet("orders")]
        public List<Order> getAllOrders([FromForm] long tennantId)
        {
           var orders = _warehouseDb.Orders
           .Where(o => o.tennantId == tennantId)
           .ToList();
           return orders;
        }

        [Authorize]
        [HttpGet("customers")]
        public List<Customer> getCustomers([FromForm] long tennantId) {
            var customers = _warehouseDb.Customers
            .Where(c => c.tennantId == tennantId)
            .ToList();
            return customers;
        }
        
        [Authorize]
        [HttpGet("balance")]
        public List<BalanceAndBudget> getBalanceAndBudget([FromForm] long tennantId) {
            var balanceAndBudgets = _warehouseDb.BalanceAndBudgets
            .Where(b => b.tennantId == tennantId)
            .ToList();
            return balanceAndBudgets;
        }

        
        [Authorize]
        [HttpGet("absence")]
        public List<AbsenceRegister> getAbsenceRegister([FromForm] long tennantId)
        {
            var absence = _warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennantId == tennantId)
            .ToList();
            return absence;
        }

        [Authorize]
        [HttpGet("timeregister")]
        public List<TimeRegister> getTimeRegisters([FromForm] long tennantId)
        {
            var timeRegisters = _warehouseDb.TimeRegisters
            .Where(t => t.employee.tennantId == tennantId)
            .ToList();
            return timeRegisters;
        }

        [Authorize]
        [HttpGet("accrecieve")]
        public List<AccountsReceivable> getAccountsReceivables([FromForm] long customerId) {
            var accountsReceivable = _warehouseDb.AccountsReceivables
            .Where(a => a.customerId == customerId)
            .ToList();
            return accountsReceivable;
        }

    }



}