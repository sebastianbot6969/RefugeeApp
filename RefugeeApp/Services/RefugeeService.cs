using RefugeeApp.Models;
using RefugeeApp.Repositories;
using RefugeeApp.Data;

namespace RefugeeApp.Services
{
    public class RefugeeService : IRefugeeService
    {
        private readonly IRefugeeRepository _repository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly RefugeeContext _context;

        public RefugeeService(IRefugeeRepository repository, IFamilyRepository familyRepository, IResidenceRepository residenceRepository, RefugeeContext context)
        {
            _repository = repository;
            _familyRepository = familyRepository;
            _residenceRepository = residenceRepository;
            _context = context;
        }
        public Task<IEnumerable<Refugee>> GetFamilyByRefugeeAsync(int refugeeId)
            => _repository.GetFamilyByRefugeeAsync(refugeeId);

        public Task<IEnumerable<Refugee>> SearchAsync(string? firstName, string? lastName, string? familyName, int page = 1, int pageSize = 50)
            => _repository.SearchAsync(firstName, lastName, familyName, page, pageSize);

        public async Task<IEnumerable<Refugee>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 50;

            return await _repository.GetAllAsync(page, pageSize);
        }


        public Task<Refugee?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<Refugee>> GetByResidenceAsync(int residenceId)
            => _repository.GetByResidenceAsync(residenceId);

        public Task<IEnumerable<Refugee>> GetByFamilyAsync(int familyId)
            => _repository.GetByFamilyAsync(familyId);

        public async Task AddAsync(Refugee refugee)
        {
            // 1️⃣ Validering af basale værdier
            if (string.IsNullOrWhiteSpace(refugee.FirstName) || string.IsNullOrWhiteSpace(refugee.LastName))
                throw new ArgumentException("First name and last name cannot be empty.");

            if (refugee.DateOfBirth > DateTime.UtcNow)
                throw new ArgumentException("Date of birth cannot be in the future.");

            // 2️⃣ Validering af Residence
            var residenceExists = await _residenceRepository.GetByIdAsync(refugee.ResidenceId);
            if (residenceExists == null)
                throw new ArgumentException($"Residence with ID {refugee.ResidenceId} does not exist.");


            // 4️⃣ Family-håndtering
            if (refugee.FamilyId == null || refugee.FamilyId == 0)
            {
                // Opret ny familie, hvis der ikke findes en
                var newFamily = new Family { FamilyName = refugee.LastName };
                newFamily = await _familyRepository.AddAsync(newFamily);
                refugee.FamilyId = newFamily.Id;
            }
            await _repository.AddAsync(refugee);
        }
    }
}

