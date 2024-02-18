﻿using HousekeepingBook.Controllers;
using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HousekeepingBook.Tests.Controllers
{
    public class SettingsControllerTests
    {
        #region GetSettingsById
        [Fact]
        public void GetSettingsById_ReturnsOk_WithSettings()
        {
            // Arrange
            var settings = new Settings
            {
                SettingsId = 1,
                ContributionMembersCount = 1,
                PreferredColorMode = "light",
                CreateTimestamp = new DateTime(2024, 1, 15),
                UpdateTimestamp = new DateTime(2024, 2, 15),
            };
            
            var settingRepositoryMock = new Mock<ISettingRepository>();
            settingRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return settings;
                });

            var controller = new SettingsController(settingRepositoryMock.Object);

            var id = 1;

            // Act
            var result = controller.GetSettingsById(id);

            // Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            Assert.Equal(settings, statusCodeResult.Value);
        }

        [Fact]
        public void GetSettingsById_ReturnsNotFound_SettingsIsNull()
        {
            // Arrange
            var settingRepositoryMock = new Mock<ISettingRepository>();
            settingRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return null;
                });

            var controller = new SettingsController(settingRepositoryMock.Object);

            var id = 1;

            // Act
            var result = controller.GetSettingsById(id);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
            Assert.Equal("No settings found for id 1", statusCodeResult.Value);
        }

        [Fact]
        public void GetSettingsById_ReturnsInternalError()
        {
            // Arrange
            var settingRepositoryMock = new Mock<ISettingRepository>();
            settingRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>()))
                .Throws(new Exception("Simulated error"));

            var controller = new SettingsController(settingRepositoryMock.Object);

            var id = 1;

            // Act
            var result = controller.GetSettingsById(id);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error occurred while executing GetSettingsById: Simulated error", statusCodeResult.Value);
        }
        #endregion

        #region UpdateSettingsById
        [Fact]
        public void UpdateSettingsById_ReturnsOk()
        {
            // Arrange
            var settingsRepositoryMock = new Mock<ISettingRepository>();
            settingsRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>())).Returns((int id) =>
            {
                return new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    PreferredColorMode = "light",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
            });

            settingsRepositoryMock.Setup(repo => repo.UpdateSettingsById(It.IsAny<Settings>())).Returns(true);

            var controller = new SettingsController(settingsRepositoryMock.Object);

            var model = new UpdateSettingsModel
            {
                SettingsId = 1,
                ContributionMembersCount = 5,
                PreferredColorMode = "light",
            };

            // Act
            var result = controller.UpdateSettingsById(model);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void UpdateSettingsById_ReturnsNotFound_BecauseSettingsIsNull()
        {
            // Arrange
            var settingsRepositoryMock = new Mock<ISettingRepository>();
            settingsRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>())).Returns((int id) =>
            {
                return null;
            });

            var controller = new SettingsController(settingsRepositoryMock.Object);

            var model = new UpdateSettingsModel
            {
                SettingsId = 1,
                ContributionMembersCount = 5,
                PreferredColorMode = "light",
            };

            // Act
            var result = controller.UpdateSettingsById(model);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
            Assert.Equal("No settings found for id 1", statusCodeResult.Value);
        }

        [Fact]
        public void UpdateSettingsById_ReturnsNotFound_BecauseSettingsIsNotUpdated()
        {
            // Arrange
            var settingsRepositoryMock = new Mock<ISettingRepository>();
            settingsRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>())).Returns((int id) =>
            {
                return new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    PreferredColorMode = "light",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
            });

            settingsRepositoryMock.Setup(repo => repo.UpdateSettingsById(It.IsAny<Settings>())).Returns(false);

            var controller = new SettingsController(settingsRepositoryMock.Object);

            var model = new UpdateSettingsModel
            {
                SettingsId = 1,
                ContributionMembersCount = 5,
                PreferredColorMode = "light",
            };

            // Act
            var result = controller.UpdateSettingsById(model);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
            Assert.Equal("Settings with id 1 not updated.", statusCodeResult.Value);
        }

        [Fact]
        public void UpdateSettingsById_ReturnsInternalError()
        {
            // Arrange
            var settingsRepositoryMock = new Mock<ISettingRepository>();
            settingsRepositoryMock.Setup(repo => repo.GetSettingsById(It.IsAny<int>())).Returns((int id) =>
            {
                return new Settings
                {
                    SettingsId = 1,
                    ContributionMembersCount = 1,
                    PreferredColorMode = "light",
                    CreateTimestamp = new DateTime(2024, 1, 15),
                    UpdateTimestamp = new DateTime(2024, 2, 15),
                };
            });

            settingsRepositoryMock.Setup(repo => repo.UpdateSettingsById(It.IsAny<Settings>())).Throws(new Exception("Simulated error"));

            var controller = new SettingsController(settingsRepositoryMock.Object);

            var model = new UpdateSettingsModel
            {
                SettingsId = 1,
                ContributionMembersCount = 5,
                PreferredColorMode = "light",
            };

            // Act
            var result = controller.UpdateSettingsById(model);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error occurred while executing UpdateSettingsById: Simulated error", statusCodeResult.Value);
        }
        #endregion
    }
}
