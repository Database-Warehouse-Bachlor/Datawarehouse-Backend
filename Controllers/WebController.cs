
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
   /*      private string lastThirtyDays = "30";
        private string lastTwelveMonths = "12";
        private string thisMonth = "thisMonth";
        private string thisYear = "thisYear";
        private string thisWeek = "thisWeek"; */
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
        * Sets the filter to a pre-defined option based on what's requested, if no option is specified, all will be selected.
        * When modifying days, years or months, the timer for that day will allways be set to 00:00:00, so there is no need to
        * remove the hours aswell.
        */
        private DateTime compareDates(string time)
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime comparisonDate = dateTimeNow;
            int tempMonth = dateTimeNow.Month;
            int tempWeek = (int)dateTimeNow.DayOfWeek;
            int tempDay = dateTimeNow.Day;

            switch (time)
            {
                case "lastThirtyDays":
                    comparisonDate = dateTimeNow.Date.AddDays(-30);//.AddHours(-tempHour);
                    break;
                case "lastTwelveMonths":
                    comparisonDate = dateTimeNow.Date.AddYears(-1); //.AddHours(-tempHour);
                    break;
                case "thisMonth":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempDay + 1); //.AddHours(-tempHour);
                    break;
                case "thisYear":
                    comparisonDate = dateTimeNow.Date.AddMonths(-tempMonth + 1).AddDays(-tempDay + 1); //.AddHours(-tempHour);
                    break;
                case "thisWeek":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempWeek + 1); //.AddHours(-tempHour);
                    break;
                default:
                    Console.WriteLine("No filter added, listing all..");
                    comparisonDate = dateTimeNow.Date.AddYears(-30);
                    break;
            }
            Console.WriteLine("Filter selected: " +time);
            Console.WriteLine("current date: " +dateTimeNow);
            Console.WriteLine("Filtering by: " +comparisonDate);
            return comparisonDate;
        }

        /*
        * A method to fetch all inbound invoices from a specific tennant.
        */

       // [Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getAllInboundInvoice([FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var inboundInvoices = getTennant().invoicesInbound
            .Where(d => d.invoiceDate >= comparisonDate)
            .OrderByDescending(d => d.invoiceDate)
            .ToList();
            return inboundInvoices;
        }

        /*
        * Getting invoice outbound based on it's duedate and filter given.
        */
       // [Authorize]
        [HttpGet("outbound")]
        public List<InvoiceOutbound> getInvoiceOutbounds([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var invoiceOutbounds = _warehouseDb.InvoiceOutbounds
            .Where(a => a.customer.tennantId == tennantId)
            .Where(d => d.invoiceDue >= comparisonDate)
            .OrderByDescending(d => d.invoiceDue)
            .ToList();
            return invoiceOutbounds;
        }

        [Authorize]
        [HttpGet("absence")]
        public List<AbsenceView> getAbsenceRegister([FromForm] string filter)
        {
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            var absence = _warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennantId == tennantId)
            .Where(d => d.fromDate >= comparisonDate)
            .OrderByDescending(d => d.fromDate)
            .ToList();
            return absence;
        }

        [Authorize]
        [HttpGet("timeregister")]
        public List<TimeRegister> getTimeRegisters([FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            long tennantId = getTennantId();
            var timeRegisters = _warehouseDb.TimeRegisters
            .Where(t => t.employee.tennantId == tennantId)
            .Where(d => d.recordDate >= comparisonDate)
            .OrderByDescending(d => d.recordDate)
            .ToList();
            return timeRegisters;
        }
        /*
       * A method to fetch all orders from a specific tennant.
       */

        [Authorize]
        [HttpGet("orders")]
        public List<Order> getAllOrders([FromForm] string filter)
        {
            Tennant tennant =  getTennant();
            DateTime comparisonDate = compareDates(filter);
            var orders = tennant.orders.ToList();
            // .Where(d => d.invoiceDate >= comparisonDate)
            //.OrderByDescending(d => d.invoiceDate)
            return orders;
        }

        /*
       * A method to fetch all inbound invoices from a specific tennant.
       */

        [Authorize]
        [HttpGet("customers")]
        public List<Customer> getCustomers([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var customers = _warehouseDb.Customers
            .Where(c => c.tennantId == tennantId)
            //.Where(d => d.invoiceDate >= comparisonDate)
            // .OrderByDescending(d => d.invoiceDate)
            .ToList();
            return customers;
        }

        /*
       * A method to fetch all inbound invoices from a specific tennant.
       */

        [Authorize]
        [HttpGet("balance")]
        public List<BalanceAndBudget> getBalanceAndBudget([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var balanceAndBudgets = _warehouseDb.BalanceAndBudgets
            .Where(b => b.tennantId == tennantId)
            //.Where(d => d.invoiceDate >= comparisonDate)
            // .OrderByDescending(d => d.invoiceDate)
            .ToList();
            return balanceAndBudgets;
        }


        [Authorize]
        [HttpGet("accrecieve")]
        public List<AccountsReceivable> getAccountsReceivables([FromForm] long customerId, [FromForm] string filter)
        {
            var accountsReceivable = _warehouseDb.AccountsReceivables
            .Where(a => a.customerId == customerId)
            .ToList();
            return accountsReceivable;
        }

        private long getTennantId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            return tennantId;
        }
        private Tennant getTennant()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            Tennant tennant = _warehouseDb.Tennants.Where(t => t.id == tennantId).FirstOrDefault<Tennant>();
            return tennant;
        }
    }
}