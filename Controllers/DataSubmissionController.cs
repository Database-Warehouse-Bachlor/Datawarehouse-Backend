
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

            

            string dataM = data + "";



            ContentsList dataFull = JsonConvert.DeserializeObject<ContentsList>(dataM);
            //DeserializeObject<InvoiceList>(dataM);


            //-------------------------------------------------
            //Er dette fin kode? Eller burde jeg endre på noe?
            //-------------------------------------------------

            //List<InvoiceInbound> invoices = [];
            
            //Adds Invoice inbound to datawarehouse
            
            for(int i = 0; i < dataFull.InvoiceInbound.Count; i++) {
                InvoiceInbound invoice = new InvoiceInbound();
                invoice = dataFull.InvoiceInbound[i];
                _db.InvoiceInbounds.Add(invoice);
                _db.SaveChanges();
                }

            //Adds Invoice outbound to datawarehouse
            for(int i = 0; i < dataFull.InvoiceOutbound.Count; i++) {
                InvoiceOutbound outbound = new InvoiceOutbound();
                outbound = dataFull.InvoiceOutbound[i];
                _db.InvoiceOutbounds.Add(outbound);
                _db.SaveChanges();
            }
            
            //Adds custumer to datawarehouse
            for(int i = 0; i < dataFull.Customer.Count; i++) {
                Customer customer = new Customer();
                customer = dataFull.Customer[i];
                _db.Customers.Add(customer);
                _db.SaveChanges();
            }


                

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

        private void addTennant(string bId, string bName){
            var business = _db.Tennants.Where(b => b.businessId == bId).FirstOrDefault<Tennant>();
            if(business == null) {
                Tennant tennant = new Tennant();
                tennant.businessId = bId;
                tennant.tennantName = bName;
                _db.Tennants.Add(tennant);
                _db.SaveChanges();
            }
        }
    }

}