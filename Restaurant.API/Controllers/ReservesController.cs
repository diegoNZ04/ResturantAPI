using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.Services.Interfaces;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservesController : ControllerBase
{
    private readonly IReserveService _reserveService;
    public ReservesController(IReserveService reserveService)
    {
        _reserveService = reserveService;
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpPost("create-reserve")]
    public async Task<IActionResult> CreateReserve([FromBody] CreateReserveRequest request)
    {
        var reserve = await _reserveService.CreateReserveAsync(request);
        return CreatedAtAction(nameof(CreateReserve), new { id = reserve.Id }, reserve);
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpDelete("delete-reserve/{id}")]
    public async Task<IActionResult> DeleteReserve(int id)
    {
        var reserve = await _reserveService.GetReserveByIdAsync(id);

        if (reserve == null)
            return NotFound($"Reserve with ID {id} not found.");

        await _reserveService.DeleteReserveAsync(id);

        return NoContent();
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpGet("get-all-reserves")]
    public async Task<IActionResult> GetAllReserves([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var reserves = await _reserveService.GetAllReservesAsync(page, pageSize);

        if (!reserves.Any())
            return NoContent();

        return Ok(reserves);
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpGet("get-reserve-by-id/{id}")]
    public async Task<IActionResult> GetReserveById(int id)
    {
        var reserve = await _reserveService.GetReserveByIdAsync(id);

        if (reserve == null)
            return NotFound($"Reserve with ID {id} not found.");

        return Ok(reserve);
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpPut("update-reserve/{id}")]
    public async Task<IActionResult> UpdateReserve([FromBody] UpdateReserveRequest request, int id)
    {
        var reserve = await _reserveService.GetReserveByIdAsync(id);

        if (reserve == null)
            return NotFound($"Reserve with ID {id} not found.");

        var updatedReserve = await _reserveService.UpdateReserveAsync(id, request);

        return Ok(updatedReserve);
    }
}
