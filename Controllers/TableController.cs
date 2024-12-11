using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTOs;
using RestaurantAPI.Interfaces;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public ActionResult<List<TableDTO>> GetAll()
        {
            try
            {
                var result = _tableService.GetAllTables();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add([FromBody] TableDTO tableDTO)
        {
            try
            {
                var result = _tableService.CreateTable(tableDTO);
                return Ok(new { Message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] TableDTO tableUpdated)
        {
            try
            {
                _tableService.UpdateTable(id, tableUpdated);
                return Ok(new { Message = "Mesa atualizada!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _tableService.DeleteTableById(id);
                return Ok(new { Message = "Mesa removida!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}