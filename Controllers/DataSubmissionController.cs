
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

            //-------------------------------------------------
            //Er dette fin kode? Eller burde jeg endre på noe?
            //-------------------------------------------------

            try{

            ContentsList dataFull = JsonConvert.DeserializeObject<ContentsList>(dataM);

            //Adds Invoice inbound to datawarehouse
            for(int i = 0; i < dataFull.InvoiceInbound.Count; i++) {
                InvoiceInbound invoice = new InvoiceInbound();
                invoice = dataFull.InvoiceInbound[i];
                _db.InvoiceInbounds.Add(invoice);
                }

            //Adds Invoice outbound to datawarehouse
            for(int i = 0; i < dataFull.InvoiceOutbound.Count; i++) {
                InvoiceOutbound outbound = new InvoiceOutbound();
                outbound = dataFull.InvoiceOutbound[i];
                _db.InvoiceOutbounds.Add(outbound);
            }
            
            //Adds custumer to datawarehouse
            for(int i = 0; i < dataFull.Customer.Count; i++) {
                Customer customer = new Customer();
                customer = dataFull.Customer[i];
                _db.Customers.Add(customer);
            }

            for(int i = 0; i < dataFull.BalanceAndBudget.Count; i++) {
                BalanceAndBudget balanceAndBudget = new BalanceAndBudget();
                balanceAndBudget = dataFull.BalanceAndBudget[i];
                _db.BalanceAndBudgets.Add(balanceAndBudget);
            }

            for(int i = 0; i < dataFull.AbsenceRegister.Count; i++) {
                AbsenceRegister absenceRegister = new AbsenceRegister();
                absenceRegister = dataFull.AbsenceRegister[i];
                _db.AbsenceRegisters.Add(absenceRegister);
            }

            for(int i = 0; i < dataFull.Accountsreceivable.Count; i++) {
                Accountsreceivable accountsreceivable = new Accountsreceivable();
                accountsreceivable = dataFull.Accountsreceivable[i];
                _db.Accountsreceivables.Add(accountsreceivable);
            }

            for(int i = 0; i < dataFull.Employee.Count; i++) {
                Employee employee = new Employee();
                employee = dataFull.Employee[i];
                _db.Employees.Add(employee);
            }

            for(int i = 0; i < dataFull.Order.Count; i++) {
                Order order = new Order();
                order = dataFull.Order[i];
                _db.Orders.Add(order);
            }

            for(int i = 0; i < dataFull.TimeRegister.Count; i++) {
                TimeRegister timeRegister = new TimeRegister();
                timeRegister = dataFull.TimeRegister[i];
                _db.timeRegisters.Add(timeRegister);
            }

            //Saves changes to DB if everything is OK
            _db.SaveChanges();

            } catch (Exception e) {
                Console.WriteLine(e);
            }

            return Ok();
        }




    }
}