using RefugeeApp.Data;
using RefugeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RefugeeApp.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly RefugeeContext _context;

        public FamilyRepository(RefugeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Family>> GetAllAsync()
        {
            return await _context.Families
                .Include(f => f.Members)
                .ThenInclude(r => r.Residence)
                .ToListAsync();
        }

        public async Task<Family?> GetByIdAsync(int id)
        {
            return await _context.Families
                .Include(f => f.Members)
                .ThenInclude(r => r.Residence)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Family> AddAsync(Family family)
        {
            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            return family; 
        }
    }
}
