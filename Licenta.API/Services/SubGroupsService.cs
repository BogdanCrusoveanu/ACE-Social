using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class SubGroupsService : ISubGroupsService
    {
        private readonly ISubGroupsRepository _subGroupsRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public SubGroupsService(ISubGroupsRepository subGroupsRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _subGroupsRepo = subGroupsRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void AddSubGroup(SubGroup subGroup)
        {
            _genericsRepo.Add(subGroup);
        }

        public async Task<List<SubGroup>> GetSubGroups()
        {
            return await _subGroupsRepo.GetSubGroups();
        }

        public List<SubGroupForReturnDto> MapSubGroups(List<SubGroup> subGroups)
        {
            return _mapper.Map<List<SubGroupForReturnDto>>(subGroups);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<bool> SubGroupExists(SubGroup subGroup)
        {
            var subGroups = await GetSubGroups();

            foreach (var existingSubGroup in subGroups)
            {
                if (subGroup.Name == existingSubGroup.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<SubGroup> GetGroupById(int id)
        {
            return await _subGroupsRepo.GetSubGroupById(id);
        }

        public async Task<SpecializationForReturnDto> UpdateSubGroup(SubGroup subGroup)
        {
            var updatedSubGroup = await GetGroupById(subGroup.Id);

            updatedSubGroup.Name = subGroup.Name;

            updatedSubGroup.GroupId = subGroup.GroupId;

            var mappedDubGroup = _mapper.Map<SpecializationForReturnDto>(updatedSubGroup);

            return mappedDubGroup;
        }
    }
}
