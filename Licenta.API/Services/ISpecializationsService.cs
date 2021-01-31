using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ISpecializationsService
    {
        Task<List<Specialization>> GetSpecializations();
        List<SpecializationForReturnDto> MapSpecializations(List<Specialization> specializations);
        Task<bool> SpecializationExists(Specialization specialization);
        void AddSpecialization(Specialization specialization);
        Task<bool> SaveChangesInContext();
        Task<SpecializationForReturnDto> UpdateSpecialization(Specialization updatedSpecialization);
    }
}
