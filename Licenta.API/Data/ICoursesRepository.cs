using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
