using Microsoft.AspNetCore.Mvc;
using RefugeeApp.Models;
using RefugeeApp.Services;

namespace RefugeeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidencesController : ControllerBase
    {
        private readonly IResidenceService _service;

        public ResidencesController(IResidenceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residence>>> GetAll()
        {
            var residences = await _service.GetAllAsync();
            return Ok(residences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Residence>> GetById(int id)
        {
            var residence = await _service.GetByIdAsync(id);
            if (residence == null)
                return NotFound();
            return Ok(residence);
        }
    }
}
