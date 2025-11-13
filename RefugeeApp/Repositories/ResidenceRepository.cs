using RefugeeApp.Data;
using RefugeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RefugeeApp.Repositories
{
    public class ResidenceRepository : IResidenceRepository
    {
        private readonly RefugeeContext _context;

        public ResidenceRepository(RefugeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Residence>> GetAllAsync()
        {
            return await _context.Residences
                .Include(r => r.Residents)
                .ThenInclude(x => x.Family)
                .ToListAsync();
        }

        public async Task<Residence?> GetByIdAsync(int id)
        {
            return await _context.Residences
                .Include(r => r.Residents)
                .ThenInclude(x => x.Family)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
