using RefugeeApp.Models;
using RefugeeApp.Models.DTOs;

namespace RefugeeApp.Mappers
{
    public static class RefugeeMapper
    {
        public static RefugeeDto ToDto(this Refugee refugee)
        {
            return new RefugeeDto
            {
                Id = refugee.Id,
                FirstName = refugee.FirstName,
                LastName = refugee.LastName,
                DateOfBirth = refugee.DateOfBirth,
                Age = refugee.Age,
                ResidenceName = refugee.Residence?.Name,
                FamilyName = refugee.Family?.FamilyName
            };
        }

        public static IEnumerable<RefugeeDto> ToDtoList(this IEnumerable<Refugee> refugees)
        {
            return refugees.Select(r => r.ToDto());
        }

        public static Refugee ToEntity(this CreateRefugeeDto dto)
        {
            var age = (int)((DateTime.Now - dto.DateOfBirth).TotalDays / 365.25);

            return new Refugee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Age = age,
                ResidenceId = dto.ResidenceId,
                FamilyId = dto.FamilyId
            };
        }
    }
}
