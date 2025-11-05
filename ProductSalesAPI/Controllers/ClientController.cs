using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSalesAPI.Models;
using ProductSalesAPI.Services;

namespace ProductSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAll()
        {
            var clients = await _service.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _service.GetByIdAsync(id);

            if (client == null)
                return NotFound(new { message = "Client not found" });

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update(int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            client.Id = id;
            var updated = await _service.UpdateAsync(client);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}