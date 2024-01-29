using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using Microsoft.EntityFrameworkCore;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Repositories;

namespace HousekeepingBook.Tests.Repositories
{
    public class SQLSettingRepositoryTests
    {
        private readonly DataContext context;

        public SQLSettingRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            context = new DataContext((DbContextOptions<DataContext>)dbOptions.Options);
        }

        #region GetSettingsById
        [Fact]
        public void GetSettingsById_ShouldGetCorrectSettingsById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetSettingsById_ShouldGetCorrectSettingsById")
                .Options;

            var initialSettings = new Settings
            {
                SettingsId = 1,
                ContributionMembersCount = 1,
                Year = "2023",
                MonthId = Month.March,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 2, 15),
            };

            using (var initialContext = new DataContext(options))
            {
                // Add settings
                initialContext.Settings.Add(initialSettings);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLSettingRepository(context);

                // Act
                Settings? result = sut.GetSettingsById(1);

                // Assert
                Assert.Equal(result?.SettingsId, initialSettings.SettingsId);
            }
        }

        [Fact]
        public void GetSettingsById_ShouldGetNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetSettingsById_ShouldGetNull")
                .Options;

            var initialSettings1 = new Settings
            {
                SettingsId = 1,
                ContributionMembersCount = 1,
                Year = "2023",
                MonthId = Month.March,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 2, 15),
            };
            var initialSettings2 = new Settings
            {
                SettingsId = 2,
                ContributionMembersCount = 6,
                Year = "2024",
                MonthId = Month.March,
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 2, 15),
            };

            using (var initialContext = new DataContext(options))
            {
                // Add settings
                initialContext.Settings.Add(initialSettings1);
                initialContext.Settings.Add(initialSettings2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLSettingRepository(context);

                // Act
                Settings? result = sut.GetSettingsById(3);

                // Assert
                Assert.Null(result);
            }
        }
        #endregion

        #region UpdateSettingsById
        [Fact]
        public void UpdateSettingsById_ShouldUpdateSettings()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateSettingsById_ShouldUpdateSettings")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial settings
                var initialSettings1 = new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    Year = "2023",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
                var initialSettings2 = new Settings
                {
                    SettingsId = 2,
                    ContributionMembersCount = 6,
                    Year = "2024",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };

                initialContext.Settings.Add(initialSettings1);
                initialContext.Settings.Add(initialSettings2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLSettingRepository(context);

                Settings newModel = new Settings()
                {
                    SettingsId = 1,
                    ContributionMembersCount = 9,
                    Year = "2025",
                    MonthId = Month.October,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 18),
                };

                // Act
                bool result = sut.UpdateSettingsById(newModel);

                // Assert
                Assert.True(result);
                List<Settings> settings = context.Settings.ToList();
                Assert.Equal(2, settings.Count());
                Assert.Equal(newModel.SettingsId, settings[0].SettingsId);
                Assert.Equal(newModel.ContributionMembersCount, settings[0].ContributionMembersCount);
                Assert.Equal(newModel.Year, settings[0].Year); 
                Assert.Equal(newModel.MonthId, settings[0].MonthId);
                Assert.Equal(newModel.UpdateTimestamp, settings[0].UpdateTimestamp);
            }
        }

        [Fact]
        public void UpdateSettingsById_ShouldNotUpdateSettings()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateSettingsById_ShouldNotUpdateSettings")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial settings
                var initialSettings1 = new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    Year = "2023",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
                var initialSettings2 = new Settings
                {
                    SettingsId = 2,
                    ContributionMembersCount = 6,
                    Year = "2024",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };

                initialContext.Settings.Add(initialSettings1);
                initialContext.Settings.Add(initialSettings2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLSettingRepository(context);

                Settings newModel = new Settings()
                {
                    SettingsId = 3,
                    ContributionMembersCount = 9,
                    Year = "2025",
                    MonthId = Month.October,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 18),
                };

                // Act
                bool result = sut.UpdateSettingsById(newModel);

                // Assert
                Assert.False(result);
                List<Settings> settings = context.Settings.ToList();
                Assert.Equal(2, settings.Count());
                Assert.NotEqual(newModel.SettingsId, settings[0].SettingsId);
                Assert.NotEqual(newModel.ContributionMembersCount, settings[0].ContributionMembersCount);
                Assert.NotEqual(newModel.Year, settings[0].Year);
                Assert.NotEqual(newModel.MonthId, settings[0].MonthId);
                Assert.NotEqual(newModel.UpdateTimestamp, settings[0].UpdateTimestamp);
            }
        }

        [Fact]
        public void UpdateSettingsById_ShouldNotUpdateSettings_Because_Count_Is_0()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateSettingsById_ShouldNotUpdateSettings_Because_Count_Is_0")
                .Options;

            using (var initialContext = new DataContext(options))
            {
                // Add an initial settings
                var initialSettings1 = new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    Year = "2023",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
                var initialSettings2 = new Settings
                {
                    SettingsId = 2,
                    ContributionMembersCount = 6,
                    Year = "2024",
                    MonthId = Month.March,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };

                initialContext.Settings.Add(initialSettings1);
                initialContext.Settings.Add(initialSettings2);
                initialContext.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var sut = new SQLSettingRepository(context);

                Settings newModel = new Settings()
                {
                    SettingsId = 1,
                    ContributionMembersCount = 0,
                    Year = "2025",
                    MonthId = Month.October,
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 18),
                };

                // Act
                bool result = sut.UpdateSettingsById(newModel);

                // Assert
                Assert.False(result);
                List<Settings> settings = context.Settings.ToList();
                Assert.Equal(2, settings.Count());
                Assert.Equal(newModel.SettingsId, settings[0].SettingsId);
                Assert.NotEqual(newModel.ContributionMembersCount, settings[0].ContributionMembersCount);
                Assert.NotEqual(newModel.Year, settings[0].Year);
                Assert.NotEqual(newModel.MonthId, settings[0].MonthId);
                Assert.NotEqual(newModel.UpdateTimestamp, settings[0].UpdateTimestamp);
            }
        }
        #endregion
    }
}
