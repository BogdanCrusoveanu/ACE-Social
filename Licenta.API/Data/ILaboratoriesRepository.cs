using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ILaboratoriesRepository
    {
        Task<List<Laboratory>> GetLaboratoriesForUser(int userId);
        Task<Laboratory> GetLaboratoryById(int laboratoryId);
        Task<List<Laboratory>> GetAll();
    }
}
