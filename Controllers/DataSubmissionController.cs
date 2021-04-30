
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


                    tennantId = addTennant("", "", apiKey);

                    //Second verification where the apiKey in the URL gets compared with the apiKey
                    //inside the body
                    if (apiKey == contentsList.apiKey)
                    {
                        //Failsafe if addtennant dont update the tennantId
                        //This happend once but never again, and we dont know why or what caused it
                        if (tennantId > 0)
                        {
                            //Adds Client to datawarehouse
                            for (int i = 0; i < contentsList.Client.Count; i++)
                            {
                                Client client = new Client();
                                client = contentsList.Client[i];

                                findClient(client, tennantId);

                                client.tennantFK = tennantId;
                                addClient(client, tennantId);
                            }

                            //Adds order to the datawarehouse
                            for (int i = 0; i < contentsList.Order.Count; i++)
                            {
                                Order order = new Order();
                                order = contentsList.Order[i];
                                order.tennantFK = tennantId;

                                findOrder(order, tennantId);

                                long ClientFK = getClientId(order.clientId, tennantId);

                                if (ClientFK > -1)
                                {
                                    order.clientFK = ClientFK;
                                    _db.Orders.Add(order);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                            for (int i = 0; i < contentsList.Employee.Count; i++)
                            {                                
                                Employee employee = new Employee();
                                employee = contentsList.Employee[i];
                                employee.tennantFK = tennantId;

                                findEmployee(employee, tennantId);

                                addEmployee(employee, tennantId);
                            }

                            //Adds Absence Register to datawarehouse
                            for (int i = 0; i < contentsList.AbsenceRegister.Count; i++)
                            {
                                AbsenceRegister absenceRegister = new AbsenceRegister();
                                absenceRegister = contentsList.AbsenceRegister[i];

                                findAbsenceRegister(absenceRegister, tennantId);

                                long employeeFK = getEmployeeId(absenceRegister.employeeId, tennantId);

                                if (employeeFK > -1)
                                {
                                    absenceRegister.employeeFK = employeeFK;
                                    _db.AbsenceRegisters.Add(absenceRegister);
                                    _db.SaveChanges();
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

                                findTimeregister(timeRegister, tennantId);

                                long employeeFK = getEmployeeId(timeRegister.employeeId, tennantId);

                                if (employeeFK > -1)
                                {
                                    timeRegister.employeeFK = employeeFK;
                                    _db.TimeRegisters.Add(timeRegister);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                            //Adds Balance and Budget to datawarehouse
                            for (int i = 0; i < contentsList.BalanceAndBudget.Count; i++)
                            {
                                BalanceAndBudget balanceAndBudget = new BalanceAndBudget();
                                balanceAndBudget = contentsList.BalanceAndBudget[i];
                                balanceAndBudget.tennantFK = tennantId;

                                findBalanceAndBudget(balanceAndBudget, tennantId);

                                _db.BalanceAndBudgets.Add(balanceAndBudget);
                                _db.SaveChanges();
                            }

                            for (int i = 0; i < contentsList.Voucher.Count; i++)
                            {
                                Voucher voucher = new Voucher();
                                voucher = contentsList.Voucher[i];

                                findVoucher(voucher, tennantId);

                                long clientFK = getClientId(voucher.clientId, tennantId);
                                if (clientFK != -1)
                                {
                                    voucher.clientFK = clientFK;
                                    _db.Vouchers.Add(voucher);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }


                            for (int i = 0; i < contentsList.Invoice.Count; i++)
                            {
                                Invoice invoice = new Invoice();
                                invoice = contentsList.Invoice[i];

                                //If the invoice is not stored in the datawarehouse already
                                findInvoice(invoice, tennantId);

                                long voucherFK = getVoucherId(invoice.voucherId, tennantId);
                                if (voucherFK != -1)
                                {
                                    Console.WriteLine("\\\\\\\\\\\\\\\\\\");
                                    Console.WriteLine(voucherFK);
                                    invoice.voucherFK = voucherFK;
                                    _db.Invoices.Add(invoice);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }

                            }

                            for (int i = 0; i < contentsList.InvoiceLine.Count; i++)
                            {
                                InvoiceLine invoiceLines = new InvoiceLine();
                                invoiceLines = contentsList.InvoiceLine[i];

                                findInvoiceLine(invoiceLines, tennantId);
                                Console.WriteLine("---------------" + invoiceLines.invoiceLineId);

                                long invoiceFK = getInvoiceId(invoiceLines.invoiceId, tennantId);
                                Console.WriteLine("++++++++++ " + invoiceFK);
                                if (invoiceFK != -1)
                                {
                                    invoiceLines.invoiceFK = invoiceFK;
                                    _db.InvoiceLines.Add(invoiceLines);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                            //Adds a financial year to the datawarehouse
                            for (int i = 0; i < contentsList.FinancialYear.Count; i++)
                            {
                                FinancialYear financialYear = new FinancialYear();
                                financialYear = contentsList.FinancialYear[i];

                                findFinancialYear(financialYear, tennantId);

                                financialYear.tennantFK = tennantId;

                                _db.FinancialYears.Add(financialYear);
                                _db.SaveChanges();
                            }

                            for (int i = 0; i < contentsList.Account.Count; i++)
                            {
                                Account account = new Account();
                                account = contentsList.Account[i];

                                findAccount(account, tennantId);

                                long financialYearFK = getFinancialYearId(account.financialYearid, tennantId);
                                if (financialYearFK != -1)
                                {
                                    account.financialYearFK = financialYearFK;
                                    _db.Accounts.Add(account);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //TODO Logg feil
                                }
                            }

                            for (int i = 0; i < contentsList.Post.Count; i++)
                            {
                                Post post = new Post();
                                post = contentsList.Post[i];

                                findPost(post, tennantId);

                                long voucherFK = getVoucherId(post.voucherId, tennantId);
                                long accountFK = getAccountId(post.accountId, tennantId);
                                if (voucherFK != -1)
                                {
                                    post.voucherFK = voucherFK;
                                    post.accountFK = accountFK;
                                    _db.Posts.Add(post);
                                    _db.SaveChanges();
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
            Etter dette går den videre til catch, hvor alt funker som det skal helt til den prøver SaveChanges hvor samme feilmelding
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
            var business = _db.Tennants.Where(b => b.apiKey == apiKey).FirstOrDefault<Tennant>();
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
            /*else if (bId == null || bId == "")
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
            }*/
            return business.id;
        }
        private long addClient(Client client, long tennantFK)
        {
            ErrorLog errorLog = new ErrorLog();
            Client databaseClient = _db.Clients
            .Where(c => c.clientId == client.clientId)
            .Where(t => t.tennantFK == tennantFK).FirstOrDefault<Client>();
            if (databaseClient == null)
            {
                Client client1 = new Client();
                client1 = client;
                client1.tennantFK = tennantFK;
                _db.Clients.Add(client1);
                _db.SaveChanges();

                return client1.id;
            }
            return databaseClient.id;
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
            return -1;
        }

        /*
        The function getClientId checks for an existing client in the datawarehouse,
        and then return the unique back if it exists, or return -1 if it does not exist
        */
        private long getClientId(long cordelId, long tennantId)
        {
            Client databaseClient = _db.Clients
            .Where(c => c.clientId == cordelId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Client>();

            if (databaseClient != null)
            {
                return databaseClient.id;
            }
            return -1;
        }

        private long getVoucherId(long cordelId, long tennantId)
        {
            Voucher databaseVoucher = _db.Vouchers
            .Where(v => v.voucherId == cordelId)
            .Where(t => t.client.tennantFK == tennantId).FirstOrDefault<Voucher>();

            if (databaseVoucher != null)
            {
                return databaseVoucher.id;
            }
            return -1;
        }

        private long getInvoiceId(long cordelId, long tennantId)
        {
            Invoice databaseInvoice = _db.Invoices
            .Where(c => c.invoiceId == cordelId)
            .Where(t => t.voucher.client.tennantFK == tennantId).FirstOrDefault<Invoice>();

            if (databaseInvoice != null)
            {
                return databaseInvoice.voucherFK;
            }
            return -1;
        }

        private long getFinancialYearId(long cordelId, long tennantId)
        {
            FinancialYear databaseFinancialYear = _db.FinancialYears
            .Where(c => c.financialYearId == cordelId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<FinancialYear>();

            if (databaseFinancialYear != null)
            {
                return databaseFinancialYear.id;
            }
            return -1;
        }

        private long getAccountId(long cordelId, long tennantId)
        {
            Account databaseAccount = _db.Accounts
            .Where(c => c.accountId == cordelId)
            .Where(t => t.financialYear.tennantFK == tennantId).FirstOrDefault<Account>();

            if (databaseAccount != null)
            {
                return databaseAccount.id;
            }
            return -1;
        }

        private void findClient(Client client, long tennantId)
        {
            Client databaseClient = _db.Clients
            .Where(c => c.clientId == client.clientId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Client>();

            if(databaseClient != null) 
            {
                _db.Remove(databaseClient);
                _db.SaveChanges();
            }  
        }

        private void findVoucher(Voucher voucher, long tennantId)
        {
            Voucher databaseVoucher = _db.Vouchers
            .Where(c => c.voucherId == voucher.voucherId)
            .Where(t => t.client.tennantFK == tennantId).FirstOrDefault<Voucher>();

            if(databaseVoucher != null) 
            {
                _db.Remove(databaseVoucher);
                _db.SaveChanges();
            }
        }

        private void findInvoice(Invoice invoice, long tennantId)
        {
            Invoice databaseInvoice = _db.Invoices
            .Where(c => c.invoiceId == invoice.invoiceId)
            .Where(t => t.voucher.client.tennantFK == tennantId).FirstOrDefault<Invoice>();

            if(databaseInvoice != null) 
            {
                _db.Remove(databaseInvoice);
                _db.SaveChanges();
            }
        }

        private void findInvoiceLine(InvoiceLine invoiceLine, long tennantId)
        {
            Console.WriteLine(invoiceLine.invoiceLineId);
            InvoiceLine databaseInvoiceLine = _db.InvoiceLines
            .Where(c => c.invoiceLineId == invoiceLine.invoiceLineId)
            .Where(t => t.invoice.voucher.client.tennantFK == tennantId).FirstOrDefault<InvoiceLine>();

            
            if(databaseInvoiceLine != null) 
            {
                Console.WriteLine("Removed InvoiceLine: " + databaseInvoiceLine.invoiceLineId);
                _db.Remove(databaseInvoiceLine);
                _db.SaveChanges();
            }
        }

        private void findFinancialYear(FinancialYear financialYear, long tennantId)
        {
            FinancialYear databaseFinancialYear = _db.FinancialYears
            .Where(c => c.financialYearId == financialYear.financialYearId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<FinancialYear>();

            if(databaseFinancialYear != null) 
            {
                _db.Remove(databaseFinancialYear);
                _db.SaveChanges();
            }
        }

        private void findAccount(Account account, long tennantId)
        {
            Account databaseAccount = _db.Accounts
            .Where(c => c.accountId == account.accountId)
            .Where(t => t.financialYear.tennantFK == tennantId).FirstOrDefault<Account>();

            if(databaseAccount != null) 
            {
                _db.Remove(databaseAccount);
                _db.SaveChanges();
            }
        }

        private void findPost(Post post, long tennantId)
        {
            Post databasePost = _db.Posts
            .Where(c => c.postId == post.postId)
            .Where(t => t.account.financialYear.tennantFK == tennantId).FirstOrDefault<Post>();

            if(databasePost != null) 
            {
                _db.Remove(databasePost);
                _db.SaveChanges();
            }
        }
        private void findBalanceAndBudget(BalanceAndBudget balanceAndBudget, long tennantId)
        {
            BalanceAndBudget databaseBalanceAndBudget = _db.BalanceAndBudgets
            .Where(c => c.tennantFK == tennantId).FirstOrDefault<BalanceAndBudget>();

            if(databaseBalanceAndBudget != null) 
            {
                _db.Remove(databaseBalanceAndBudget);
                _db.SaveChanges();
            }
        }
        private void findOrder(Order order, long tennantId)
        {
            Order databaseOrder = _db.Orders
            .Where(c => c.orderId == order.orderId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Order>();

            if(databaseOrder != null) 
            {
                _db.Remove(databaseOrder);
                _db.SaveChanges();
            }
        }
        private void findEmployee(Employee employee, long tennantId)
        {
            Employee databaseEmployee = _db.Employees
            .Where(c => c.employeeId == employee.employeeId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Employee>();

            if(databaseEmployee != null) 
            {
                _db.Remove(databaseEmployee);
                _db.SaveChanges();
            }
        }
        private void findAbsenceRegister(AbsenceRegister absenceRegister, long tennantId)
        {
            AbsenceRegister databaseAbsenceRegister = _db.AbsenceRegisters
            .Where(c => c.absenceRegisterId == absenceRegister.absenceRegisterId)
            .Where(t => t.employee.tennantFK == tennantId).FirstOrDefault<AbsenceRegister>();

            if(databaseAbsenceRegister != null) 
            {
                _db.Remove(databaseAbsenceRegister);
                _db.SaveChanges();
            }
        }
        private void findTimeregister(TimeRegister timeRegister, long tennantId)
        {
            TimeRegister databaseTimeRegister = _db.TimeRegisters
            .Where(c => c.timeRegisterId == timeRegister.timeRegisterId)
            .Where(t => t.employee.tennantFK == tennantId).FirstOrDefault<TimeRegister>();

            if(databaseTimeRegister != null) 
            {
                _db.Remove(databaseTimeRegister);
                _db.SaveChanges();
            }
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