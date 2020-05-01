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
    public class GroupsService : IGroupsService
    {
        private readonly IGroupsRepository _groupsRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public GroupsService(IGroupsRepository groupsRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _groupsRepo = groupsRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void AddGroup(Group group)
        {
            _genericsRepo.Add(group);
        }

        public async Task<List<Group>> GetGroups()
        {
            return await _groupsRepo.GetGroups();
        }

        public async Task<bool> GroupExists(Group group)
        {
            var groups = await GetGroups();

            foreach (var existingGroup in groups)
            {
                if (group.Name == existingGroup.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public List<GroupForReturnDto> MapGroups(List<Group> groups)
        {
            return _mapper.Map<List<GroupForReturnDto>>(groups);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _groupsRepo.GetGroupById(id);
        }

        public async Task<SpecializationForReturnDto> UpdateGroup(Group group)
        {
            var updatedGroup = await GetGroupById(group.Id);

            updatedGroup.Name = group.Name;

            updatedGroup.SpecializationId = group.SpecializationId;

            var mappedGroup = _mapper.Map<SpecializationForReturnDto>(updatedGroup);

            return mappedGroup;
        }
    }
}
