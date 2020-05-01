using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ISemestersRepository
    {
        Task<Semester> GetSemesterById(int id);
        Task<List<Semester>> GetSemesters();
        Task<Semester> GetSemesterByDate();
    }
}
