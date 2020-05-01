using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ICoursesService
    {
        Task<List<Course>> GetCoursesForUser(int userId);
        List<ActivityForReturnDto> GetActivitiesForUser(List<Course> courses, List<Laboratory> laboratories,
            List<Seminar> seminars, List<CompanyPresentation> companyPresentations);
        Task<bool> CourseExists(Course course, int id);
        void AddCourse(Course course);
        List<CourseForUpdateDto> MapCoursesForAdmin(List<Course> courses);
        Task<CourseForUpdateDto> UpdateCourse(Course course);
    }
}
