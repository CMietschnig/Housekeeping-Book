using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Models;
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

        #region AddInvoiceToMonthAndYear
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

        [Fact]
        public void AddInvoiceToMonthAndYear_ShouldAddDifferentInvoiceToDatabase()
        {
            // Arrange
            var sut = new SQLInvoiceRepository(context);
            var invoice = new Invoice()
            {
                InvoiceId = 3,
                Total = 6789.09,
                CreateTimestamp = new DateTime(2024, 2, 15),
                UpdateTimestamp = new DateTime(2024, 2, 15),
                MonthlyInvoiceSummaryId = 5,
                Store = new Store()
                {
                    StoreId = 1,
                    StoreName = "Billa",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                }
            };

            // Act
            bool result = sut.AddInvoiceToMonthAndYear(invoice);

            // Assert
            List<Invoice> invoices = context.Invoices.ToList();
            Assert.True(result);
            Assert.Single(invoices);
        }
        #endregion

        #region DeleteInvoiceById

        [Fact]
        public void DeleteInvoiceById_ShouldDeleteInvoice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeleteInvoiceById_ShouldDeleteInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
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

                initialContext.Invoices.Add(initialInvoice);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);
                int id = 1;

                // Act
                bool result = sut.DeleteInvoiceById(id);

                // Assert
                Assert.True(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.DoesNotContain(invoices, invoice => invoice.InvoiceId == id);
                Assert.Empty(invoices);
            }
        }

        [Fact]
        public void DeleteInvoiceById_ShouldNotDeleteNonExistingInvoice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeleteInvoiceById_ShouldNotDeleteNonExistingInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add invoices
                var initialInvoice1 = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 1,
                    Store = null
                };
                var initialInvoice2 = new Invoice
                {
                    InvoiceId = 2,
                    Total = 70.0,
                    CreateTimestamp = new DateTime(2024, 1, 17),
                    UpdateTimestamp = new DateTime(2024, 1, 18),
                    MonthlyInvoiceSummaryId = 3,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);
                int id = 3;

                // Act
                bool result = sut.DeleteInvoiceById(id);

                // Assert
                Assert.False(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.DoesNotContain(invoices, invoice => invoice.InvoiceId == id);
                Assert.Equal(2, invoices.Count);
            }
        }

        [Fact]
        public void DeleteInvoiceById_ShouldDeleteCorrectInvoice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeleteInvoiceById_ShouldDeleteCorrectInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add invoices
                var initialInvoice1 = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 1,
                    Store = null
                };
                var initialInvoice2 = new Invoice
                {
                    InvoiceId = 2,
                    Total = 70.0,
                    CreateTimestamp = new DateTime(2024, 1, 17),
                    UpdateTimestamp = new DateTime(2024, 1, 18),
                    MonthlyInvoiceSummaryId = 3,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);
                int id = 1;

                // Act
                bool result = sut.DeleteInvoiceById(id);

                // Assert
                Assert.True(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.DoesNotContain(invoices, invoice => invoice.InvoiceId == id);
                Assert.Contains(invoices, invoice => invoice.InvoiceId == 2);
            }
        }
        #endregion

        #region GetInvoiceById
        [Fact]
        public void GetInvoiceById_ShouldGetCorrectInvoiceById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetInvoiceById_ShouldGetInvoiceById")
                .Options;

            var initialInvoice1 = new Invoice
            {
                InvoiceId = 1,
                Total = 50.0,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 1, 15),
                MonthlyInvoiceSummaryId = 1,
                Store = null
            };
            var initialInvoice2 = new Invoice
            {
                InvoiceId = 2,
                Total = 70.0,
                CreateTimestamp = new DateTime(2024, 1, 17),
                UpdateTimestamp = new DateTime(2024, 1, 18),
                MonthlyInvoiceSummaryId = 3,
                Store = null
            };

            using (var initialContext = new DataContext(options))
            {
                // Add invoices
                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);

                // Act
                Invoice? result = sut.GetInvoiceById(1);

                // Assert
                Assert.Equal(result?.InvoiceId, initialInvoice1.InvoiceId);
            }
        }

        [Fact]
        public void GetInvoiceById_ShouldGetNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetInvoiceById_ShouldGetNull")
                .Options;

            var initialInvoice1 = new Invoice
            {
                InvoiceId = 1,
                Total = 50.0,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 1, 15),
                MonthlyInvoiceSummaryId = 1,
                Store = null
            };
            var initialInvoice2 = new Invoice
            {
                InvoiceId = 2,
                Total = 70.0,
                CreateTimestamp = new DateTime(2024, 1, 17),
                UpdateTimestamp = new DateTime(2024, 1, 18),
                MonthlyInvoiceSummaryId = 3,
                Store = null
            };

            using (var initialContext = new DataContext(options))
            {
                // Add invoices
                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);

                // Act
                Invoice? result = sut.GetInvoiceById(3);

                // Assert
                Assert.Null(result);
            }
        }
        #endregion

        #region GetInvoicesPerMonthlyInvoiceSummaryId
        [Fact]
        public void GetInvoicesPerMonthlyInvoiceSummaryId_ShouldGetEmptyListForNonExistingId()
        { 
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetInvoicesPerMonthlyInvoiceSummaryId_ShouldGetEmptyListForNonExistingId")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial invoice
                var initialInvoice = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 1,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);
                int monthlySummaryId =  3;

                // Act
                IEnumerable<Invoice> result = sut.GetInvoicesPerMonthlyInvoiceSummaryId(monthlySummaryId);

                // Assert
                Assert.Empty(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.DoesNotContain(invoices, invoice => invoice.MonthlyInvoiceSummaryId == monthlySummaryId);
                Assert.Single(invoices);
            }   
        }

        [Fact]
        public void GetInvoicesPerMonthlyInvoiceSummaryId_ShouldGetListWithCorrectInvoices()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetInvoicesPerMonthlyInvoiceSummaryId_ShouldGetListWithCorrectInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial invoice
                var initialInvoice1 = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };
                var initialInvoice2 = new Invoice
                {
                    InvoiceId = 2,
                    Total = 70.0,
                    CreateTimestamp = new DateTime(2024, 1, 14),
                    UpdateTimestamp = new DateTime(2024, 1, 19),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);
                int monthlySummaryId = 5;

                // Act
                IEnumerable<Invoice> result = sut.GetInvoicesPerMonthlyInvoiceSummaryId(monthlySummaryId);

                // Assert
                Assert.NotEmpty(result);
                Assert.All(result, invoice => Assert.Equal(monthlySummaryId, invoice.MonthlyInvoiceSummaryId));
            }
        }
        #endregion

        #region UpdateInvoiceById
        [Fact]
        public void UpdateInvoiceById_ShouldUpdateInvoice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateInvoiceById_ShouldUpdateInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial invoice
                var initialInvoice1 = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };
                var initialInvoice2 = new Invoice
                {
                    InvoiceId = 2,
                    Total = 70.0,
                    CreateTimestamp = new DateTime(2024, 1, 14),
                    UpdateTimestamp = new DateTime(2024, 1, 19),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);

                Invoice newModel = new Invoice()
                {
                    InvoiceId = 1,
                    Total = 67.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 18),
                    MonthlyInvoiceSummaryId = 5,
                    Store = new Store()
                    {
                        StoreId = 1,
                        StoreName = "Test",
                        CreateTimestamp = new DateTime(2024, 1, 12),
                        UpdateTimestamp = new DateTime(2024, 1, 15),
                    }
                };

                // Act
                bool result = sut.UpdateInvoiceById(newModel);

                // Assert
                Assert.True(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.Equal(2, invoices.Count());
                Assert.Equal(newModel.InvoiceId, invoices[0].InvoiceId);
                Assert.Equal(newModel.Total, invoices[0].Total);
                Assert.Equal(newModel.UpdateTimestamp, invoices[0].UpdateTimestamp);
                // will be added later!
                //Assert.NotNull(invoices[0].Store);
                //Assert.Equal(newModel.Store.StoreId, invoices[0].Store.StoreId);
                //Assert.Equal(newModel.Store.StoreName, invoices[0].Store.StoreName);
            }
        }

        [Fact]
        public void UpdateInvoiceById_ShouldNotUpdateInvoice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateInvoiceById_ShouldNotUpdateInvoice")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial invoice
                var initialInvoice1 = new Invoice
                {
                    InvoiceId = 1,
                    Total = 50.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };
                var initialInvoice2 = new Invoice
                {
                    InvoiceId = 2,
                    Total = 70.0,
                    CreateTimestamp = new DateTime(2024, 1, 14),
                    UpdateTimestamp = new DateTime(2024, 1, 19),
                    MonthlyInvoiceSummaryId = 5,
                    Store = null
                };

                initialContext.Invoices.Add(initialInvoice1);
                initialContext.Invoices.Add(initialInvoice2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLInvoiceRepository(context);

                Invoice newModel = new Invoice()
                {
                    InvoiceId = 3,
                    Total = 90.0,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 18),
                    MonthlyInvoiceSummaryId = 5,
                    Store = new Store()
                    {
                        StoreId = 5,
                        StoreName = "Test",
                        CreateTimestamp = new DateTime(2024, 1, 12),
                        UpdateTimestamp = new DateTime(2024, 1, 15),
                    }
                };

                // Act
                bool result = sut.UpdateInvoiceById(newModel);

                // Assert
                Assert.False(result);
                List<Invoice> invoices = context.Invoices.ToList();
                Assert.Equal(2, invoices.Count());
                Assert.NotEqual(newModel.Total, invoices[0].Total);
                Assert.NotEqual(newModel.Total, invoices[1].Total);
                Assert.NotEqual(newModel.UpdateTimestamp, invoices[0].UpdateTimestamp);
                Assert.NotEqual(newModel.UpdateTimestamp, invoices[1].UpdateTimestamp);
                // will be added later!
                //Assert.NotNull(invoices[0].Store);
                //Assert.Equal(newModel.Store.StoreId, invoices[0].Store.StoreId);
                //Assert.Equal(newModel.Store.StoreName, invoices[0].Store.StoreName);
            }
        }
        #endregion
    }
}
