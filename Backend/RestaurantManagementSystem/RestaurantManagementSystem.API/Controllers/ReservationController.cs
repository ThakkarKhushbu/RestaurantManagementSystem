using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Models.Constants;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Models.Filters;

namespace RestaurantManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController(IReservationService reservationService, ILogService log) : ControllerBase
    {
        [HttpPost(Constants.Create)]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            try
            {
                Models.Models.Reservation response = await reservationService.CreateReservationAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Constants.GetAll)]
        public async Task<IActionResult> GetReservations([FromQuery] Filters filter)
        {
            try
            {
                List<Models.Models.Reservation> response = await reservationService.GetReservationsAsync(filter);
                return Ok(response);
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(Constants.Cancel)]
        public async Task<IActionResult> CancelReservation([FromQuery]Guid id)
        {
            try
            {
                bool response = await reservationService.CancelReservationAsync(id);
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
