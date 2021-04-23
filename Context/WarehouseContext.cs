
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
        public DbSet<AccountsReceivable> AccountsReceivables { get; set; }
        public DbSet<BalanceAndBudget> BalanceAndBudgets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InvoiceInbound> InvoiceInbounds { get; set; }
        public DbSet<InvoiceOutbound> InvoiceOutbounds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tennant> Tennants { get; set; }
        public DbSet<TimeRegister> TimeRegisters { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public Tennant findTennantById(long tennantId)
        {
            var tennant = Tennants
            .Where(o => o.id == tennantId)
            .FirstOrDefault<Tennant>();
            return tennant;
        }

        public List<Tennant> getAllTennants()
        {
            return Tennants.ToList();
        }

        public int getNumberOfTennants() {
            return Tennants.Count();
        }
        
    }
}