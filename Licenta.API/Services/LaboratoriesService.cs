using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class LaboratoriesService : ILaboratoriesService
    {
        private readonly ILaboratoriesRepository _laboratoriesRepo;
        private readonly IGenericsRepository _genericsRepo;
        private readonly IMapper _mapper;

        public LaboratoriesService(ILaboratoriesRepository laboratoriesRepo, IGenericsRepository genericsRepo, IMapper mapper)
        {
            _laboratoriesRepo = laboratoriesRepo;
            _genericsRepo = genericsRepo;
            _mapper = mapper;
        }

        public void AddLaboratory(Laboratory laboratory)
        {
            _genericsRepo.Add(laboratory);
        }

        public Task<List<Laboratory>> GetLaboratoriesForUser(int userId)
        {
            return _laboratoriesRepo.GetLaboratoriesForUser(userId);
        }

        public async Task<bool> LaboratoryExists(Laboratory addedLaboratory, int id)
        {
            var laboratories = await GetLaboratoriesForUser(id);

            foreach (var laboratory in laboratories)
            {
                if (addedLaboratory.Name == laboratory.Name && addedLaboratory.SubGroupId == laboratory.SubGroupId)
                {
                    return true;
                }
            }

            return false;
        }

        public List<LaboratoryForUpdateDto> MapLaboratoriesForAdmin(List<Laboratory> laboratories)
        {
            var laboratoriesForReturn = _mapper.Map<List<LaboratoryForUpdateDto>>(laboratories);

            return laboratoriesForReturn;
        }

        public async Task<LaboratoryForUpdateDto> UpdateLaboratory(Laboratory laboratory)
        {
            var currentLaboratory = await _laboratoriesRepo.GetLaboratoryById(laboratory.Id);

            currentLaboratory.Name = laboratory.Name;
            currentLaboratory.StartDate = laboratory.StartDate;
            currentLaboratory.EndDate = laboratory.EndDate;
            currentLaboratory.TeacherId = laboratory.TeacherId;
            currentLaboratory.SubGroupId = laboratory.SubGroupId;
            currentLaboratory.ClassId = laboratory.ClassId;
            currentLaboratory.CourseId = laboratory.CourseId;

            var mappedCourse = _mapper.Map<LaboratoryForUpdateDto>(currentLaboratory);

            return mappedCourse;
        }
    }
}
