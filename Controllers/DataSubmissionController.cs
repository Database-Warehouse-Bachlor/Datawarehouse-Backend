
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

            ContentsList contentsList = JsonConvert.DeserializeObject<ContentsList>(jsonDataAsString);
            //DeserializeObject<InvoiceList>(jsonDataAsString);
            try{
            addTennant(contentsList.businessId, contentsList.tennantName, contentsList.apiKey);
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
    }
}