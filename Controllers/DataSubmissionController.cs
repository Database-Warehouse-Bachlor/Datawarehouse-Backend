
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

                    //Gets the tennantId that is connected to the ApiKey
                    tennantId = getTennantId(apiKey);

                    //Second verification where the apiKey in the URL gets compared with the apiKey
                    //inside the body
                    if (apiKey == contentsList.apiKey)
                    {
                        //Failsafe if addtennant dont update the tennantId
                        //This happend once but never again, and we dont know why or what caused it
                        if (tennantId > 0)
                        {
                            //Adds Client to datawarehouse
                            for (int i = 0; i < contentsList.Clients.Count; i++)
                            {
                                //Creates a new Client object
                                Client client = new Client();
                                //Connects the new Client object to data from the request(Incoming data)
                                client = contentsList.Clients[i];

                                //Checks if the Client exists in the datawarehouse
                                findClient(client, tennantId);

                                //Sets the client's tennant foreign key to tennantId
                                client.tennantFK = tennantId;
                                //Adds the Client to the datawarehouse
                                _db.Clients.Add(client);
                                _db.SaveChanges();
                            }

                            //Adds order to the datawarehouse
                            for (int i = 0; i < contentsList.Orders.Count; i++)
                            {
                                //Creates a new Order object
                                Order order = new Order();
                                //Connects the new Order object to data from the request(Incoming data)
                                order = contentsList.Orders[i];

                                //Sets the orders's tennant foreign key to tennantId
                                order.tennantFK = tennantId;

                                //Checks if the Order exists in the datawarehouse
                                findOrder(order, tennantId);

                                //Gets the clientFk that it will be connected to
                                long ClientFK = getClientId(order.clientId, tennantId);

                                //If it finds a client to connect to, it will add it to the datawarehouse
                                if (ClientFK > -1)
                                {
                                    //Sets the clientFK
                                    order.clientFK = ClientFK;
                                    //Adds to the datawarehouse
                                    _db.Orders.Add(order);
                                    _db.SaveChanges();
                                }
                                //If it does not find a client to connect to, it will logg an error
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "Order - Client Connection";
                                    string errorMessage = "There is no Client with Id: "
                                        + order.clientId
                                        + " that orderId: "
                                        + order.orderId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds Employee to the datawarehouse
                            for (int i = 0; i < contentsList.Employees.Count; i++)
                            {
                                //Creates a new Employee object
                                Employee employee = new Employee();
                                //Connects the new Employee object to data from the request(Incoming data)
                                employee = contentsList.Employees[i];

                                //Sets the employee's tennant foreign key to tennantId
                                employee.tennantFK = tennantId;

                                //Checks if the Employee exists in the datawarehouse
                                findEmployee(employee, tennantId);

                                //Adds the Client to the datawarehouse
                                _db.Employees.Add(employee);
                                _db.SaveChanges();
                            }

                            //Adds Absence Register to datawarehouse
                            for (int i = 0; i < contentsList.AbsenceRegisters.Count; i++)
                            {
                                //Creates a new AbsenceRegister object
                                AbsenceRegister absenceRegister = new AbsenceRegister();
                                //Connects the new AbsenceRegister object to data from the request(Incoming data)
                                absenceRegister = contentsList.AbsenceRegisters[i];

                                //Checks if the AbsenceRegister exists in the datawarehouse
                                findAbsenceRegister(absenceRegister, tennantId);

                                //Gets the employeeFK that it will be connected to
                                long employeeFK = getEmployeeId(absenceRegister.employeeId, tennantId);

                                //If it finds a employee to connect to, it will add it to the datawarehouse
                                if (employeeFK > -1)
                                {
                                    //Sets the employeeFK
                                    absenceRegister.employeeFK = employeeFK;

                                    //Adds the AbsenceRegister to the datawarehouse
                                    _db.AbsenceRegisters.Add(absenceRegister);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "AbsenceRegister - Employee Connection";
                                    string errorMessage = "There is no Employee with Id: "
                                        + absenceRegister.employeeId
                                        + " that absenceRegisterId: "
                                        + absenceRegister.absenceRegisterId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds TimeRegister to datawarehouse
                            for (int i = 0; i < contentsList.TimeRegisters.Count; i++)
                            {
                                //Creates a new TimeRegister object
                                TimeRegister timeRegister = new TimeRegister();
                                //Connects the new TimeRegister object to data from the request(Incoming data)
                                timeRegister = contentsList.TimeRegisters[i];

                                //Checks if the TimeRegister exists in the datawarehouse
                                findTimeregister(timeRegister, tennantId);

                                //Gets the employeeFK that it will be connected to
                                long employeeFK = getEmployeeId(timeRegister.employeeId, tennantId);

                                //If it finds a employee to connect to, it will add it to the datawarehouse
                                if (employeeFK > -1)
                                {
                                    //Sets the employeeFK
                                    timeRegister.employeeFK = employeeFK;
                                    
                                    //Adds the TimeRegister to the datawarehouse
                                    _db.TimeRegisters.Add(timeRegister);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "TimeRegister - Employee Connection";
                                    string errorMessage = "There is no Employee with Id: "
                                        + timeRegister.employeeId
                                        + " that timeRegisterId: "
                                        + timeRegister.timeRegisterId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds Balance and Budget to datawarehouse
                            for (int i = 0; i < contentsList.BalanceAndBudgets.Count; i++)
                            {
                                //Creates a new BalanceAndBudget object
                                BalanceAndBudget balanceAndBudget = new BalanceAndBudget();

                                //Connects the new BalanceAndBudget object to data from the request(Incoming data)
                                balanceAndBudget = contentsList.BalanceAndBudgets[i];
                                
                                //Checks if the BalanceAndBudget exists in the datawarehouse
                                findBalanceAndBudget(balanceAndBudget, tennantId);

                                //Sets the tennantFK
                                balanceAndBudget.tennantFK = tennantId;

                                //Adds the BalanceAndBudget to the datawarehouse
                                _db.BalanceAndBudgets.Add(balanceAndBudget);
                                _db.SaveChanges();
                            }

                            //Adds Voucher to datawarehouse
                            for (int i = 0; i < contentsList.Vouchers.Count; i++)
                            {
                                //Creates a new Voucher object
                                Voucher voucher = new Voucher();
                                //Connects the new Voucher object to data from the request(Incoming data)
                                voucher = contentsList.Vouchers[i];

                                //Checks if the Voucher exists in the datawarehouse
                                findVoucher(voucher, tennantId);

                                //Gets the employeeFK that it will be connected to
                                long clientFK = getClientId(voucher.clientId, tennantId);
                                //If it finds a client to connect to, it will add it to the datawarehouse
                                if (clientFK != -1)
                                {
                                    //Sets the clientFK
                                    voucher.clientFK = clientFK;

                                    //Adds the Voucher to the datawarehouse
                                    _db.Vouchers.Add(voucher);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "Voucher - Client Connection";
                                    string errorMessage = "There is no Client with Id: "
                                        + voucher.clientId
                                        + " that voucherId: "
                                        + voucher.voucherId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds Voucher to datawarehouse
                            for (int i = 0; i < contentsList.Invoices.Count; i++)
                            {
                                //Creates a new Invoice object
                                Invoice invoice = new Invoice();
                                //Connects the new Invoice object to data from the request(Incoming data)
                                invoice = contentsList.Invoices[i];

                                //Checks if the Invoice exists in the datawarehouse
                                findInvoice(invoice, tennantId);

                                //Gets the voucherFK that it will be connected to
                                long voucherFK = getVoucherId(invoice.voucherId, tennantId);
                                
                                //If it finds a voucher to connect to, it will add it to the datawarehouse
                                if (voucherFK != -1)
                                {
                                    //Sets the voucherFK
                                    invoice.voucherFK = voucherFK;

                                    //Adds the Voucher to the datawarehouse
                                    _db.Invoices.Add(invoice);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "Invoice - Voucher Connection";
                                    string errorMessage = "There is no Voucher with Id: "
                                        + invoice.voucherId
                                        + " that invoiceId: "
                                        + invoice.invoiceId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }

                            }

                            for (int i = 0; i < contentsList.InvoiceLines.Count; i++)
                            {
                                //Creates a new InvoiceLine object
                                InvoiceLine invoiceLines = new InvoiceLine();
                                //Connects the new InvoiceLine object to data from the request(Incoming data)
                                invoiceLines = contentsList.InvoiceLines[i];

                                //Checks if the InvoiceLine exists in the datawarehouse
                                findInvoiceLine(invoiceLines, tennantId);

                                //Gets the invoiceFK that it will be connected to
                                long invoiceFK = getInvoiceId(invoiceLines.invoiceId, tennantId);

                                //If it finds a Invoice to connect to, it will add it to the datawarehouse
                                if (invoiceFK != -1)
                                {
                                    //Sets the invoiceFK
                                    invoiceLines.invoiceFK = invoiceFK;

                                    //Adds the Invoice to the datawarehouse
                                    _db.InvoiceLines.Add(invoiceLines);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "InvoiceLine - Invoice Connection";
                                    string errorMessage = "There is no Invoice with Id: "
                                        + invoiceLines.invoiceId
                                        + " that invoiceLineId: "
                                        + invoiceLines.invoiceLineId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds a FinancialYear to the datawarehouse
                            for (int i = 0; i < contentsList.FinancialYears.Count; i++)
                            {
                                //Creates a new FinancialYear object
                                FinancialYear financialYear = new FinancialYear();
                                //Connects the new FinancialYear object to data from the request(Incoming data)
                                financialYear = contentsList.FinancialYears[i];

                                //Checks if the FinancialYear exists in the datawarehouse
                                findFinancialYear(financialYear, tennantId);

                                //Gets the tennantFK that it will be connected to
                                financialYear.tennantFK = tennantId;

                                //Adds the FinancialYear to the datawarehouse
                                _db.FinancialYears.Add(financialYear);
                                _db.SaveChanges();
                            }

                            //Adds a Account year to the datawarehouse
                            for (int i = 0; i < contentsList.Accounts.Count; i++)
                            {
                                //Creates a new Account object
                                Account account = new Account();
                                //Connects the new Account object to data from the request(Incoming data)
                                account = contentsList.Accounts[i];

                                //Checks if the Account exists in the datawarehouse
                                findAccount(account, tennantId);

                                //Gets the invoiceFK that it will be connected to
                                long financialYearFK = getFinancialYearId(account.financialYearid, tennantId);

                                //If it finds a FinancialYear to connect to, it will add it to the datawarehouse
                                if (financialYearFK != -1)
                                {
                                    //Gets the financialYearFK that it will be connected to
                                    account.financialYearFK = financialYearFK;

                                    //Adds the Account to the datawarehouse
                                    _db.Accounts.Add(account);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "Account - FinancialYear Connection";
                                    string errorMessage = "There is no FinancialYear with Id: "
                                        + account.financialYearid
                                        + " that accountId: "
                                        + account.accountId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
                                }
                            }

                            //Adds a Post year to the datawarehouse
                            for (int i = 0; i < contentsList.Posts.Count; i++)
                            {
                                //Creates a new Post object
                                Post post = new Post();
                                //Connects the new Account object to data from the request(Incoming data)
                                post = contentsList.Posts[i];

                                //Checks if the Post exists in the datawarehouse
                                findPost(post, tennantId);

                                //Gets the voucherFK that it will be connected to
                                long voucherFK = getVoucherId(post.voucherId, tennantId);
                                //Gets the accountFK that it will be connected to
                                long accountFK = getAccountId(post.accountId, tennantId);

                                //If it finds a Voucher to connect to, it will add it to the datawarehouse
                                if (voucherFK != -1)
                                {
                                    //If it finds an Account to connect to, it will add it to the datawarehouse
                                    if (accountFK != -1)
                                    {
                                        //Gets the voucherFK that it will be connected to
                                        post.voucherFK = voucherFK;
                                        //Gets the accountFK that it will be connected to
                                        post.accountFK = accountFK;

                                        //Adds the Post to the datawarehouse
                                        _db.Posts.Add(post);
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        //Sets the errorType and ErrorMessage
                                        string errorType = "Post - Account Connection";
                                        string errorMessage = "There is no Account with Id: "
                                            + post.accountId
                                            + " that postId: "
                                            + post.postId +
                                            " can be connected to. TennantId: " + tennantId;

                                        //Creates a new errorLog to the datawarehouse
                                        logError(errorMessage, errorType);

                                        //Throws a new exception so the program dont handle more data
                                        throw new InvalidModelFK();
                                    }

                                }
                                else
                                {
                                    //Sets the errorType and ErrorMessage
                                    string errorType = "Post - Voucher Connection";
                                    string errorMessage = "There is no Voucher with Id: "
                                        + post.voucherId
                                        + " that postId: "
                                        + post.postId +
                                        " can be connected to. TennantId: " + tennantId;

                                    //Creates a new errorLog to the datawarehouse
                                    logError(errorMessage, errorType);

                                    //Throws a new exception so the program dont handle more data
                                    throw new InvalidModelFK();
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
            catch (InvalidModelFK)
            {
                /*
                This catch is only used for skipping rest of the processing of incoming data.
                This exception is only thrown when the program cannot connect one object to another in the database
                because the Id and FK dont connect.
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
        If the incoming data doesn't have a tennant to connect the data to, it will return -1.
        If the incoming data does have a tennant to connect the data to, it will return the tennantId
         */
        private long getTennantId(string apiKey)
        {
            //Checks the datawarehouse for a tennant with the same apiKey
            var tennant = _db.Tennants.Where(b => b.apiKey == apiKey).FirstOrDefault<Tennant>();

            //If it does not find any tennant, it returns -1
            if (tennant == null)
            {
                return -1;
            }

            //If it finds the tennant, it returns the tennantId
            return tennant.id;
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

        /*
        The function checks if there is any clients in the datawarehouse that has the same clientId
        and is connected to the same tennant. If there is, the Client will be removed so the 
        updated data can be added later.
        */
        private void findClient(Client client, long tennantId)
        {
            //Checks the datawarehouse for a client with the same clientId and tennantId
            Client databaseClient = _db.Clients
            .Where(c => c.clientId == client.clientId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<Client>();

            //If it finds a Client, it will be deleted
            if (databaseClient != null)
            {
                //Deletes the Client
                _db.Remove(databaseClient);
                _db.SaveChanges();
            }
        }

        private void findVoucher(Voucher voucher, long tennantId)
        {
            Voucher databaseVoucher = _db.Vouchers
            .Where(c => c.voucherId == voucher.voucherId)
            .Where(t => t.client.tennantFK == tennantId).FirstOrDefault<Voucher>();

            if (databaseVoucher != null)
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

            if (databaseInvoice != null)
            {
                _db.Remove(databaseInvoice);
                _db.SaveChanges();
            }
        }

        private void findInvoiceLine(InvoiceLine invoiceLine, long tennantId)
        {
            InvoiceLine databaseInvoiceLine = _db.InvoiceLines
            .Where(c => c.invoiceLineId == invoiceLine.invoiceLineId)
            .Where(t => t.invoice.voucher.client.tennantFK == tennantId).FirstOrDefault<InvoiceLine>();


            if (databaseInvoiceLine != null)
            {
                _db.Remove(databaseInvoiceLine);
                _db.SaveChanges();
            }
        }

        private void findFinancialYear(FinancialYear financialYear, long tennantId)
        {
            FinancialYear databaseFinancialYear = _db.FinancialYears
            .Where(c => c.financialYearId == financialYear.financialYearId)
            .Where(t => t.tennantFK == tennantId).FirstOrDefault<FinancialYear>();

            if (databaseFinancialYear != null)
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

            if (databaseAccount != null)
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

            if (databasePost != null)
            {
                _db.Remove(databasePost);
                _db.SaveChanges();
            }
        }
        private void findBalanceAndBudget(BalanceAndBudget balanceAndBudget, long tennantId)
        {
            BalanceAndBudget databaseBalanceAndBudget = _db.BalanceAndBudgets
            .Where(c => c.tennantFK == tennantId).FirstOrDefault<BalanceAndBudget>();

            if (databaseBalanceAndBudget != null)
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

            if (databaseOrder != null)
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

            if (databaseEmployee != null)
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

            if (databaseAbsenceRegister != null)
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

            if (databaseTimeRegister != null)
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