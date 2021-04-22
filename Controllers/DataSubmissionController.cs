
using System;
using System.IO;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Datawarehouse_Backend.Context;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace Datawarehouse_Backend.Controllers
{
    [Route("submission/")]
    [ApiController]


    public class DataSubmissionController : ControllerBase
    {

        private readonly IConfiguration _config;

        private readonly WarehouseContext _db;

        public DataSubmissionController(IConfiguration config, WarehouseContext db)
        {
            _config = config;
            _db = db;
        }

        [HttpPost("add")]
        [Consumes("application/json")]
        public IActionResult addData([FromQuery] string apiKey, [FromBody] dynamic data)
        {

            string jsonDataAsString = data + "";
            long tennantId = -1;

            try
            {
                //Checks if the apiKey is valid in the datawarehouse
                if (verifyApiKey(apiKey))
                {


                    ContentsList contentsList = JsonConvert.DeserializeObject<ContentsList>(
                        jsonDataAsString);

                    //Second verification where the apiKey in the URL gets compared with the apiKey
                    //inside the body
                    if (apiKey == contentsList.apiKey)
                    {

                        tennantId = addTennant(contentsList.businessId, contentsList.tennantName, contentsList.apiKey);


                        //Failsafe if addtennant dont update the tennantId
                        //This happend once but never again, and we dont know why or what caused it
                        if (tennantId > 0)
                        {
                            //Adds custumer to datawarehouse
                            for (int i = 0; i < contentsList.Customer.Count; i++)
                            {
                                Customer customer = new Customer();
                                customer = contentsList.Customer[i];
                                customer.tennantFK = tennantId;

                                addCustomer(customer, tennantId);
                            }

                            //Adds Employee to datawarehouse
                            for (int i = 0; i < contentsList.Employee.Count; i++)
                            {
                                Employee employee = new Employee();
                                employee = contentsList.Employee[i];
                                employee.tennantFK = tennantId;

                                addEmployee(employee, tennantId);
                            }

                            //Adds Order to datawarehouse
                            for (int i = 0; i < contentsList.Order.Count; i++)
                            {
                                Order order = new Order();
                                order = contentsList.Order[i];
                                order.tennantFK = tennantId;
                                long customerFK = getCustomerId(order.customerId, tennantId);

                                if (customerFK > -1)
                                {
                                    order.customerFK = customerFK;
                                    _db.Orders.Add(order);
                                    _db.SaveChanges();

                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                            //Adds Invoice inbound to datawarehouse
                            for (int i = 0; i < contentsList.InvoiceInbound.Count; i++)
                            {
                                InvoiceInbound invoice = new InvoiceInbound();
                                invoice = contentsList.InvoiceInbound[i];
                                invoice.tennantFK = tennantId;
                                _db.InvoiceInbounds.Add(invoice);
                            }

                            //Adds Invoice outbound to datawarehouse
                            for (int i = 0; i < contentsList.InvoiceOutbound.Count; i++)
                            {
                                InvoiceOutbound outbound = new InvoiceOutbound();
                                outbound = contentsList.InvoiceOutbound[i];

                                long orderFK = getOrderId(outbound.orderId, tennantId);
                                long customerFK = getCustomerId(outbound.customerId, tennantId);

                                if (orderFK > 0 || customerFK > 0)
                                {
                                    outbound.orderFK = orderFK;
                                    outbound.customerFK = customerFK;
                                    _db.InvoiceOutbounds.Add(outbound);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO LOGGE FEIL
                                }
                            }

                            //Adds Balance and Budget to datawarehouse
                            for (int i = 0; i < contentsList.BalanceAndBudget.Count; i++)
                            {
                                BalanceAndBudget balanceAndBudget = new BalanceAndBudget();
                                balanceAndBudget = contentsList.BalanceAndBudget[i];
                                balanceAndBudget.tennantFK = tennantId;
                                _db.BalanceAndBudgets.Add(balanceAndBudget);
                            }

                            //Adds Absence Register to datawarehouse
                            for (int i = 0; i < contentsList.AbsenceRegister.Count; i++)
                            {
                                AbsenceRegister absenceRegister = new AbsenceRegister();
                                absenceRegister = contentsList.AbsenceRegister[i];

                                long employeeFK = getEmployeeId(absenceRegister.employeeId, tennantId);

                                if (employeeFK > -1)
                                {
                                    absenceRegister.employeeFK = employeeFK;
                                    _db.AbsenceRegisters.Add(absenceRegister);
                                }
                                else
                                {
                                    //TODO Logg feil
                                }

                            }

                            //Adds Accountsreceivable to datawarehouse
                            for (int i = 0; i < contentsList.Accountsreceivable.Count; i++)
                            {
                                AccountsReceivable accountsReceivable = new AccountsReceivable();
                                accountsReceivable = contentsList.Accountsreceivable[i];

                                long customerFK = getCustomerId(accountsReceivable.customerId, tennantId);

                                if (customerFK > -1)
                                {
                                    accountsReceivable.customerFK = customerFK;
                                    _db.AccountsReceivables.Add(accountsReceivable);
                                }
                                else
                                {
                                    //TODO Logg feil
                                }

                            }

                            //Adds TimeRegister to datawarehouse
                            for (int i = 0; i < contentsList.TimeRegister.Count; i++)
                            {
                                TimeRegister timeRegister = new TimeRegister();
                                timeRegister = contentsList.TimeRegister[i];

                                long employeeFK = getEmployeeId(timeRegister.employeeId, tennantId);

                                if (employeeFK > -1)
                                {
                                    timeRegister.employeeFK = employeeFK;
                                    _db.TimeRegisters.Add(timeRegister);
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                        }
                        else
                        {
                            string errorType = "Tennant ID update fail";
                            string errorMessage = "Tennant id ble ikke oppdatert i addData (DataSubmissionController.cs) før behandlingen av data skjedde";

                            //Creates a new errorLog to the datawarehouse
                            logError(errorMessage, errorType);

                        }

                    }

                    //Saves changes to DB if everything is OK
                    _db.SaveChanges();

                }
                /*
                The programm picks up this error if the JsonConvert.DeserializeObject fails.
                Fields that are null or not the expected datatype can cause this
                */
            }
            catch (DbUpdateException e)
            {
                ErrorLog errorLog = new ErrorLog();

                string errorType = e.GetType().ToString();
                string errorMessage =
                "Failed when trying to save changes to the database. This might be an result of required fields being null. TennantId: " +
                tennantId;
                
                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);

            }
            catch (JsonSerializationException e)
            {

                ErrorLog errorLog = new ErrorLog();
                string errorType = e.GetType().ToString();
                string errorMessage = e.Message.ToString() + " businessId: " + apiKey;
            
                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);


                /*
                This can be caused if some fields managed to stay null after JsonConvert.DeserializeObject
                and the programm tries to create a new object with that information and add it to the database
                where it is required to have a value. 
                */
            }
            catch (NullReferenceException e)
            {
                ErrorLog errorLog = new ErrorLog();

                string errorType = e.GetType().ToString();
                string errorMessage = e.ToString() + " Tennant ID: " + tennantId;
                
                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);
            }
            catch (InvalidbusinessIdOrApiKeyException)
            {
                /*
                This catch is only used for skipping the processing of incoming data.
                There is only one place where it is thrown, and that is when the businessID
                or apiKey is invalid. It gets logged in the database, and jumpes out of the Try.
                */
            }

            /*
            Systemet får en exception etter at et Required field er tom. Denne exception blir logga i terminal og i postman
            Etter dette går den videre til catch, hvor alt funker som det skal helt til den SaveChanges hvor samme feilmelding
            dukker opp selv om alle endringer i try egentlig skal bli kastet og ikke lagra. Aner ikke hvordan jeg skal løse dette 
            eller hva det kommer av, så bare å rope ut hvis noen har peiling...
            */

            catch (Exception e)
            {
                ErrorLog errorLog = new ErrorLog();

                string errorType = e.GetType().ToString();
                string errorMessage =
                "Failed when trying to save changes to the database. This might be an result of required fields being null. TennantId: " +
                tennantId;

                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);

            }

            return Ok();
        }

        /*
        This api is used when a tennant has bought the solution. This registers the tennant to the datawarehouse
        and the system is after that ready to handle incoming data to the datawarehouse.
        */
        //[Authorize(Roles = "User")]
        [HttpPost("registerTennant")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult registerTennant(
            [FromForm] string tennantName,
            [FromForm] string businessId,
            [FromForm] string apiKey)
        {
            try
            {

                //If no apiKey is send with the request
                if (apiKey == null)
                {

                    ErrorLog errorLog = new ErrorLog();

                    string errorType = "API-Key empty when register tennant";
                    string errorMessage = "API-nøkkelen er enten tom eller ikke presentert på riktig måte.\nAPI-key: null";

                    //Creates a new errorLog to the datawarehouse
                    logError(errorMessage, errorType);

                }
                else
                {
                    //Creates a new tennant object with the information
                    Tennant tennant = new Tennant();
                    tennant.tennantName = tennantName;
                    tennant.businessId = businessId;
                    tennant.apiKey = apiKey;

                    //Adds it to the database and saves the changes
                    _db.Tennants.Add(tennant);
                    _db.SaveChanges();
                }
               
            } //If it catches an error, it will log it in the datawarehouse
            catch (Exception e)
            {
                ErrorLog errorLog = new ErrorLog();

                string errorType = e.GetType().ToString();
                string errorMessage = e.ToString();

                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);
            }


            return Ok();
        }


        /* 
        This function is called whenever the datawarehouse receives data.
        If the incoming data doesn't have a tennant to connect the data to, it will create a tennant based on
        the incoming information (businessname, businessID and API-key).
         */
        private long addTennant(string bId, string bName, string apiKey)
        {
            ErrorLog errorLog = new ErrorLog();
            var business = _db.Tennants.Where(b => b.businessId == bId).FirstOrDefault<Tennant>();
            if (business == null && bId != null && apiKey != null && bId != "" && apiKey != "")
            {
                Tennant tennant = new Tennant();
                tennant.businessId = bId;
                tennant.tennantName = bName;
                tennant.apiKey = apiKey;
                _db.Tennants.Add(tennant);
                _db.SaveChanges();

                return tennant.id;

            }
            else if (bId == null || bId == "")
            {
                string errorType = "BusinessId fail";
                string errorMessage = "BusinessId er enten tom eller ikke presentert på riktig måte.\nBID: " + bId;

                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);

                //Throws an Exception so it does not try to process incoming data that will lead to new Exception
                throw new InvalidbusinessIdOrApiKeyException();
            }
            else if (apiKey == null || apiKey == "")
            {
                string errorType = "API-Key fail";
                string errorMessage = "API-nøkkelen er enten tom eller ikke presentert på riktig måte.\nAPI-key: " + apiKey;

                //Creates a new errorLog to the datawarehouse
                logError(errorMessage, errorType);

                //Throws an Exception so it does not try to process incoming data that will lead to new Exception
                throw new InvalidbusinessIdOrApiKeyException();
            }
            else if (business != null)
            {
                Console.WriteLine("Tennant found, submitting data...");
            }
            return business.id;
        }
        private long addCustomer(Customer customer, long tennantFK)
        {
            ErrorLog errorLog = new ErrorLog();
            Customer databaseCustomer = _db.Customers
            .Where(c => c.customerId == customer.customerId)
            .Where(t => t.tennantFK == tennantFK).FirstOrDefault<Customer>();
            if (databaseCustomer == null)
            {
                Customer customer1 = new Customer();
                customer1 = customer;
                customer1.tennantFK = tennantFK;
                _db.Customers.Add(customer1);

                _db.SaveChanges();

                return customer1.id;
            }
            return databaseCustomer.id;
        }

        private long addEmployee(Employee employee, long tennantFK)
        {
            ErrorLog errorLog = new ErrorLog();
            Employee databaseEmployee = _db.Employees
            .Where(c => c.employeeId == employee.employeeId)
            .Where(t => t.tennantFK == tennantFK).FirstOrDefault<Employee>();
            if (databaseEmployee == null)
            {
                Employee employee1 = new Employee();
                employee1 = employee;
                employee1.tennantFK = tennantFK;
                _db.Employees.Add(employee);

                _db.SaveChanges();

                return employee.id;
            }
            return databaseEmployee.id;
        }


        private long getEmployeeId(long cordelId, long tennantId)
        {
            Employee databaseEmployee = _db.Employees
            .Where(c => c.employeeId == cordelId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Employee>();

            if (databaseEmployee != null)
            {
                return databaseEmployee.id;
            }
            Console.WriteLine("Employee er null");
            return -1;
        }

        private long getOrderId(long cordelId, long tennantId)
        {
            Order databaseOrder = _db.Orders
            .Where(c => c.orderId == cordelId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Order>();

            if (databaseOrder != null)
            {
                return databaseOrder.id;
            }
            Console.WriteLine("Order er null");
            return -1;
        }

        private long getCustomerId(long cordelId, long tennantId)
        {
            Customer databaseCustomer = _db.Customers
            .Where(c => c.customerId == cordelId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Customer>();

            if (databaseCustomer != null)
            {
                return databaseCustomer.id;
            }
            Console.WriteLine("Customer er null");
            return -1;
        }


        //This function checks if there is a tennant registert with the apiKey
        //If there is, true will be returned and the handling of data will start
        private bool verifyApiKey(string apiKey)
        {
            Tennant tennant = _db.Tennants
            .Where(t => t.apiKey == apiKey).FirstOrDefault<Tennant>();

            //Checks if there is a tennant with that api key
            if (tennant != null)
            {
                return true;
            }

            return false;
        }

        /*
        This function creats a new errorLog, and uses the information in the 
        parameters to create a new errorLog in the datawarehouse, and saves the changes
        */
        private void logError(string errorMessage, string errorType) 
        {
            ErrorLog errorLog = new ErrorLog();

            DateTime timeOfError = DateTime.Now;

            errorLog.errorType = errorType;
            errorLog.errorMessage = errorMessage;
            errorLog.timeOfError = timeOfError;

            _db.ErrorLogs.Add(errorLog);
            _db.SaveChanges();
        }
    }



}