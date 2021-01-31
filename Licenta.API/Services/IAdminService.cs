using Licenta.API.Dtos;
using Licenta.API.Models;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IAdminService
    {
        Task<List<User>> GetTeachers();
        List<TeacherDto> MapTeachersForReturn(List<User> teachers);
        Task<List<Semester>> GetSemesters();
        Task<Semester> GetSemester(int id);
        Task<bool> SaveChangesInContext();
        Task<Semester> UpdateSemester(Semester updatedSemester);
        Task<List<Class>> GetClasses();
        Task<Class> GetClassById(int id);
        Class AddClass(ClassForCreateDto newClass);
        bool ClassExists(ClassForCreateDto newClass);
        void DeleteClass(int id);
    }
}
