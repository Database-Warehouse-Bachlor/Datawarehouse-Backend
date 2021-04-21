using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context
{
    public interface IWarehouseContext
    {

        DbSet<AbsenceRegister> AbsenceRegisters { get; set; }
        DbSet<AccountsReceivable> AccountsReceivables { get; set; }
        DbSet<BalanceAndBudget> BalanceAndBudgets { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<InvoiceInbound> InvoiceInbounds { get; set; }
        DbSet<InvoiceOutbound> InvoiceOutbounds { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Tennant> Tennants { get; set; }
        DbSet<TimeRegister> TimeRegisters { get; set; }
        DbSet<ErrorLog> ErrorLogs { get; set; }
        int SaveChanges();
    }
}