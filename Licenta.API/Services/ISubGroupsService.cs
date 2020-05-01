using Licenta.API.Dtos;
using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ISubGroupsService
    {
        Task<List<SubGroup>> GetSubGroups();
        List<SubGroupForReturnDto> MapSubGroups(List<SubGroup> subGroups);
        Task<bool> SubGroupExists(SubGroup subGroup);
        void AddSubGroup(SubGroup subGroup);
        Task<bool> SaveChangesInContext();
        Task<SpecializationForReturnDto> UpdateSubGroup(SubGroup subGroup);
    }
}
