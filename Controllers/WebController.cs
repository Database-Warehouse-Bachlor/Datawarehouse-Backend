
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
                case "lastSevenDays":
                    comparisonDate = dateTimeNow.Date.AddDays(-7);
                    break;
                case "lastThirtyDays":
                    comparisonDate = dateTimeNow.Date.AddDays(-30);
                    break;
                case "lastTwelveMonths":
                    comparisonDate = dateTimeNow.Date.AddYears(-1);
                    break;
                case "thisWeek":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempWeek + 1);
                    break;
                case "thisMonth":
                    comparisonDate = dateTimeNow.Date.AddDays(-tempDay + 1);
                    break;
                case "thisYear":
                    comparisonDate = dateTimeNow.Date.AddMonths(-tempMonth + 1).AddDays(-tempDay + 1);
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
        public List<InvoiceInbound> getAllInboundInvoice(string filter)
        {
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            var inboundInvoices = _warehouseDb.InvoiceInbounds
            .Where(i => i.tennantFK == tennantId)
            .Where(d => d.invoiceDate >= comparisonDate)
            .OrderByDescending(d => d.invoiceDate)
            .ToList();
            return inboundInvoices;
        }

        /*
        * Getting invoice outbound based on it's duedate and filter given.
        */
        [Authorize]
        [HttpGet("outbound")]
        public List<InvoiceOutbound> getInvoiceOutbounds(string filter)
        {
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            var invoiceOutbounds = _warehouseDb.InvoiceOutbounds
            .Where(a => a.customer.tennantFK == tennantId)
            .Where(d => d.invoiceDue >= comparisonDate)
            .OrderByDescending(d => d.invoiceDue)
            .ToList();
            return invoiceOutbounds;
        }

        //[Authorize]
        [HttpGet("absence")]
        public IList<AbsenceView> getAbsenceRegister(string filter)
        {
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            var absence = _warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennantFK == tennantId)
            .Where(d => d.fromDate >= comparisonDate)
            .OrderBy(d => d.fromDate)
            .ToList();

            Console.WriteLine("Number of objects found: " + absence.Count);
            List<AbsenceView> absenceViews = new List<AbsenceView>();
            double totalAbsence = 0;

            /*
            *  Takes information from all the absenceRegisters requested, and puts them into a new list of absence viewmodels which
            *  only tracks year, month and total absence for that month OR Date and total absence for that date.
            *  If it's this week/month or last 30 / 7 days it will summarize for each date instead of month.
            * So instead of getting a list of all absences, it gives a list of total absences per month/date.
            */
            if (filter == "thisWeek" || filter == "thisMonth" || filter == "lastThirtyDays" || filter == "lastSevenDays")
            {
                try
                {
                    for (int i = 0; i < absence.Count; i++)
                    {
                        Console.WriteLine("i value: " + i);
                        if (i != absence.Count - 1)
                        {
                            if (absence[i].fromDate.Day == absence[i + 1].fromDate.Day)
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
                                view.day = absence[i].fromDate.Day;
                                view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                                view.totalDuration = totalAbsence;
                                Console.WriteLine("WIEW WeekDay: " + view.weekDay + "VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                                absenceViews.Add(view);
                                totalAbsence = 0;
                            }
                        }
                        else if (absence[i].fromDate.Day == absence[i - 1].fromDate.Day)
                        { //last absence has the same month as the one previously added absence
                            totalAbsence += absence[i].duration;
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.day = absence[i].fromDate.Day;
                            view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                            view.totalDuration = totalAbsence;
                            Console.WriteLine("WIEW WeekDay: " + view.weekDay + "VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                            absenceViews.Add(view);
                            totalAbsence = 0;
                        }
                        else
                        { //last absence is in a new month
                            totalAbsence += absence[i].duration;
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.day = absence[i].fromDate.Day;
                            view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                            view.totalDuration = absence[i].duration;
                            Console.WriteLine("WIEW WeekDay: " + view.weekDay + "VIEW Month: " + view.month + "\nVIEW Year: " + view.year + "\nTotal Duration: " + view.totalDuration);
                            absenceViews.Add(view);
                            totalAbsence = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    //TODO: Create errorlogg
                }
                return absenceViews;
            }
            else
            {
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
                    //TODO: Create errorlogg
                }
                return absenceViews;
            }
        }

        [Authorize]
        [HttpGet("timeregister")]
        public List<TimeRegister> getTimeRegisters(string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            long tennantId = getTennantId();
            var timeRegisters = _warehouseDb.TimeRegisters
            .Where(t => t.employee.tennantFK == tennantId)
            .Where(d => d.recordDate >= comparisonDate)
            .OrderByDescending(d => d.recordDate)
            .ToList();
            return timeRegisters;
        }
        /*
        * A method to fetch all orders from a specific tennant.
        */

        [Authorize]
        [HttpGet("pendingOrders")]
        public List<OrderView> getPendingOrders()
        {
            var orders = getTennant().orders
            .Where(o => o.endDate >= DateTime.Now)
            .OrderByDescending(o => o.endDate)
            .ToList();
            List<OrderView> orderList = new List<OrderView>();
            for (int i = 0; i < orders.Count; i++)
            {
                OrderView order = new OrderView();
                order.customerName = orders[i].customer.customerName;
                order.invoiceTotal = orders[i].invoiceOutbound.amountTotal;
                order.jobname = orders[i].jobName;
                order.endDate = orders[i].endDate;
                orderList.Add(order);

            }
            return orderList;
        }

        [Authorize]
        [HttpGet("allOrders")]
        public List<OrderView> getAllOrders()
        {
            var orders = getTennant().orders
            .OrderByDescending(o => o.endDate)
            .ToList();
            List<OrderView> orderList = new List<OrderView>();
            for (int i = 0; i < orders.Count; i++)
            {
                OrderView order = new OrderView();
                order.customerName = orders[i].customer.customerName;
                order.invoiceTotal = orders[i].invoiceOutbound.amountTotal;
                order.jobname = orders[i].jobName;
                order.endDate = orders[i].endDate;
                orderList.Add(order);
            }
            return orderList;
        }


        /*
        * A method to find all customers for a specific tennant.
        * Returns a list of customers ordered by their names.
        * The customer information returned: Name, address, zipcode, city and total AmountDue from
        * it's list of accounts receivables.
        *
        * Returns:
        * A list of customer addresses, zipcode, city and total amount due
        *
        * NOTE: Nested forloop. n^2 runtime.
        * Possible fix: Make the amountDue total for a customer be stored in the model
        * and updated whenever data is submitted in the dataSubmissionController.
        */

        [Authorize]
        [HttpGet("customers")]
        public List<CustomerView> getCustomers()
        {
            var customers = getTennant().customers
            .OrderByDescending(c => c.customerName)
            .ToList();

            List<CustomerView> customerList = new List<CustomerView>();
            for (int i = 0; i < customers.Count; i++)
            {
                CustomerView cust = new CustomerView();
                cust.customerName = customers[i].customerName;
                cust.address = customers[i].address;
                cust.zipcode = customers[i].zipcode;
                cust.city = customers[i].city;
                decimal due = 0;
                List<AccountsReceivable> accounts = customers[i].accountsreceivables.ToList();
                for (int j = 0; j < accounts.Count; j++)
                {
                    due += accounts[j].amountDue;
                }
                cust.amountDue = due;
                customerList.Add(cust);
            }
            return customerList;
        }

        /*
        * A method to fetch all inbound invoices from a specific tennant.
        *
        * 
        * Returns:
        * A list of customer addresses, zipcode, city and total amount due
        */

        [Authorize]
        [HttpGet("balance")]
        public List<BnbView> getBalanceAndBudget(string filter)
        {
            //Gets all BnBs within the filter
            DateTime comparisonDate = compareDates(filter);
            var balanceAndBudgets = getTennant().bnb
            .Where(d => d.periodDate >= comparisonDate)
            .OrderByDescending(d => d.periodDate).ToList();
            //Grab the important information and put it in a list of views so it's easier to handle @frontend
            List<BnbView> bnbList = new List<BnbView>();
            for(int i = 0; i<balanceAndBudgets.Count; i++){
                BnbView bnb = new BnbView();
                bnb.account = balanceAndBudgets[i].account;
                bnb.startBalance = balanceAndBudgets[i].startBalance;
                bnb.periodBalance = balanceAndBudgets[i].periodBalance;
                bnb.endBalance = balanceAndBudgets[i].endBalance;
                bnbList.Add(bnb);
            }
            return bnbList;
        }

        /*
        * Finds all the customers for the tennant then for each customer it iterates over
        * the customers accountreceivables and adds the information needed to AccountsView
        * and returns a list of AccountsView
        */
        [Authorize]
        [HttpGet("accreceive")]
        public List<AccountsView> getAccountsReceivables(string filter)
        {
            Tennant tennant = getTennant();
            var customers = tennant.customers.ToList();
            List<AccountsView> accountsReceivable = new List<AccountsView>();
            for (int i = 0; i < customers.Count; i++)
            {
                List<AccountsReceivable> accounts = customers[i].accountsreceivables.ToList();
                for (int j = 0; j < accounts.Count; j++)
                {
                    AccountsView acc = new AccountsView();
                    acc.amount = accounts[j].amount;
                    acc.amountDue = accounts[j].amountDue;
                    acc.dueDate = accounts[j].dueDate;
                    accountsReceivable.Add(acc);
                }
            }
            return accountsReceivable;
        }


        /*
        * Two functions that takes the first claim from the JWT Token to find which tennantId
        * their accounts is bound to.  The first function returns the object tennant based on the ID
        * The other function simply returns the tennantId that has been found.
        */

        private Tennant getTennant()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            Tennant tennant = _warehouseDb.Tennants
            .Where(t => t.id == tennantId).FirstOrDefault<Tennant>();
            return tennant;
        }
        private long getTennantId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            return tennantId;
        }
    }
}