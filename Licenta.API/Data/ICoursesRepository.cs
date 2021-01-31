using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ICoursesRepository
    {
        Task<List<Course>> GetCoursesForUser(int userId);
        Task<List<Course>> GetAll();
        Task<Course> GetCourseById(int id);
    }
}
