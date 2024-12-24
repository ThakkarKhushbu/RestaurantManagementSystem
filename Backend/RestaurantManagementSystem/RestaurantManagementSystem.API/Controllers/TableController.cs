using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models.Constants;
using RestaurantManagementSystem.Models.DTOs;
using RestaurantManagementSystem.Service.Interface;

namespace RestaurantManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController(ITableService tableService) : ControllerBase
    {
        [HttpPost(Constants.GetAll)]
        public async Task<IActionResult> GetAll([FromBody] Filters filter)
        {
            return Ok(await tableService.GetAll(filter));
        }
    }
}
