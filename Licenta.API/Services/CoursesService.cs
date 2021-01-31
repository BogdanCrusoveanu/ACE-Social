using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository _coursesRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public CoursesService(ICoursesRepository coursesRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _coursesRepo = coursesRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void AddCourse(Course course)
        {
            course.StartDate = course.StartDate.LocalDateTime;
            course.EndDate = course.EndDate.LocalDateTime;
            _genericsRepo.Add(course);
        }

        public async Task<bool> CourseExists(Course course, int id)
        {
            var courses =  await GetCoursesForUser(id);

            foreach (var item in courses)
            {
                if (course.Name == item.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public List<ActivityForReturnDto> GetActivitiesForUser(List<Course> courses, List<Laboratory> laboratories, List<Seminar> seminars, List<CompanyPresentation> companyPresentations)
        {
            var activitiesForReturn = _mapper.Map<List<ActivityForReturnDto>>(courses);
            var seminarsForReturn = _mapper.Map<List<ActivityForReturnDto>>(seminars);
            var laboratoriesForReturn = _mapper.Map<List<ActivityForReturnDto>>(laboratories);
            var companiesForReturn = _mapper.Map<List<ActivityForReturnDto>>(companyPresentations);
            activitiesForReturn.AddRange(seminarsForReturn);
            activitiesForReturn.AddRange(laboratoriesForReturn);
            activitiesForReturn.AddRange(companiesForReturn);
            return activitiesForReturn;
        }

        public List<CourseForUpdateDto> MapCoursesForAdmin(List<Course> courses)
        {
            var activitiesForReturn = _mapper.Map<List<CourseForUpdateDto>>(courses);

            return activitiesForReturn;
        }

        public async Task<List<Course>> GetCoursesForUser(int userId)
        {
             return await _coursesRepo.GetCoursesForUser(userId);
        }

        public async Task<CourseForUpdateDto> UpdateCourse(Course course)
        {
            var currentCourse = await _coursesRepo.GetCourseById(course.Id);

            currentCourse.Name = course.Name;
            currentCourse.StartDate = course.StartDate.LocalDateTime;
            currentCourse.EndDate = course.EndDate.LocalDateTime;
            currentCourse.TeacherId = course.TeacherId;
            currentCourse.SpecializationId = course.SpecializationId;
            currentCourse.ClassId = course.ClassId;

            var mappedCourse = _mapper.Map<CourseForUpdateDto>(currentCourse);

            return mappedCourse;
        }
    }
}
