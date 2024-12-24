using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Models.Constants;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Filters;
using RestaurantManagementSystem.Models.Models;

namespace RestaurantManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController(ITableService tableService, ILogService log) : ControllerBase
    {
        [HttpGet(Constants.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] Filters filter)
        {
            try
            {
                List<TableWithReservationsDto> response = await tableService.GetTablesAsync(filter);
                return Ok(response);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost(Constants.Create)]
        public async Task<ActionResult<Table>> AddTable([FromBody] CreateTableDto dto)
        {
            try
            {
                Table response = await tableService.CreateTableAsync(dto);
                log.LogInformation($"Table {response.Id} created successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
