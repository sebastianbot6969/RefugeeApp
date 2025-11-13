using Microsoft.AspNetCore.Mvc;
using RefugeeApp.Models;
using RefugeeApp.Services;

namespace RefugeeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamilyService _service;

        public FamiliesController(IFamilyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetAll()
        {
            var families = await _service.GetAllAsync();
            return Ok(families);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetById(int id)
        {
            var family = await _service.GetByIdAsync(id);
            if (family == null)
                return NotFound();
            return Ok(family);
        }
    }
}
