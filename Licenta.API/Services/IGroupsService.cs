using Licenta.API.Dtos;
using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IGroupsService
    {
        Task<List<Group>> GetGroups();
        List<GroupForReturnDto> MapGroups(List<Group> Groups);
        Task<bool> GroupExists(Group group);
        void AddGroup(Group group);
        Task<bool> SaveChangesInContext();
        Task<SpecializationForReturnDto> UpdateGroup(Group group);
    }
}
