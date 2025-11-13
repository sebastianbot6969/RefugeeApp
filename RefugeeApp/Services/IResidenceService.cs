using RefugeeApp.Models;

namespace RefugeeApp.Services
{
    public interface IResidenceService
    {
        Task<IEnumerable<Residence>> GetAllAsync();
        Task<Residence?> GetByIdAsync(int id);
    }
}
