using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousekeepingBook.Tests.Repositories
{
    public class SQLMonthlyInvoiceSummaryRepositoryTests
    {

        private readonly DataContext context;

        public SQLMonthlyInvoiceSummaryRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            context = new DataContext((DbContextOptions<DataContext>)dbOptions.Options);
        }

        #region AddNewMonthlyInvoiceSummary
        [Fact]
        public void AddNewMonthlyInvoiceSummary_ShouldAddSummaryToEmptyDatabase()
        {
            // Arrange
            var sut = new SQLMonthlyInvoiceSummaryRepository(context);
            var month = 1;
            var year = "2024";

            // Act
            bool result = sut.AddNewMonthlyInvoiceSummary(month, year);

            // Assert
            Assert.True(result);
            List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
            Assert.Single(summaries);
            Assert.Equal((int)month, (int)summaries[0].MonthId);
            Assert.Equal(year, summaries[0].Year);
            Assert.Null(summaries[0].Comment);
            Assert.IsType<DateTime>(summaries[0].CreateTimestamp);
            Assert.IsType<DateTime>(summaries[0].UpdateTimestamp);
        }

        [Fact]
        public void AddNewMonthlyInvoiceSummary_ShouldAddNewSummary_NewMonth_ToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "AddNewMonthlyInvoiceSummary_ShouldAddNewSummary_NewMonth_ToDatabase")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary = new MonthlyInvoiceSummary
                {
                  MonthlyInvoiceSummaryId = 1,
                  MonthId = Entities.Enums.Month.January,
                  Year = "2024",
                  Comment = null,
                  CreateTimestamp = new DateTime(2024, 1, 15),
                  UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary);
                context.SaveChanges();
            }

            // Now, attempt to add another summary
            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 4; // new
                var year = "2024"; // same as in database

                // Act
                bool result = sut.AddNewMonthlyInvoiceSummary(month, year);

                // Assert
                Assert.True(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }

        [Fact]
        public void AddNewMonthlyInvoiceSummary_ShouldAddNewSummary_NewYear_ToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "AddNewMonthlyInvoiceSummary_ShouldAddNewSummary_NewYear_ToDatabase")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = null,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary);
                context.SaveChanges();
            }

            // Now, attempt to add another summary
            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 0; // same as in database
                var year = "2023"; // new

                // Act
                bool result = sut.AddNewMonthlyInvoiceSummary(month, year);

                // Assert
                Assert.True(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }

        [Fact]
        public void AddNewMonthlyInvoiceSummary_ShouldFailAddingADuplicateSummaryToTheDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "AddNewMonthlyInvoiceSummary_ShouldFailAddingASummaryToTheDatabase")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = null,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary);
                context.SaveChanges();
            }

            // Now, attempt to add another summary
            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 0; // same as in database
                var year = "2024"; // same as in database

                // Act
                bool result = sut.AddNewMonthlyInvoiceSummary(month, year);

                // Assert
                Assert.False(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Single(summaries);
            }
        }

        [Fact]
        public void AddNewMonthlyInvoiceSummary_ShouldNotAddASummaryForNonExistingMonth()
        {
            // Arrange
            var sut = new SQLMonthlyInvoiceSummaryRepository(context);
            var month = 13;
            var year = "2024";

            // Act
            bool result = sut.AddNewMonthlyInvoiceSummary(month, year);

            // Assert
            Assert.False(result);
            List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
            Assert.Empty(summaries);
        }
        #endregion

        #region GetCommentByMonthlyInvoiceSummaryId
        [Fact]
        public void GetCommentByMonthlyInvoiceSummaryId_ShouldGetRightComment()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetCommentByMonthlyInvoiceSummaryId_ShouldGetRightComment")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2023",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 1;

                // Act
                string? result = sut.GetCommentByMonthlyInvoiceSummaryId(id);

                // Assert
                Assert.Equal("Test", result);
                Assert.NotNull(result);
                Assert.NotEmpty(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }

        [Fact]
        public void GetCommentByMonthlyInvoiceSummaryId_ShouldGetNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetCommentByMonthlyInvoiceSummaryId_ShouldGetNull")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2023",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 3;

                // Act
                string? result = sut.GetCommentByMonthlyInvoiceSummaryId(id);

                // Assert
                Assert.Null(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }
        #endregion

        #region GetMonthlyInvoiceSummaryId
        [Fact]
        public void GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByDifferentYear()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByDifferentYear")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2023",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 0;
                var year = "2023";

                // Act
                int result = sut.GetMonthlyInvoiceSummaryId(month, year);

                // Assert
                Assert.Equal(2, result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }

        [Fact]
        public void GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByDifferentMonth()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByDifferentMonth")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.February,
                    Year = "2024",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 1;
                var year = "2024";

                // Act
                int result = sut.GetMonthlyInvoiceSummaryId(month, year);

                // Assert
                Assert.Equal(2, result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
            }
        }
        [Fact]
        public void GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByCreatingANewSummary()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetMonthlyInvoiceSummaryId_ShouldGetRightId_ByCreatingANewSummary")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.February,
                    Year = "2024",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var month = 6;
                var year = "2024";

                // Act
                int result = sut.GetMonthlyInvoiceSummaryId(month, year);

                // Assert
                Assert.Equal(3, result); // because in my mocked context id 1 and two are already taken, so 3 will be the next id
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(3, summaries.Count);
            }
        }
        #endregion

        #region UpdateComment 
        [Fact]
        public void UpdateComment_ShouldUpdateRightComment()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "UpdateComment_ShouldUpdateRightComment")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2023",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 1;
                var newComment = "new comment";

                // Act
                bool result = sut.UpdateComment(id, newComment);

                // Assert
                Assert.True(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
                Assert.Equal(newComment, summaries[0].Comment);
                Assert.NotEqual(newComment, summaries[1].Comment);
            }
        }

        [Fact]
        public void UpdateComment_ShouldNotUpdateComment()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "UpdateComment_ShouldNotUpdateComment")
            .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialsummary1 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };
                var initialsummary2 = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 2,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2023",
                    Comment = "Test summary 2",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialsummary1);
                context.MonthlyInvoiceSummaries.Add(initialsummary2);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 5;
                var newComment = "new comment";

                // Act
                bool result = sut.UpdateComment(id, newComment);

                // Assert
                Assert.False(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Equal(2, summaries.Count);
                Assert.NotEqual(newComment, summaries[0].Comment);
                Assert.NotEqual(newComment, summaries[1].Comment);
            }
        }

        [Fact]
        public void UpdateComment_ShouldUpdateCommentToEmptyString()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateComment_ShouldUpdateCommentToEmptyString")
                .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialSummary = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialSummary);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 1;
                var newComment = "";

                // Act
                bool result = sut.UpdateComment(id, newComment);

                // Assert
                Assert.True(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Single(summaries);
                Assert.Equal(newComment, summaries[0].Comment);
            }
        }

        [Fact]
        public void UpdateComment_ShouldNotUpdateCommentToNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateComment_ShouldNotUpdateCommentToNull")
                .Options;

            using (var context = new DataContext(options))
            {
                // Add an initial summary
                var initialSummary = new MonthlyInvoiceSummary
                {
                    MonthlyInvoiceSummaryId = 1,
                    MonthId = Entities.Enums.Month.January,
                    Year = "2024",
                    Comment = "Test",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 1, 15)
                };

                context.MonthlyInvoiceSummaries.Add(initialSummary);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLMonthlyInvoiceSummaryRepository(context);

                var id = 1;
                string? newComment = null;

                // Act
                bool result = sut.UpdateComment(id, newComment);

                // Assert
                Assert.False(result);
                List<MonthlyInvoiceSummary> summaries = context.MonthlyInvoiceSummaries.ToList();
                Assert.Single(summaries);
                Assert.NotEqual(newComment, summaries[0].Comment);
            }
        }
        #endregion

    }
}
