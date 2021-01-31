using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using Licenta.Data;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IMapper _mapper;
        private readonly ISemestersRepository _semestersRepo;
        private readonly IGenericsRepository _genericsRepo;
        private readonly IClassesRepository _classesRepo;

        public AdminService(IUsersRepository usersRepo, IMapper mapper, ISemestersRepository semestersRepo,
            IGenericsRepository genericsRepo, IClassesRepository classesRepo)
        {
            _usersRepo = usersRepo;
            _mapper = mapper;
            _semestersRepo = semestersRepo;
            _genericsRepo = genericsRepo;
            _classesRepo = classesRepo;
        }

        public Class AddClass(ClassForCreateDto newClass)
        {
            var addedClass = _mapper.Map<Class>(newClass);

            _genericsRepo.Add(addedClass);

            return addedClass;
        }

        public bool ClassExists(ClassForCreateDto newClass)
        {
            var classes = GetClasses().Result;

            foreach (var existentClass in classes)
            {
                if (newClass.Name == existentClass.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteClass(int id)
        {
            var classToDelete =  _classesRepo.GetClassById(id).Result;
            _genericsRepo.Delete(classToDelete);
        }

        public async Task<Class> GetClassById(int id)
        {
            return await _classesRepo.GetClassById(id);
        }

        public async Task<List<Class>> GetClasses()
        {
            return await _classesRepo.GetClasses();
        }

        public async Task<Semester> GetSemester(int id)
        {
            return await _semestersRepo.GetSemesterById(id);
        }

        public async Task<List<Semester>> GetSemesters()
        {
            return await _semestersRepo.GetSemesters();
        }

        public async Task<List<User>> GetTeachers()
        {
            return await _usersRepo.GetTeachers();
        }

        public List<TeacherDto> MapTeachersForReturn(List<User> teachers)
        {
            return _mapper.Map<List<TeacherDto>>(teachers);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<Semester> UpdateSemester(Semester updatedSemester)
        {
            var semester = await GetSemester(updatedSemester.Id);

            semester.StartDate = updatedSemester.StartDate.LocalDateTime;

            semester.EndDate = updatedSemester.EndDate.LocalDateTime;

            return semester;
        }
    }
}
