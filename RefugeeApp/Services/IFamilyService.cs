using RefugeeApp.Models;

namespace RefugeeApp.Services
{
    public interface IFamilyService
    {
        Task<IEnumerable<Family>> GetAllAsync();
        Task<Family?> GetByIdAsync(int id);
    }
}
