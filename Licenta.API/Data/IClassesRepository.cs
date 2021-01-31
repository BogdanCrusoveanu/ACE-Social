using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IClassesRepository
    {
        Task<List<Class>> GetClasses();
        Task<Class> GetClassById(int id);
    }
}
