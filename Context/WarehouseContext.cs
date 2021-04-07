
using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context {
    public class WarehouseContext : DbContext {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) {
            
        }
        // Define database tables
        public DbSet<AbsenceRegister> AbsenceRegisters { get; set; }
        public DbSet<Accountsreceivable> Accountsreceivables { get; set; }
        public DbSet<BalanceAndBudget> BalanceAndBudgets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InvoiceInbound> InvoiceInbounds { get; set; } 
        public DbSet<InvoiceOutbound> InvoiceOutbounds { get; set; } 
        public DbSet<Customer> Customers { get; set; }     
        public DbSet<Tennant> Tennants { get; set; }   
        public DbSet<Order> Orders { get; set; }    
        public DbSet<TimeRegister> timeRegisters { get; set;}
  
    }      
}