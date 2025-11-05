using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSalesAPI.Models;
using ProductSalesAPI.Services;

namespace ProductSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;

        public SalesController(ISaleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sale>>> GetAll()
        {
            var sales = await _service.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = await _service.GetByIdAsync(id);

            if (sale == null)
                return NotFound(new { message = "Sale not found" });

            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Create([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}