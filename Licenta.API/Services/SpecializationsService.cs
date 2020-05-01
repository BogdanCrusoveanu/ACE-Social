using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class SpecializationsService : ISpecializationsService
    {
        private readonly ISpecializationsRepository _specializationsRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public SpecializationsService(ISpecializationsRepository specializationsRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _specializationsRepo = specializationsRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void AddSpecialization(Specialization specialization)
        {
            _genericsRepo.Add(specialization);
        }

        public async Task<List<Specialization>> GetSpecializations()
        {
            return await _specializationsRepo.GetSpecializations();
        }

        public List<SpecializationForReturnDto> MapSpecializations(List<Specialization> specializations)
        {
            return _mapper.Map<List<SpecializationForReturnDto>>(specializations);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<bool> SpecializationExists(Specialization specialization)
        {
            var specializations = await GetSpecializations();

            foreach (var existingSpecialization in specializations)
            {
                if(specialization.Name == existingSpecialization.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Specialization> GetSpecializationById(int id)
        {
            return await _specializationsRepo.GetSpecializationById(id);
        }

        public async Task<SpecializationForReturnDto> UpdateSpecialization(Specialization updatedSpecialization)
        {
            var specialization = await GetSpecializationById(updatedSpecialization.Id);

            specialization.Name = updatedSpecialization.Name;

            var mappedSpecialization = _mapper.Map<SpecializationForReturnDto>(specialization);

            return mappedSpecialization;
        }
    }
}
