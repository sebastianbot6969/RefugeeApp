using RefugeeApp.Data;
using RefugeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RefugeeApp.Repositories
{
    public class RefugeeRepository : IRefugeeRepository
    {
        private readonly RefugeeContext _context;

        public RefugeeRepository(RefugeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Refugee>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Refugees
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Refugee?> GetByIdAsync(int id)
        {
            return await _context.Refugees
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Refugee>> GetByResidenceAsync(int residenceId)
        {
            return await _context.Refugees
                .Where(r => r.ResidenceId == residenceId)
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .ToListAsync();
        }

        public async Task<IEnumerable<Refugee>> GetByFamilyAsync(int familyId)
        {
            return await _context.Refugees
                .Where(r => r.FamilyId == familyId)
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .ToListAsync();
        }

        public async Task<IEnumerable<Refugee>> GetFamilyByRefugeeAsync(int refugeeId)
        {
            var refugee = await _context.Refugees.FindAsync(refugeeId);
            if (refugee == null)
                return Enumerable.Empty<Refugee>();

            return await _context.Refugees
                .Where(r => r.FamilyId == refugee.FamilyId && r.Id != refugeeId)
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .ToListAsync();
        }

        public async Task<IEnumerable<Refugee>> SearchAsync(string? firstName, string? lastName, string? familyName, int page, int pageSize)
        {
            var query = _context.Refugees
                .Include(r => r.Residence)
                .Include(r => r.Family)
                .AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(r => r.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(r => r.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(familyName))
            {
                query = query.Where(r => r.Family != null && r.Family.FamilyName.Contains(familyName));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(Refugee refugee)
        {
            _context.Refugees.Add(refugee);
            await _context.SaveChangesAsync();
        }
    }
}
