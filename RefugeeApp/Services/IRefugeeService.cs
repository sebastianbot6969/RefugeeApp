using RefugeeApp.Models;

namespace RefugeeApp.Services
{
    public interface IRefugeeService
    {
        // I IRefugeeService
        Task<IEnumerable<Refugee>> GetFamilyByRefugeeAsync(int refugeeId);
        Task<IEnumerable<Refugee>> SearchAsync(string? firstName, string? lastName, string? familyName, int page = 1, int pageSize = 50);

        Task<IEnumerable<Refugee>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<Refugee?> GetByIdAsync(int id);
        Task<IEnumerable<Refugee>> GetByResidenceAsync(int residenceId);
        Task<IEnumerable<Refugee>> GetByFamilyAsync(int familyId);
        Task AddAsync(Refugee refugee);
    }
}
