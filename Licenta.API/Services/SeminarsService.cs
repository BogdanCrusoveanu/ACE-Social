using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class SeminarsService : ISeminarsService
    {
        private readonly ISeminarsRepository _seminarsRepo;
        private readonly IGenericsRepository _genericsRepo;
        private readonly IMapper _mapper;

        public SeminarsService(ISeminarsRepository seminarsRepo, IGenericsRepository genericsRepo, IMapper mapper)
        {
            _seminarsRepo = seminarsRepo;
            _genericsRepo = genericsRepo;
            _mapper = mapper;
        }

        public void AddSeminar(Seminar seminar)
        {
            seminar.StartDate = seminar.StartDate.LocalDateTime;
            seminar.EndDate = seminar.EndDate.LocalDateTime;
            _genericsRepo.Add(seminar);
        }

        public async Task<List<Seminar>> GetSeminarsForUser(int userId)
        {
            return await _seminarsRepo.GetSeminarsForUser(userId);
        }

        public List<SeminarForUpdateDto> MapSeminarsForAdmin(List<Seminar> seminars)
        {
            var seminarsForReturn = _mapper.Map<List<SeminarForUpdateDto>>(seminars);

            return seminarsForReturn;
        }

        public async Task<bool> SeminarExists(Seminar addedSeminar, int id)
        {
            var seminars = await GetSeminarsForUser(id);

            foreach (var seminar in seminars)
            {
                if (addedSeminar.Name == seminar.Name && addedSeminar.GroupId == seminar.GroupId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<SeminarForUpdateDto> UpdateSeminar(Seminar seminar)
        {
            var currentCourse = await _seminarsRepo.GetSeminarById(seminar.Id);

            currentCourse.Name = seminar.Name;
            currentCourse.StartDate = seminar.StartDate.LocalDateTime;
            currentCourse.EndDate = seminar.EndDate.LocalDateTime;
            currentCourse.TeacherId = seminar.TeacherId;
            currentCourse.GroupId = seminar.GroupId;
            currentCourse.ClassId = seminar.ClassId;
            currentCourse.CourseId = seminar.CourseId;

            var mappedCourse = _mapper.Map<SeminarForUpdateDto>(currentCourse);

            return mappedCourse;
        }
    }
}
