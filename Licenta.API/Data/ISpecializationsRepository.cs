using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ISpecializationsRepository
    {
        Specialization GetSpecializationByName(string name);
        Task<Specialization> GetSpecializationByUser(int userId);
        Task<List<Specialization>> GetSpecializations();
        Task<Specialization> GetSpecializationById(int id);
    }
}
