using System;
using System.Collections.Generic;
using Datawarehouse_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context
{
    public interface IWarehouseContext
    {
        public DbSet<AbsenceRegister> AbsenceRegisters { get; set; }
        public DbSet<BalanceAndBudget> BalanceAndBudgets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tennant> Tennants { get; set; }
        public DbSet<TimeRegister> TimeRegisters { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }

        int getNumberOfTennants();
        List<Tennant> getAllTennants();
        Tennant findTennantById(long tennantId);
        int getNumberOfErrorsLastTwentyFour();
        List<ErrorLog> getLatestErrors();
        List<ErrorLog> getAllErrors();
        List<AbsenceRegister> getAllAbsenceFromDate(long tennantId, DateTime comparisonDate);
        List<Invoice> getAllInboundInvoice(long tennantId, DateTime comparisonDate);
        List<TimeRegister> getAllTimeRegistersInDescendingOrder(long tennantId, DateTime comparisonDate);
        List<Voucher> getVouchersInDescendingByPaymentThenByType(long tennantId, DateTime comparisonDate);
        List<Voucher> getInboundVouchersInDescendingByPaymentThenByType(long tennantId, DateTime comparisonDate);

    }
}