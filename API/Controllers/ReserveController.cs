using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTOs;
using RestaurantAPI.Services.Interfaces;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService _reserveService;

        public ReserveController(IReserveService reserveService)
        {
            _reserveService = reserveService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReserveDTO reserveDTO)
        {
            try
            {
                var result = _reserveService.CreateReserve(reserveDTO);
                return Ok(new { Message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int userId)
        {
            try
            {
                var reserves = _reserveService.ListUserReserves(userId);
                return Ok(reserves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            try
            {
                var result = _reserveService.CancelReserve(id);
                return Ok(new { Message = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}