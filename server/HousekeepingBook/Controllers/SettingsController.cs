using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousekeepingBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingRepository _settingRepository;

        public SettingsController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        [HttpPost("getSettingsById")]
        public IActionResult GetSettingsById([FromBody] int id)
        {
            try
            {
                Settings? settings = _settingRepository.GetSettingsById(id);
                if (settings == null)
                {
                    return NotFound($"No settings found for id {id}");
                }

                return Ok(settings);
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing GetSettingsById: " + ex.Message);
            }
        }

        [HttpPut("updateSettingsById")]
        public IActionResult UpdateSettingsById([FromBody] UpdateSettingsModel model)
        {
            try
            {
                Settings? oldSettings = _settingRepository.GetSettingsById(model.SettingsId);
                if (oldSettings == null)
                {
                    return NotFound($"No settings found for id {model.SettingsId}");
                }

                Settings newModel = new Settings()
                {
                    SettingsId = model.SettingsId,
                    ContributionMembersCount = model.ContributionMembersCount,
                    Year = model.Year,
                    MonthId = model.MonthId,
                    CreateTimestamp = oldSettings.CreateTimestamp,
                    UpdateTimestamp = DateTime.Now,
                };

                bool settingsUpdated = _settingRepository.UpdateSettingsById(newModel);

                return settingsUpdated ? Ok() : NotFound($"Settings with id {model.SettingsId} not updated.");

            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing UpdateSettingsById: " + ex.Message);

            }
        }
    }
}
