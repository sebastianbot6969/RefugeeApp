using RefugeeApp.Models;
using RefugeeApp.Repositories;

namespace RefugeeApp.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _repository;

        public FamilyService(IFamilyRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Family>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Family?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    }
}
