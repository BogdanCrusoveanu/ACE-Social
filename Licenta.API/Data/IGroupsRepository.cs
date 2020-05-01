using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IGroupsRepository
    {
        Group GetGroupByName(string name);
        Task<Group> GetGroupByUser(int userId);
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupById(int id);
    }
}
