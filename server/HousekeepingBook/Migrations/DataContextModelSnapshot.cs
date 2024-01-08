﻿// <auto-generated />
using System;
using HousekeepingBook.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HousekeepingBook.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HousekeepingBook.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"));

                    b.Property<DateTime>("CreateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonthlyInvoiceSummaryId")
                        .HasColumnType("int");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdateTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("InvoiceId");

                    b.HasIndex("MonthlyInvoiceSummaryId");

                    b.HasIndex("StoreId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("HousekeepingBook.Entities.MonthlyInvoiceSummary", b =>
                {
                    b.Property<int>("MonthlyInvoiceSummaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthlyInvoiceSummaryId"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("MonthlyInvoiceSummaryId");

                    b.ToTable("MonthlyInvoiceSummaries");
                });

            modelBuilder.Entity("HousekeepingBook.Entities.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<DateTime>("CreateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("HousekeepingBook.Entities.Invoice", b =>
                {
                    b.HasOne("HousekeepingBook.Entities.MonthlyInvoiceSummary", null)
                        .WithMany("Invoices")
                        .HasForeignKey("MonthlyInvoiceSummaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousekeepingBook.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("HousekeepingBook.Entities.MonthlyInvoiceSummary", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}