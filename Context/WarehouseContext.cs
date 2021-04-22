
using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context {
    public class WarehouseContext : DbContext {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) {
            
        }
        // Define database tables
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
    }
}