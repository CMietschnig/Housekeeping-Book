using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HousekeepingBook.Tests.Repositories
{
    public  class SQLInvoiceRepositoryTests
    {

        private readonly DataContext context;

        public SQLInvoiceRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            context = new DataContext((DbContextOptions<DataContext>)dbOptions.Options);
        }


        [Fact]
        public void AddInvoiceToMonthAndYear_ShouldAddInvoiceToDatabase()
        {
            // Arrange
            var sut = new SQLInvoiceRepository(context);
            var invoice = new Invoice()
            {
                InvoiceId = 1,
                Total = 23.56,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 1, 15),
                MonthlyInvoiceSummaryId = 1,
                Store = null 
            };

            // Act
            bool result = sut.AddInvoiceToMonthAndYear(invoice);

            // Assert
            List<Invoice> invoices = context.Invoices.ToList();
            Assert.True(result);
            Assert.Single(invoices);
        }

        [Fact]
        public void AddInvoiceToMonthAndYear_ShouldFailAddingAInvoiceToTheDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "AddInvoiceToMonthAndYear_ShouldFailAddingDuplicateInvoiceId")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial invoice with InvoiceId = 1
                var initialInvoice = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 1,
                    Store = null
                };

                context.Invoices.Add(initialInvoice);
                context.SaveChanges();
            }

            // Now, attempt to add another invoice with the same InvoiceId
            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);

                var duplicateInvoice = new Invoice
                {
                    InvoiceId = 1, // Duplicate InvoiceId
                    Total = 75.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 1,
                    Store = null
                };

                // Act and assert
                Assert.Throws<ArgumentException>(() => sut.AddInvoiceToMonthAndYear(duplicateInvoice));
            }
        }
    }
}
