
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using Datawarehouse_Backend.Context;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        private readonly IWarehouseContext _warehouseDb;
        public WebController(IConfiguration config, IWarehouseContext warehouseDb)
        {
            this.config = config;
            this._warehouseDb = warehouseDb;
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
                    comparisonDate = dateTimeNow.Date.AddYears(-30);
                    break;
            }
            return comparisonDate;
        }

        /*
        * A method to get all outbound vouchers that are paid too late or not paid.
        * Returns:
        * A custom(Weekly) update starting from the date of the filter til todays date, for all outbound vouchers that are
        * paid too late or not paid at all.  The custom updates keeps track of the amount due at every given time, and in which
        * category the amount due belongs(1-30/31-60/61-90/90+). 
        */

        [Authorize(Roles = "User")]
        [HttpGet("accrec")]
        public List<DateStatusView> getAccountReceivables(string filter)
        {
            DateTime dateTimeNow = DateTime.Now;
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            var vouchers = _warehouseDb.getVouchersInDescendingByPaymentThenByType(tennantId, comparisonDate);
            //We now have a list of all vouchers that has date
            //after the filter given, ordered by paymentId, then by type
            // This enables us to compare voucher n to n+1
            // if n has a voucher that is paid, it will be n+1
            // and it makes sure that voucher n is the first voucher that is made on that id
            // making n the outgoing voucher, and n+1 the payment voucher
            // but only if n and n+1 has same paymentId

            // Now we find all the vouchers that has been paid too late.
            List<AccRecView> accList = new List<AccRecView>();
            for (int i = 0; i < vouchers.Count; i++)
            {
                //Even though there shouldnt be any vouchers without invoices getting through the datasubmission
                //We double check to avoid any errors, and still provide a visual feed in the frontend.
                if (vouchers[i].invoice == null)
                {
                    i++;
                }
                if (i < vouchers.Count - 1)
                {
                    //if outbound and payment has same paymentId and is paid too late
                    if (vouchers[i].paymentId == vouchers[i + 1].paymentId && vouchers[i].invoice.dueDate < vouchers[i + 1].date)
                    {
                        AccRecView view = new AccRecView();
                        view.PID = vouchers[i].paymentId;
                        view.dueDate = vouchers[i].invoice.dueDate;
                        view.payDate = vouchers[i + 1].date;
                        view.amount = vouchers[i].invoice.amountTotal;
                        view.daysDue = view.payDate.DayOfYear - view.dueDate.DayOfYear;
                        accList.Add(view);
                        i++; //Skip i+1, since we compiled i and i+1
                    }
                    //If an outbound voucher has not been paid. 
                    //Setting payDate to the date of request so that I can categorize how long overdue the payment is. 
                    else if (vouchers[i].paymentId != vouchers[i + 1].paymentId && vouchers[i].type == "outbound")
                    {
                        AccRecView view = new AccRecView();
                        view.PID = vouchers[i].paymentId;
                        view.dueDate = vouchers[i].invoice.dueDate;
                        view.payDate = dateTimeNow;
                        view.amount = vouchers[i].invoice.amountTotal;
                        view.daysDue = view.payDate.DayOfYear - view.dueDate.DayOfYear;
                        accList.Add(view);
                    }
                    //if a payment without an outbound ended up in the list. Which is either a missing outbound or because the outbound was before the filter.
                    else if (vouchers[i].paymentId != vouchers[i + 1].paymentId && vouchers[i].type == "payment")
                    {
                        //Go to next
                    }
                    //If the outbound voucher is paid, and paid within the duedate, skip this and next voucher.
                    else
                    {
                        i++;
                    }
                }
                //Last payment n was not connected to n-1, therefore it's an unpaid outbound invoice.
                else
                {
                    AccRecView view = new AccRecView();
                    view.PID = vouchers[i].paymentId;
                    view.dueDate = vouchers[i].invoice.dueDate;
                    view.payDate = dateTimeNow;
                    view.amount = vouchers[i].invoice.amountTotal;
                    view.daysDue = view.payDate.DayOfYear - view.dueDate.DayOfYear;
                    accList.Add(view);
                }
            }
            //accList.Sort((x, y) => x.dueDate.CompareTo(y.dueDate));
            /*
            We now have a sorted list of vouchers that was paid too late, or not paid at all.

            We start at the day of the filter requested, and move with a custom timeintervall that is now set to 7days,
            all the way til we reach todays date then once more for today.
            For every step we take towards todays date, we iterate over the list of vouchers that was paid too late, or not paid at all.
            For every voucher in that list we check which duedate category it belongs to, and then add the amountdue the voucher has to the correct category
            */

            //Setting the temporary date to the first day of the filter requested.
            //And setting timeIntervall to 7 Days.
            List<DateStatusView> graphList = new List<DateStatusView>();
            DateTime tempDate = comparisonDate;
            int timeIntervall = 7;
            while (tempDate <= dateTimeNow)
            {
                DateStatusView aView = new DateStatusView();
                aView.year = tempDate.Year;
                aView.month = tempDate.Month;
                aView.day = tempDate.Day;
                aView.thirtyAmount = 0;
                aView.sixtyAmount = 0;
                aView.ninetyAmount = 0;
                aView.ninetyPlusAmount = 0;

                for (int i = 0; i < accList.Count; i++)
                {
                    if (accList[i].dueDate <= tempDate && tempDate <= accList[i].payDate)
                    {
                        if (accList[i].daysDue < 30)
                        {
                            aView.thirtyAmount += accList[i].amount;
                        }
                        else if (accList[i].daysDue >= 30 && accList[i].daysDue < 60)
                        {
                            aView.sixtyAmount += accList[i].amount;
                        }
                        else if (accList[i].daysDue >= 60 && accList[i].daysDue < 90)
                        {
                            aView.ninetyAmount += accList[i].amount;
                        }
                        else
                        {
                            aView.ninetyPlusAmount += accList[i].amount;
                        }
                    }
                }
                //If one week doesnt surpass today, add one week
                if (tempDate.AddDays(timeIntervall) < dateTimeNow)
                {
                    tempDate = tempDate.AddDays(timeIntervall);
                }
                // if one week surpasses todays date, set it as today
                else if (tempDate != dateTimeNow)
                {
                    tempDate = dateTimeNow;
                }
                // We have now added all dates wanted, including todays date, push it over to break the whileloop.
                else
                {
                    tempDate = dateTimeNow.AddDays(1);
                }
                graphList.Add(aView);
            }
            return graphList;
        }

        /*
        *  Takes information from all the absenceRegisters requested, and puts them into a new list of absence viewmodels which
        *  only tracks year, month and total absence for that month OR Date and total absence for that date.
        *  If it's this week/month or last 30 / 7 days it will summarize for each date instead of month.
        *  So instead of getting a list of all absences, it gives a list of total absences per month/date.
        * Returns:
        * A list of absences for each date an absence was recorded, or a summarized duration of absences for a given month.
        */

        [Authorize(Roles = "User")]
        [HttpGet("absence")]
        public IList<AbsenceView> getAbsenceRegister(string filter)
        {
            long tennantId = getTennantId();
            DateTime comparisonDate = compareDates(filter);
            //Gets a list of absences for the given tennant within the specified timelimit, ordered by date(ascending).
            var absence = _warehouseDb.getAllAbsenceFromDate(tennantId, comparisonDate);

            List<AbsenceView> absenceViews = new List<AbsenceView>();
            double totalAbsence = 0;
            if (filter == "thisWeek" || filter == "thisMonth" || filter == "lastThirtyDays" || filter == "lastSevenDays")
            {
                try
                {
                    for (int i = 0; i < absence.Count; i++)
                    {
                        if(absence.Count < 2) {
                            totalAbsence += absence[i].duration;
                                AbsenceView view = new AbsenceView();
                                view.year = absence[i].fromDate.Year;
                                view.month = absence[i].fromDate.Month;
                                view.day = absence[i].fromDate.Day;
                                view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                                view.totalDuration = absence[i].duration;
                                absenceViews.Add(view);
                        }
                        if (i != absence.Count - 1)
                        {
                            //since the list is ordered allready, we can compare current month with next, if it is, add the duration to months total
                            if (absence[i].fromDate.DayOfYear == absence[i + 1].fromDate.DayOfYear)
                            {
                                totalAbsence += absence[i].duration;
                            }
                            // Next absence is a new day, add the current absence we're on and add the view to the new list of views.
                            else
                            {
                                totalAbsence += absence[i].duration;
                                AbsenceView view = new AbsenceView();
                                view.year = absence[i].fromDate.Year;
                                view.month = absence[i].fromDate.Month;
                                view.day = absence[i].fromDate.Day;
                                view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                                view.totalDuration = totalAbsence;
                                absenceViews.Add(view);
                                totalAbsence = 0;
                            }
                        }
                        //last absence has the same day as the one previously added absence
                        else if (absence[i].fromDate.DayOfYear == absence[i - 1].fromDate.DayOfYear)
                        {
                            totalAbsence += absence[i].duration;
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.day = absence[i].fromDate.Day;
                            view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                            view.totalDuration = totalAbsence;
                            absenceViews.Add(view);
                        }
                        //last absence is in a new day
                        else
                        {
                            totalAbsence += absence[i].duration; 
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.day = absence[i].fromDate.Day;
                            view.weekDay = absence[i].fromDate.DayOfWeek.ToString();
                            view.totalDuration = absence[i].duration;
                            absenceViews.Add(view);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
                return absenceViews;
            }
            else  //Monthly-based filter is chosen, now we summarize for each month instead of day.
            {
                try
                {
                    for (int i = 0; i < absence.Count; i++)
                    {
                        if (i != absence.Count - 1)
                        {
                            //since the list is ordered allready, we can compare current month with next, if it is, add the duration to months total
                            if (absence[i].fromDate.Month == absence[i + 1].fromDate.Month)
                            {
                                totalAbsence += absence[i].duration;
                            }
                            // Next absence is a new month, add the current absence we're on and add the view to the new list of views.
                            else
                            {
                                totalAbsence += absence[i].duration;
                                AbsenceView view = new AbsenceView();
                                view.year = absence[i].fromDate.Year;
                                view.month = absence[i].fromDate.Month;
                                view.totalDuration = totalAbsence;
                                absenceViews.Add(view);
                                totalAbsence = 0;
                            }
                        }
                        //last absence has the same month as the one previously added absence
                        else if (absence[i].fromDate.Month == absence[i - 1].fromDate.Month)
                        {
                            totalAbsence += absence[i].duration;
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.totalDuration = totalAbsence;
                            absenceViews.Add(view);
                        }
                        //last absence is in a new month
                        else
                        {
                            totalAbsence += absence[i].duration; 
                            AbsenceView view = new AbsenceView();
                            view.year = absence[i].fromDate.Year;
                            view.month = absence[i].fromDate.Month;
                            view.totalDuration = absence[i].duration;
                            absenceViews.Add(view);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
                return absenceViews;
            }
        }

        /*
        * A method to get all timeregisters in the given filter.
        * Returns:
        * A list of all time registers, the list is not adapted for frontend-view.
        */
        [Authorize(Roles = "User")]
        [HttpGet("timeregister")]
        public List<TimeRegister> getTimeRegisters(string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            long tennantId = getTennantId();
            var timeRegisters = _warehouseDb.TimeRegisters
            .Where(t => t.employee.tennantFK == tennantId && t.recordDate >= comparisonDate)
            .OrderByDescending(d => d.recordDate)
            .ToList();
            return timeRegisters;
        }

        /*
        * A method to fetch all pending orders
        * Returns:
        * A list of all tennants orders that have an end date later than the date of the request.
        * All orders are converted to orderView that only show customer name, jobname and end date.
        */

        [Authorize(Roles = "User")]
        [HttpGet("pendingOrders")]
        public List<OrderView> getPendingOrders()
        {
            long tennantId = getTennantId();
            var orders = _warehouseDb.getPendingOrdersByDescending(tennantId);
            List<OrderView> orderList = new List<OrderView>();
            for (int i = 0; i < orders.Count; i++)
            {
                OrderView order = new OrderView();
                order.clientName = orders[i].client.clientName;
                order.jobname = orders[i].jobName;
                order.endDate = orders[i].endDate;
                orderList.Add(order);
            }
            return orderList;
        }

        /*
        * A method to fetch all orders
        * Returns:
        * A list of orders for given tennant, every order includes the clientname of the client it's bound to
        * and the name of the job that the order was given and the date the order ended.
        */

        [Authorize(Roles = "User")]
        [HttpGet("allOrders")]
        public List<OrderView> getAllOrders()
        {
            long tennantId = getTennantId();
            var orders = _warehouseDb.getOrdersByDescending(tennantId);
            List<OrderView> orderList = new List<OrderView>();

            for (int i = 0; i < orders.Count; i++)
            {
                OrderView order = new OrderView();
                order.clientName = orders[i].client.clientName;
                order.jobname = orders[i].jobName;
                order.endDate = orders[i].endDate;
                orderList.Add(order);
            }
            return orderList;
        }

        /*
        * A method to find all customers for a specific tennant.
        * Returns:
        * A list of client addresses, zipcode, city and type of client
        */

        [Authorize(Roles = "User")]
        [HttpGet("clients")]
        public List<ClientView> getClients()
        {
            long tennantId = getTennantId();
            var clients = _warehouseDb.getAllClients(tennantId);

            List<ClientView> customerList = new List<ClientView>();
            for (int i = 0; i < clients.Count; i++)
            {
                ClientView cust = new ClientView();
                cust.clientName = clients[i].clientName;
                cust.address = clients[i].address;
                cust.zipcode = clients[i].zipcode;
                cust.city = clients[i].city;
                if (clients[i].customer)
                {
                    cust.type = "Customer";
                }
                else
                {
                    cust.type = "Supplier";
                }
                customerList.Add(cust);
            }
            return customerList;
        }

        /*
        * A method to find all clients for each city, for the given tennant
        * Returns:
        * A list of cities with amount of customers for that city
        */
        [Authorize(Roles = "User")]
        [HttpGet("customerzones")]
        public List<ZoneView> getClientZones()
        {
            long tennantId = getTennantId();
            var clients = _warehouseDb.getAllCustomersOrderedByCity(tennantId);

            List<ZoneView> zoneList = new List<ZoneView>();
            int totalClients = 0;
            for (int i = 0; i < clients.Count; i++)
            {
                if (i != clients.Count() - 1)
                {
                    // there are more clients in that city
                    if (clients[i].city == clients[i + 1].city)
                    {
                        totalClients += 1;
                    }
                    // adds the city and number to list, 1 if its alone
                    else
                    {
                        totalClients += 1;
                        ZoneView zone = new ZoneView();
                        zone.city = clients[i].city;
                        zone.totalAmount = totalClients;
                        zoneList.Add(zone);
                        totalClients = 0;
                    }
                }
                //last is not a new city
                else if (clients[i].city == clients[i - 1].city)
                {
                    totalClients += 1;
                    ZoneView zone = new ZoneView();
                    zone.city = clients[i].city;
                    zone.totalAmount = totalClients;
                    zoneList.Add(zone);
                }
                //Last is a new city
                else
                {
                    ZoneView zone = new ZoneView();
                    zone.city = clients[i].city;
                    zone.totalAmount = 1;
                    zoneList.Add(zone);
                }
            }
            return zoneList;
        }

        /*
        * A method to get all clients for a tennant and summarize them based on their type
        * Returns:
        * A statistic of how many customer and suppliers the tennant has
        */
        [Authorize(Roles = "User")]
        [HttpGet("clientnumbers")]
        public ClientStatisticsView getNumberOfClients()
        {
            long tennantId = getTennantId();
            var clients = _warehouseDb.getAllClients(tennantId);

            ClientStatisticsView statisticsView = new ClientStatisticsView();
            statisticsView.numberOfCustomers = 0;
            statisticsView.numberOfSuppliers = 0;
            if (clients != null)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].customer)
                    {
                        statisticsView.numberOfCustomers += 1;
                    }
                    else
                    {
                        statisticsView.numberOfSuppliers += 1;
                    }
                }
            }
            return statisticsView;
        }

        /*
        * A method to get statistics over number of employees and the male to female ratio
        * Returns:
        * Number of female / male Employees
        */
        [Authorize(Roles = "User")]
        [HttpGet("employeenumber")]
        public EmployeeStatView getNumberOfEmployees()
        {
            long tennantId = getTennantId();
            var employees = _warehouseDb.getAllEmployees(tennantId);

            EmployeeStatView statisticsView = new EmployeeStatView();
            statisticsView.numberOfEmployees = employees.Count;
            statisticsView.numberOfMales = 0;
            statisticsView.numberOfFemales = 0;
            if (employees != null)
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    if (employees[i].gender == "male")
                    {
                        statisticsView.numberOfMales += 1;
                    }
                    else
                    {
                        statisticsView.numberOfFemales += 1;
                    }
                }
            }
            return statisticsView;

        }

        /*
        * A method to get the latest balance and budget.
        * Returns:
        * The statbalance, endbalance and periodBalance. 
        */

        [Authorize(Roles = "User")]
        [HttpGet("balance")]
        public List<BnbView> getBalanceAndBudget(string filter)
        {
            DateTime comparisonDate = compareDates(filter);
            long tennantId = getTennantId();
            var balanceAndBudgets = _warehouseDb.GetBalanceAndBudgetsWithFilter(tennantId, comparisonDate);
            List<BnbView> bnbList = new List<BnbView>();
            for (int i = 0; i < balanceAndBudgets.Count; i++)
            {
                BnbView bnb = new BnbView();
                bnb.account = balanceAndBudgets[i].account;
                bnb.startBalance = balanceAndBudgets[i].startBalance;
                bnb.endBalance = balanceAndBudgets[i].endBalance;
                bnb.periodBalance = balanceAndBudgets[i].periodBalance;
                bnbList.Add(bnb);
            }
            return bnbList;
        }

        /*
        * Two functions that takes the first claim from the JWT Token to find which tennant/tennantId
        * their accounts is bound to.  The first function returns the object tennant based on the ID
        * The other function simply returns the tennantId that has been found.
        */
        private long getTennantId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            long tennantId = long.Parse(claim[0].Value);
            return tennantId;
        }
    }
}