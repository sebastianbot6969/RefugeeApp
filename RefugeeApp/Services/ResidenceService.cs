using RefugeeApp.Models;
using RefugeeApp.Repositories;

namespace RefugeeApp.Services
{
    public class ResidenceService : IResidenceService
    {
        private readonly IResidenceRepository _repository;

        public ResidenceService(IResidenceRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Residence>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Residence?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    }
}
