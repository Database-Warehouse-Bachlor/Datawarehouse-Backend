﻿// <auto-generated />
using System;
using Datawarehouse_Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    [DbContext(typeof(LoginDatabaseContext))]
    partial class LoginDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Datawarehouse_Backend.Models.AbsenceRegister", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AbsenceRegisterId")
                        .HasColumnType("bigint");

                    b.Property<string>("abcenseType")
                        .HasColumnType("text");

                    b.Property<string>("abcenseTypeText")
                        .HasColumnType("text");

                    b.Property<string>("comment")
                        .HasColumnType("text");

                    b.Property<string>("degreeDisability")
                        .HasColumnType("text");

                    b.Property<double>("duration")
                        .HasColumnType("double precision");

                    b.Property<long>("employeeFK")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("fromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("soleCaretaker")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("toDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("employeeFK");

                    b.ToTable("AbsenceRegister");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.AccountsReceivable", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountsReceivableId")
                        .HasColumnType("bigint");

                    b.Property<double>("amount")
                        .HasColumnType("double precision");

                    b.Property<double>("amountDue")
                        .HasColumnType("double precision");

                    b.Property<long>("customerFK")
                        .HasColumnType("bigint");

                    b.Property<string>("customerName")
                        .HasColumnType("text");

                    b.Property<DateTime>("dueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("jobId")
                        .HasColumnType("bigint");

                    b.Property<string>("note")
                        .HasColumnType("text");

                    b.Property<long>("oderId")
                        .HasColumnType("bigint");

                    b.Property<string>("overdueNotice")
                        .HasColumnType("text");

                    b.Property<DateTime>("recordDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("recordType")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("customerFK");

                    b.ToTable("AccountsReceivable");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.BalanceAndBudget", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("BalanceAndBudgetId")
                        .HasColumnType("bigint");

                    b.Property<string>("account")
                        .HasColumnType("text");

                    b.Property<string>("department")
                        .HasColumnType("text");

                    b.Property<double>("endBalance")
                        .HasColumnType("double precision");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<double>("periodBalance")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("periodDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("startBalance")
                        .HasColumnType("double precision");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("tennantFK");

                    b.ToTable("BalanceAndBudget");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Customer", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("address")
                        .HasColumnType("text");

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<string>("customerName")
                        .HasColumnType("text");

                    b.Property<long>("custommerId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isInactive")
                        .HasColumnType("boolean");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.Property<int>("zipcode")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("tennantFK");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Employee", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("employeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("employeeName")
                        .HasColumnType("text");

                    b.Property<int>("employmentRate")
                        .HasColumnType("integer");

                    b.Property<string>("employmentType")
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .HasColumnType("text");

                    b.Property<bool>("isCaseworker")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("leaveDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("posistionCategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("ssbPayType")
                        .HasColumnType("text");

                    b.Property<string>("ssbPositionCode")
                        .HasColumnType("text");

                    b.Property<string>("ssbPositionText")
                        .HasColumnType("text");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.Property<string>("statusText")
                        .HasColumnType("text");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("tennantFK");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.InvoiceInbound", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("amountTotal")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("invoiceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("invoiceInboundId")
                        .HasColumnType("bigint");

                    b.Property<string>("invoicePdf")
                        .HasColumnType("text");

                    b.Property<long>("jobId")
                        .HasColumnType("bigint");

                    b.Property<string>("specification")
                        .HasColumnType("text");

                    b.Property<long>("supplierId")
                        .HasColumnType("bigint");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.Property<long>("wholesalerId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("tennantFK");

                    b.ToTable("InvoiceInbound");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.InvoiceOutbound", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("amountExVat")
                        .HasColumnType("double precision");

                    b.Property<double>("amountIncVat")
                        .HasColumnType("double precision");

                    b.Property<double>("amountTotal")
                        .HasColumnType("double precision");

                    b.Property<long>("customerFK")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("invoiceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("invoiceDue")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("invoiceExVat")
                        .HasColumnType("double precision");

                    b.Property<double>("invoiceIncVat")
                        .HasColumnType("double precision");

                    b.Property<long>("invoiceOutboundId")
                        .HasColumnType("bigint");

                    b.Property<long>("jobId")
                        .HasColumnType("bigint");

                    b.Property<long>("orderFK")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("customerFK");

                    b.HasIndex("orderFK")
                        .IsUnique();

                    b.ToTable("InvoiceOutbound");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("caseHandler")
                        .HasColumnType("text");

                    b.Property<DateTime>("confimedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("customerFK")
                        .HasColumnType("bigint");

                    b.Property<string>("customerName")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("fixedPriceAmount")
                        .HasColumnType("double precision");

                    b.Property<bool>("hasWarranty")
                        .HasColumnType("boolean");

                    b.Property<double>("hoursOfWork")
                        .HasColumnType("double precision");

                    b.Property<long>("invoiceOutboundFK")
                        .HasColumnType("bigint");

                    b.Property<bool>("isFixedPrice")
                        .HasColumnType("boolean");

                    b.Property<long>("jobId")
                        .HasColumnType("bigint");

                    b.Property<string>("jobName")
                        .HasColumnType("text");

                    b.Property<long>("jobSiteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("lastChanged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("materials")
                        .HasColumnType("text");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("orderId")
                        .HasColumnType("bigint");

                    b.Property<string>("orderType")
                        .HasColumnType("text");

                    b.Property<DateTime>("plannedDelivery")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("startedDelivery")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("warrantyDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("customerFK");

                    b.HasIndex("tennantFK");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Tennant", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("apiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("businessId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tennantName")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.TimeRegister", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("account")
                        .HasColumnType("text");

                    b.Property<double>("amount")
                        .HasColumnType("double precision");

                    b.Property<long>("employeeFK")
                        .HasColumnType("bigint");

                    b.Property<string>("invoiceRate")
                        .HasColumnType("text");

                    b.Property<bool>("isCaseworker")
                        .HasColumnType("boolean");

                    b.Property<long>("orderId")
                        .HasColumnType("bigint");

                    b.Property<string>("payType")
                        .HasColumnType("text");

                    b.Property<string>("payTypeName")
                        .HasColumnType("text");

                    b.Property<string>("personDepartment")
                        .HasColumnType("text");

                    b.Property<string>("personDepartmentName")
                        .HasColumnType("text");

                    b.Property<string>("personName")
                        .HasColumnType("text");

                    b.Property<string>("processingCode")
                        .HasColumnType("text");

                    b.Property<string>("qyt")
                        .HasColumnType("text");

                    b.Property<double>("rate")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("recordDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("recordDepartment")
                        .HasColumnType("text");

                    b.Property<string>("recordDepartmentName")
                        .HasColumnType("text");

                    b.Property<string>("recordType")
                        .HasColumnType("text");

                    b.Property<string>("recordTypeName")
                        .HasColumnType("text");

                    b.Property<string>("summaryType")
                        .HasColumnType("text");

                    b.Property<long>("timeRegisterId")
                        .HasColumnType("bigint");

                    b.Property<string>("viaType")
                        .HasColumnType("text");

                    b.Property<string>("workComment")
                        .HasColumnType("text");

                    b.Property<string>("workplace")
                        .HasColumnType("text");

                    b.Property<int>("year")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("employeeFK");

                    b.ToTable("TimeRegister");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("role")
                        .HasColumnType("text");

                    b.Property<long>("tennantFK")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("tennantFK");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.AbsenceRegister", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Employee", "employee")
                        .WithMany("absenceRegisters")
                        .HasForeignKey("employeeFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.AccountsReceivable", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Customer", "customer")
                        .WithMany("accountsreceivables")
                        .HasForeignKey("customerFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.BalanceAndBudget", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("bnb")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Customer", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("customers")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Employee", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("employees")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.InvoiceInbound", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("invoicesInbound")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.InvoiceOutbound", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Customer", "customer")
                        .WithMany("invoicesOutbound")
                        .HasForeignKey("customerFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datawarehouse_Backend.Models.Order", "order")
                        .WithOne("invoiceOutbound")
                        .HasForeignKey("Datawarehouse_Backend.Models.InvoiceOutbound", "orderFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("order");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Order", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Customer", "customer")
                        .WithMany("orders")
                        .HasForeignKey("customerFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("orders")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.TimeRegister", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Employee", "employee")
                        .WithMany("timeRegisters")
                        .HasForeignKey("employeeFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.User", b =>
                {
                    b.HasOne("Datawarehouse_Backend.Models.Tennant", "tennant")
                        .WithMany("users")
                        .HasForeignKey("tennantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tennant");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Customer", b =>
                {
                    b.Navigation("accountsreceivables");

                    b.Navigation("invoicesOutbound");

                    b.Navigation("orders");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Employee", b =>
                {
                    b.Navigation("absenceRegisters");

                    b.Navigation("timeRegisters");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Order", b =>
                {
                    b.Navigation("invoiceOutbound");
                });

            modelBuilder.Entity("Datawarehouse_Backend.Models.Tennant", b =>
                {
                    b.Navigation("bnb");

                    b.Navigation("customers");

                    b.Navigation("employees");

                    b.Navigation("invoicesInbound");

                    b.Navigation("orders");

                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
