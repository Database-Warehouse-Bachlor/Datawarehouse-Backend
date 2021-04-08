
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
            long tennantId = -1;

            try{

            ContentsList contentsList = JsonConvert.DeserializeObject<ContentsList>(
                jsonDataAsString);


            tennantId = addTennant(contentsList.businessId, contentsList.tennantName, contentsList.apiKey);
            
            //-------------------------------------------------
            //Er dette fin kode? Eller burde jeg endre på noe?
            //-------------------------------------------------

            //Failsafe if addtennant dont update the tennantId
            if(tennantId > 0) {
            
            //Adds Invoice inbound to datawarehouse
            for(int i = 0; i < contentsList.InvoiceInbound.Count; i++) {
                InvoiceInbound invoice = new InvoiceInbound();
                invoice = contentsList.InvoiceInbound[i];
                invoice.tennantId = tennantId;
                _db.InvoiceInbounds.Add(invoice);
                }

            //Adds Invoice outbound to datawarehouse
            for(int i = 0; i < contentsList.InvoiceOutbound.Count; i++) {
                InvoiceOutbound outbound = new InvoiceOutbound();
                outbound = contentsList.InvoiceOutbound[i];
                _db.InvoiceOutbounds.Add(outbound);
            }
            
            //Adds custumer to datawarehouse
            for(int i = 0; i < contentsList.Customer.Count; i++) {
                Customer customer = new Customer();
                customer = contentsList.Customer[i];
                _db.Customers.Add(customer);
            }

            for(int i = 0; i < contentsList.BalanceAndBudget.Count; i++) {
                BalanceAndBudget balanceAndBudget = new BalanceAndBudget();
                balanceAndBudget = contentsList.BalanceAndBudget[i];
                _db.BalanceAndBudgets.Add(balanceAndBudget);
            }

            for(int i = 0; i < contentsList.AbsenceRegister.Count; i++) {
                AbsenceRegister absenceRegister = new AbsenceRegister();
                absenceRegister = contentsList.AbsenceRegister[i];
                _db.AbsenceRegisters.Add(absenceRegister);
            }

            for(int i = 0; i < contentsList.Accountsreceivable.Count; i++) {
                Accountsreceivable accountsreceivable = new Accountsreceivable();
                accountsreceivable = contentsList.Accountsreceivable[i];
                _db.Accountsreceivables.Add(accountsreceivable);
            }

            for(int i = 0; i < contentsList.Employee.Count; i++) {
                Employee employee = new Employee();
                employee = contentsList.Employee[i];
                _db.Employees.Add(employee);
            }

            for(int i = 0; i < contentsList.Order.Count; i++) {
                Order order = new Order();
                order = contentsList.Order[i];
                _db.Orders.Add(order);
            }

            for(int i = 0; i < contentsList.TimeRegister.Count; i++) {
                TimeRegister timeRegister = new TimeRegister();
                timeRegister = contentsList.TimeRegister[i];
                _db.timeRegisters.Add(timeRegister);
            }
            } else {
                ErrorLog errorLog = new ErrorLog();
                String errorMessage = "Tennant id ble ikke oppdatert i addData (DataSubmissionController.cs) før behandlingen av data skjedde";
                errorLog.errorMessage = errorMessage;
                errorLog.timeOfError = DateTime.Now;
                _db.ErrorLogs.Add(errorLog);
                
                Console.WriteLine("tennantid was -1");
            }

            //Saves changes to DB if everything is OK
            _db.SaveChanges();

            //TODO Fikse at orgnummer eller tennantid kommer opp i feilmelding
            } catch (JsonSerializationException e) {

                ErrorLog errorLog = new ErrorLog();

                String errorMessage = e.Message.ToString() + " Orgnummer: //TODO";
                DateTime timeOfError = DateTime.Now;

                errorLog.errorMessage = errorMessage;
                errorLog.timeOfError = timeOfError;

                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();


            } catch (NullReferenceException e) {
                ErrorLog errorLog = new ErrorLog();

                String errorMessage = e.ToString() + " Tennant ID: " + tennantId;
                DateTime timeOfError = DateTime.Now;

                errorLog.errorMessage = errorMessage;
                errorLog.timeOfError = timeOfError;

                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
            } catch (InvalidbusinessIdOrApiKeyException) {
                /*
                This catch is only used for skipping the processing of incoming data.
                There is only one place where it is thrown, and that is when the businessID
                or apiKey is invalid. It gets logged in the database, and jumpes out of the Try.
                */
            }

            return Ok();
        }


        /* 
        This function is called whenever the datawarehouse receives data.
        If the incoming data doesn't have a tennant to connect the data to, it will create a tennant based on
        the incoming information (businessname, businessID and API-key).
         */
        private long addTennant(string bId, string bName, string apiKey){
            ErrorLog errorLog = new ErrorLog();
            var business = _db.Tennants.Where(b => b.businessId == bId).FirstOrDefault<Tennant>();
            if(business == null && bId != null && apiKey != null && bId != "" && apiKey != "") {  
                Tennant tennant = new Tennant();
                tennant.businessId = bId;
                tennant.tennantName = bName;
                tennant.apiKey = apiKey;
                _db.Tennants.Add(tennant);
                _db.SaveChanges();
                
                return tennant.id;

            } else if(bId == null || bId == "") {
                string businessIdError = "BusinessId er enten tom eller ikke presentert på riktig måte.\nBID: " + bId; 
                
                errorLog.errorMessage = businessIdError;
                errorLog.timeOfError = DateTime.Now; 
                
                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
                //Throws an Exception so it does not try to process incoming data that will lead to new Exception
                throw new InvalidbusinessIdOrApiKeyException();
            } else if(apiKey == null || apiKey == "") {
                string apiKeyError = "API-nøkkelen er enten tom eller ikke presentert på riktig måte.\nAPI-key: " + apiKey;
                
                errorLog.errorMessage = apiKeyError;
                errorLog.timeOfError = DateTime.Now;

                _db.ErrorLogs.Add(errorLog);
                _db.SaveChanges();
                //Throws an Exception so it does not try to process incoming data that will lead to new Exception
                throw new InvalidbusinessIdOrApiKeyException();
            } else if(business != null) {
                Console.WriteLine("Tennant found, submitting data...");
            } 
                return business.id;
        }
    }
}