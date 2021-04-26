
using System;
using System.Collections.Generic;
using System.Linq;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context
{
    public class WarehouseContext : DbContext, IWarehouseContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }
        // Define database tables.
        public DbSet<AbsenceRegister> AbsenceRegisters { get; set; }
        public DbSet<BalanceAndBudget> BalanceAndBudgets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tennant> Tennants { get; set; }
        public DbSet<TimeRegister> TimeRegisters { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<InvoiceLine> InvoiceLines {get; set;}       
        public DbSet<Voucher> Vouchers {get; set;}       
        public DbSet<Post> Posts {get; set;}       
        public DbSet<Account> Accounts {get; set;}       
        public DbSet<FinancialYear> FinancialYears {get; set;}       
        //public DbSet<AccountsReceivable> AccountsReceivables { get; set; }

        public Tennant findTennantById(long tennantId)
        {
            var tennant = Tennants
            .Where(o => o.id == tennantId)
            .FirstOrDefault<Tennant>();
            return tennant;
        }

        // Returns a list of all errors
        public List<ErrorLog> getAllErrors()
        {
            var errors = ErrorLogs.ToList();
            return errors;
        }


        // Returns a list of errors that arrived the last 24 hours
        public List<ErrorLog> getLatestErrors()
        {
            DateTime currentTime = DateTime.Now;
            var errors = ErrorLogs
            .Where(d => d.timeOfError >= currentTime.AddHours(-24))
            .ToList();
            return errors;
        }

        // Returns the number of errors the last 24 hours
        public int getNumberOfErrorsLastTwentyFour()
        {
            DateTime currentTime = DateTime.Now;
            var errors =  ErrorLogs.Where(d => d.timeOfError >= currentTime.AddHours(-24))
            .Count();
            return errors;
        }

        // Returns the number of tennants
        public int getNumberOfTennants()
        {
            return Tennants.Count();
        }

        // Returns a list of all tennants
        public List<Tennant> getAllTennants()
        {
            return Tennants.ToList();
        }
    }
}