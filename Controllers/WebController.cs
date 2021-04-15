
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Claims;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.ViewModels;
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
                    comparisonDate = dateTimeNow.Date.AddDays(-30);
                    break;
                case "lastTwelveMonths":
                    comparisonDate = dateTimeNow.Date.AddYears(-1);
                    break;
                case "thisMonth":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempDay + 1);
                    break;
                case "thisYear":
                    comparisonDate = dateTimeNow.Date.AddMonths(-tempMonth + 1).AddDays(-tempDay + 1);
                    break;
                case "thisWeek":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempWeek + 1);
                    break;
                default:
                    Console.WriteLine("No filter added, listing all..");
                    comparisonDate = dateTimeNow.Date.AddYears(-30);
                    break;
            }
            Console.WriteLine("Filter selected: " + time);
            Console.WriteLine("current date: " + dateTimeNow);
            Console.WriteLine("Filtering by: " + comparisonDate);
            return comparisonDate;
        }

        /*
        * A method to fetch all inbound invoices from a specific tennant.
        */

        // [Authorize]
        [HttpGet("inbound")]
        public List<InvoiceInbound> getAllInboundInvoice([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantId == tennantId)
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

        //[Authorize]
        [HttpGet("absence")]
        public List<AbsenceView> getAbsenceRegister([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var absence = _warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennantId == tennantId)
            .Where(d => d.fromDate >= comparisonDate)
            .OrderBy(d => d.fromDate)
            .ToList();

            Console.WriteLine("Number of objects found: " + absence.Count);
            List<AbsenceView> absenceViews = new List<AbsenceView>();
            double totalAbsence = 0;

            /*
            *  Takes information from all the absenceRegisters requested, and puts them into a new list of absence viewmodels which
            *  only tracks year, month and total absence for that month. 
            *  So instead of getting a list of all absences, it gives a list of total absences per month.
            */
            try
            {
                for (int i = 0; i < absence.Count; i++)
                {
                    Console.WriteLine("i value: " + i);
                    if (i != absence.Count - 1)
                    {
                        if (absence[i].fromDate.Month == absence[i + 1].fromDate.Month)
                        { //since the list is ordered allready, we can compare current month with next, if it is, add the duration to months total
                            totalAbsence += absence[i].duration;
                            Console.WriteLine("Adding days.." + "\nCurrent total: " + totalAbsence);
                            Console.WriteLine("Next absence is: " + absence[i + 1].id);
                        }
                        else
                        { // Next absence is a new month, add the current absence we're on and add the view to the new list of views.
                            totalAbsence += absence[i].duration;
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.totalDuration = totalAbsence;
                            Console.WriteLine("VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                            absenceViews.Add(view);
                            totalAbsence = 0;
                        }
                    }
                    else if (absence[i].fromDate.Month == absence[i - 1].fromDate.Month)
                    { //last absence has the same month as the one previously added absence
                        totalAbsence += absence[i].duration;
                        AbsenceView view = new AbsenceView();
                        view.year = absence[i].fromDate.Year;
                        view.month = absence[i].fromDate.Month;
                        view.totalDuration = totalAbsence;
                        Console.WriteLine("VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                        absenceViews.Add(view);
                        totalAbsence = 0;
                    }
                    else
                    { //last absence is in a new month
                        totalAbsence += absence[i].duration;
                        AbsenceView view = new AbsenceView();
                        view.year = absence[i].fromDate.Year;
                        view.month = absence[i].fromDate.Month;
                        view.totalDuration = absence[i].duration;
                        Console.WriteLine("VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                        absenceViews.Add(view);
                        totalAbsence = 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            return absenceViews;
        }

        [Authorize]
        [HttpGet("timeregister")]
        public List<TimeRegister> getTimeRegisters([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
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
        public List<Order> getAllOrders([FromForm] long tennantId, [FromForm] string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            var orders = _warehouseDb.Orders
            .Where(o => o.tennantId == tennantId)
            // .Where(d => d.invoiceDate >= comparisonDate)
            //.OrderByDescending(d => d.invoiceDate)
            .ToList();
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

    }
}