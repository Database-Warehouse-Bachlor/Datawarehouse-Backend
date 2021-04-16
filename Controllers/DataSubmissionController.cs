
using System;
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

/*
* This controller handles the automated inputs sent from Cordel's systems every night.
*/ 

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
        public IActionResult addData([FromBody] dynamic data) {

            string jsonDataAsString = data + "";

            try
            {

                ContentsList contentsList = JsonConvert.DeserializeObject<ContentsList>(
                    jsonDataAsString);


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

                        addCustomer(customer, tennantId);
                    }
                    
                    //Adds Employee to datawarehouse
                    for (int i = 0; i < contentsList.Employee.Count; i++)
                    {
                        Employee employee = new Employee();
                        employee = contentsList.Employee[i];

                        addEmployee(employee, tennantId);
                    }

                    //Adds Order to datawarehouse
                    for (int i = 0; i < contentsList.Order.Count; i++)
                    {
                        Order order = new Order();
                        order = contentsList.Order[i];
                        _db.Orders.Add(order);
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
                        _db.InvoiceOutbounds.Add(outbound);
                    }

                    

                    //Adds Balance and Budget to datawarehouse
                    for (int i = 0; i < contentsList.BalanceAndBudget.Count; i++)
                    {
                        BalanceAndBudget balanceAndBudget = new BalanceAndBudget();
                        balanceAndBudget = contentsList.BalanceAndBudget[i];
                        _db.BalanceAndBudgets.Add(balanceAndBudget);
                    }

                    //Adds Absence Register to datawarehouse
                    for (int i = 0; i < contentsList.AbsenceRegister.Count; i++)
                    {
                        AbsenceRegister absenceRegister = new AbsenceRegister();
                        absenceRegister = contentsList.AbsenceRegister[i];
                        _db.AbsenceRegisters.Add(absenceRegister);
                    }

                    //Adds Accountsreceivable to datawarehouse
                    for (int i = 0; i < contentsList.Accountsreceivable.Count; i++)
                    {
                        AccountsReceivable accountsReceivable = new AccountsReceivable();
                        accountsReceivable = contentsList.Accountsreceivable[i];
                        _db.AccountsReceivables.Add(accountsReceivable);
                    }

                    //Adds TimeRegister to datawarehouse
                    for (int i = 0; i < contentsList.TimeRegister.Count; i++)
                    {
                        TimeRegister timeRegister = new TimeRegister();
                        timeRegister = contentsList.TimeRegister[i];
                        _db.TimeRegisters.Add(timeRegister);
                    }
                }
                else
                {
                    ErrorLog errorLog = new ErrorLog();
                    string errorType = "Tennant ID update fail";
                    string errorMessage = "Tennant id ble ikke oppdatert i addData (DataSubmissionController.cs) før behandlingen av data skjedde";
                    
                    errorLog.errorType = errorType;
                    errorLog.errorMessage = errorMessage;
                    errorLog.timeOfError = DateTime.Now;
                    _db.ErrorLogs.Add(errorLog);

                    Console.WriteLine("tennantid was -1");
                }

                //Saves changes to DB if everything is OK
                _db.SaveChanges();

                /*
                The programm picks up this error if the JsonConvert.DeserializeObject fails.
                Fields that are null or not the expected datatype can cause this
                */
            }
            catch (Exception e) {
                ErrorLog errorLog = new ErrorLog();
                string errorMessage = e + " Exception caught.";
                errorLog.errorMessage = errorMessage;
                errorLog.timeOfError = DateTime.Now;
                Console.WriteLine(errorLog.errorMessage + " " + errorLog.timeOfError);
                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
                Console.WriteLine();
            }

            //-------------------------------------------------
            //Er dette fin kode? Eller burde jeg endre på noe?
            //-------------------------------------------------

            //List<InvoiceInbound> invoices = [];
            
            //Adds Invoice inbound to datawarehouse
            
            // for(int i = 0; i < contentsList.InvoiceInbound.Count; i++) {
            //     InvoiceInbound invoice = new InvoiceInbound();
            //     invoice = contentsList.InvoiceInbound[i];
            //     _db.InvoiceInbounds.Add(invoice);
            //     _db.SaveChanges();
            //     }

            // //Adds Invoice outbound to datawarehouse
            // for(int i = 0; i < contentsList.InvoiceOutbound.Count; i++) {
            //     InvoiceOutbound outbound = new InvoiceOutbound();
            //     outbound = contentsList.InvoiceOutbound[i];
            //     _db.InvoiceOutbounds.Add(outbound);
            //     _db.SaveChanges();
            // }
            
            // //Adds custumer to datawarehouse
            // for(int i = 0; i < contentsList.Customer.Count; i++) {
            //     Customer customer = new Customer();
            //     customer = contentsList.Customer[i];
            //     _db.Customers.Add(customer);
            //     _db.SaveChanges();
            // }


                

                //invoice.invoiceId       =   invoices.InvoiceInbound[i].invoiceId;
                //invoice.tennantId       =   invoices.InvoiceInbound[i].tennantId;
                //invoice.jobId           =   invoices.InvoiceInbound[i].jobId;
                //invoice.supplierId      =   invoices.InvoiceInbound[i].supplierId;
                //invoice.wholesalerId    =   invoices.InvoiceInbound[i].wholesalerId;
                //invoice.invoiceDate     =   invoices.InvoiceInbound[i].invoiceDate;
                //invoice.amountTotal     =   invoices.InvoiceInbound[i].amountTotal;
                //invoice.specification   =   invoices.InvoiceInbound[i].specification;
                //invoice.invoicePdf      =   invoices.InvoiceInbound[i].invoicePdf;

            return Ok();
        }


        /* 
        This function is called whenever the datawarehouse receives data.
        If the incoming data doesn't have a tennant to connect the data to, it will create a tennant based on
        the incoming information (businessname, businessID and API-key).
         */
        private void addTennant(string bId, string bName, string apiKey){
            ErrorLog errorLog = new ErrorLog();
            var business = _db.Tennants.Where(b => b.businessId == bId).FirstOrDefault<Tennant>();
            if(business == null && bId != null && apiKey != null && bId != "" && apiKey != "") {  
                Tennant tennant = new Tennant();
                tennant.businessId = bId;
                tennant.tennantName = bName;
                tennant.apiKey = apiKey;
                _db.Tennants.Add(tennant);
                _db.SaveChanges();
            } else if(bId == null || bId == "") {
                string businessIdError = "BusinessId er enten tom eller ikke presentert på riktig måte.\nBID: " + bId; 
                errorLog.errorMessage = businessIdError;
                errorLog.timeOfError = DateTime.Now; 
                Console.WriteLine("BusinessID is either empty or not presented properly");
                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
            } else if(apiKey == null || apiKey == "") {
                string apiKeyError = "API-nøkkelen er enten tom eller ikke presentert på riktig måte.\nAPI-key: " + apiKey;
                errorLog.errorMessage = apiKeyError;
                errorLog.timeOfError = DateTime.Now;
                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
                Console.WriteLine("API-key er enten tom eller ikke presentert på riktig måte.");
            } else if(business != null) {
                Console.WriteLine("Tennant found, submitting data...");
            }
        }
        private long addCustomer(Customer customer, long tennantFK ) 
        {
            ErrorLog errorLog = new ErrorLog();
            Customer databaseCustomer  = _db.Customers.Where(c => c.custommerId == customer.custommerId).FirstOrDefault<Customer>();
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

        private long addEmployee(Employee employee, long tennantFK ) 
        {
            ErrorLog errorLog = new ErrorLog();
            Employee databaseEmployee  = _db.Employees.Where(c => c.employeeId == employee.employeeId).FirstOrDefault<Employee>();
            if (databaseEmployee == null)
            {
                Employee employee1 = new Employee();
                employee1 = employee;
                employee1.tennantFK = tennantFK;
                _db.Employees.Add(employee);

               await _db.SaveChangesAsync();

                return employee.id;
            } 
            return databaseEmployee.id;
        }
    }
}