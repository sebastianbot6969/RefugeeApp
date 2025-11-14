using RefugeeApp.Models;

namespace RefugeeApp.Repositories
{
    public interface IRefugeeRepository
    {
        Task<IEnumerable<Refugee>> GetAllAsync(int page, int pageSize);
        Task<Refugee?> GetByIdAsync(int id);
        Task<IEnumerable<Refugee>> GetByResidenceAsync(int residenceId);
        Task<IEnumerable<Refugee>> GetByFamilyAsync(int familyId);
        Task<IEnumerable<Refugee>> GetFamilyByRefugeeAsync(int refugeeId);
        Task<IEnumerable<Refugee>> SearchAsync(string? firstName, string? lastName, string? familyName, int page, int pageSize);
        Task AddAsync(Refugee refugee);
        Task UpdateAsync(Refugee refugee);
    }
}
