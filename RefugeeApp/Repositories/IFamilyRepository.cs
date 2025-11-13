using RefugeeApp.Models;

namespace RefugeeApp.Repositories
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetAllAsync();
        Task<Family?> GetByIdAsync(int id);
        Task<Family> AddAsync(Family family); 
    }
}
