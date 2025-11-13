using Microsoft.AspNetCore.Mvc;
using RefugeeApp.Models;
using RefugeeApp.Services;
using RefugeeApp.Models.DTOs;
using RefugeeApp.Mappers;

namespace RefugeeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefugeesController : ControllerBase
    {
        private readonly IRefugeeService _service;

        public RefugeesController(IRefugeeService service)
        {
            _service = service;
        }

        // GET: api/refugees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefugeeDto>>> GetAll()
        {
            var refugees = await _service.GetAllAsync();
            var refugeeDtos = refugees.ToDtoList();
            return Ok(refugeeDtos);
        }

        // GET: api/refugees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Refugee>> GetById(int id)
        {
            var refugee = await _service.GetByIdAsync(id);
            if (refugee == null)
                return NotFound();
            return Ok(refugee);
        }

        // GET: api/refugees/residence/{residenceId}
        [HttpGet("residence/{residenceId}")]
        public async Task<ActionResult<IEnumerable<Refugee>>> GetByResidence(int residenceId)
        {
            var refugees = await _service.GetByResidenceAsync(residenceId);
            return Ok(refugees);
        }

        // GET: api/refugees/family/{familyId}
        [HttpGet("family/{familyId}")]
        public async Task<ActionResult<IEnumerable<Refugee>>> GetByFamily(int familyId)
        {
            var refugees = await _service.GetByFamilyAsync(familyId);
            return Ok(refugees);
        }

        // NEW: GET: api/refugees/family/byrefugee/{refugeeId}
        [HttpGet("family/byrefugee/{refugeeId}")]
        public async Task<ActionResult<IEnumerable<Refugee>>> GetFamilyByRefugee(int refugeeId)
        {
            var familyMembers = await _service.GetFamilyByRefugeeAsync(refugeeId);
            if (familyMembers == null || !familyMembers.Any())
                return NotFound();
            return Ok(familyMembers);
        }

        // NEW (optional): GET: api/refugees/search?firstName=...&lastName=...&familyName=...
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Refugee>>> Search([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? familyName, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var refugees = await _service.SearchAsync(firstName, lastName, familyName, page, pageSize);
            return Ok(refugees);
        }

        // POST: api/refugees
        [HttpPost]
        public async Task<ActionResult<RefugeeDto>> AddRefugee([FromBody] CreateRefugeeDto dto)
        {
            // ✅ Basic model validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ Ekstra validering (eksempel)
            if (dto.DateOfBirth > DateTime.Now)
                return BadRequest("DateOfBirth kan ikke være i fremtiden.");

            var refugee = dto.ToEntity();

            await _service.AddAsync(refugee);

            // Returnér DTO tilbage til klienten
            return CreatedAtAction(nameof(GetById), new { id = refugee.Id }, refugee.ToDto());
        }
    }
}
