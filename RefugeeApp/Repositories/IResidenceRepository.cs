using RefugeeApp.Models;

namespace RefugeeApp.Repositories
{
    public interface IResidenceRepository
    {
        Task<IEnumerable<Residence>> GetAllAsync();
        Task<Residence?> GetByIdAsync(int id);
    }
}
