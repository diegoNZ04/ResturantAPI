using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.Services.Interfaces;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/tables")]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;
    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpPost("create-table")]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableRequest request)
    {
        var table = await _tableService.CreateTableAsync(request);
        return CreatedAtAction(nameof(CreateTable), new { id = table.Id }, table);
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpDelete("delete-table/{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var table = await _tableService.GetTableByIdAsync(id);

        if (table == null)
            return NotFound($"Table with ID {id} not found.");

        await _tableService.DeleteTableAsync(id);

        return NoContent();
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpGet("get-all-tables")]
    public async Task<IActionResult> GetAllTables([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var tables = await _tableService.GetAllTablesAsync(page, pageSize);

        if (!tables.Any())
            return NoContent();

        return Ok(tables);
    }


    [Authorize(Roles = "Adm, Client")]
    [HttpGet("get-table-by-table/{id}")]
    public async Task<IActionResult> GetTableById(int id)
    {
        var table = await _tableService.GetTableByIdAsync(id);

        if (table == null)
            return NotFound($"Table with ID {id} not found.");

        return Ok(table);
    }

    [Authorize(Roles = "Adm, Client")]
    [HttpPatch("update-table/{id}")]
    public async Task<IActionResult> UpdateTable([FromBody] UpdateTableRequest request, int id)
    {
        var table = await _tableService.GetTableByIdAsync(id);

        if (table == null)
            return NotFound($"Table with ID {id} not found.");

        var updatedTable = await _tableService.UpdateTableAsync(id, request);

        return Ok(updatedTable);
    }
}
