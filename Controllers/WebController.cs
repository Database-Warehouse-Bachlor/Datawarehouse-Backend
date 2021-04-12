
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

/*
* This controller handles API-calls from logged in users on the website. The calls handled by this controller
* is mainly calls to fetch data from the warehouse, to be displayed in the dashboard of the website.
*/

namespace Datawarehouse_Backend.Controllers
{
    [Route("web/")]
    [ApiController]

    public class WebController : ControllerBase
    {

        //SecurityContext security;
       // private enum time { lastThirtyDays, lastTwelveMonths, thisMonth, thisYear, thisWeek}
        private string lastThirtyDays = "30";
        private string lastTwelveMonths = "12";
        private string thisMonth = "thisMonth";
        private string thisYear = "thisYear";
        private string thisWeek = "thisWeek";
        private readonly IConfiguration config;
        private readonly WarehouseContext _warehouseDb;
        private readonly LoginDatabaseContext _db;

        public WebController(IConfiguration config, WarehouseContext warehouseDb, LoginDatabaseContext logindb)
        {
            this.config = config;
            this._warehouseDb = warehouseDb;
            this._db = logindb;
        }
        /*
        * Consider converting to switch-case if there's more than 5 options needed.
        */
        private DateTime compareDates(string time)
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime comparisonDate = dateTimeNow;
                int tempMonth = dateTimeNow.Month;
                int tempWeek = (int)dateTimeNow.DayOfWeek;
                int tempDay = dateTimeNow.Day;
                int tempHour = dateTimeNow.Hour;
                
            if (time == lastThirtyDays)
            {
                comparisonDate = dateTimeNow.Date.AddDays(-30).AddHours(-tempHour);
            }
            else if (time == lastTwelveMonths)
            {
                comparisonDate = dateTimeNow.Date.AddYears(-1).AddHours(-tempHour);
            }
            else if (time == thisMonth)
            {
                comparisonDate = dateTimeNow.Date.AddDays(-tempDay + 1).AddHours(-tempHour);
            }
            else if (time == thisYear)
            {
                comparisonDate = dateTimeNow.Date.AddMonths(-tempMonth + 1).AddDays(-tempDay + 1).AddHours(-tempHour);
            }
            else if (time == thisWeek)
            {
                comparisonDate = dateTimeNow.Date.AddDays(-tempWeek+2).AddHours(-tempHour);
                // Add +2 because +1 since metadata starts on sunday, and another +1 because we subtract all the days.
            }
            Console.WriteLine("Filtering by date: " + comparisonDate);
            return comparisonDate;
        }

        /*
        * A method to fetch all inbound invoices from a specific tennant.
        */

        [Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getAllInboundInvoice([FromForm] long tennantId)
        {
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantId == tennantId)
            .ToList();
            return inboundInvoices;
        }

        /*
       * A method to fetch all orders from a specific tennant.
       */

        [Authorize]
        [HttpGet("orders")]
        public List<Order> getAllOrders([FromForm] long tennantId)
        {
            var orders = _warehouseDb.Orders
            .Where(o => o.tennantId == tennantId)
            .ToList();
            return orders;
        }

        /*
       * A method to fetch all inbound invoices from a specific tennant.
       */

        [Authorize]
        [HttpGet("customers")]
        public List<Customer> getCustomers([FromForm] long tennantId)
        {
            var customers = _warehouseDb.Customers
            .Where(c => c.tennantId == tennantId)
            .ToList();
            return customers;
        }

        /*
       * A method to fetch all inbound invoices from a specific tennant.
       */

        [Authorize]
        [HttpGet("balance")]
        public List<BalanceAndBudget> getBalanceAndBudget([FromForm] long tennantId)
        {
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

        /* [Authorize]
         [HttpGet("absence30")]
         public List<AbsenceRegister> getAbsenceRegisterLastThirtyDays([FromForm] long tennantId)
         {
             DateTime dateTimeNow = DateTime.Now;
             DateTime comparisonDate = dateTimeNow.Date.AddDays(-30);
             var absence = _warehouseDb.AbsenceRegisters
             .Where(i => i.employee.tennantId == tennantId)
             .Where(d => d.)
             .ToList();
             return absence;
         }*/


        //[Authorize]
        [HttpGet("inbound30")]
        public List<InvoiceInbound> getAllInboundInvoiceLastThirtyDays([FromForm] long tennantId, [FromForm] string time)
        {
            DateTime comparisonDate = compareDates(time);
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantId == tennantId)
            .Where(d => d.invoiceDate >= comparisonDate)
            .OrderByDescending(d => d.invoiceDate)
            .ToList();
            return inboundInvoices;
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
        public List<AccountsReceivable> getAccountsReceivables([FromForm] long customerId)
        {
            var accountsReceivable = _warehouseDb.AccountsReceivables
            .Where(a => a.customerId == customerId)
            .ToList();
            return accountsReceivable;
        }

        [Authorize]
        [HttpGet("outbound")]
        public List<InvoiceOutbound> getInvoiceOutbounds([FromForm] long customerId)
        {
            var invoiceOutbounds = _warehouseDb.InvoiceOutbounds
            .Where(a => a.customerId == customerId)
            .ToList();
            return invoiceOutbounds;
        }
    }
}