using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models.Constants;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Service.Interface;

namespace RestaurantManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {
        [HttpPost(Constants.Create)]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            return Ok(await reservationService.Create(dto));
        }
    }
}
