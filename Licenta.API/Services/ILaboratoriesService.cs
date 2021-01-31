using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ILaboratoriesService
    {
        Task<List<Laboratory>> GetLaboratoriesForUser(int userId);
        void AddLaboratory(Laboratory laboratory);
        Task<bool> LaboratoryExists(Laboratory addedLaboratory, int id);
        List<LaboratoryForUpdateDto> MapLaboratoriesForAdmin(List<Laboratory> laboratories);
        Task<LaboratoryForUpdateDto> UpdateLaboratory(Laboratory laboratory);
    }
}
