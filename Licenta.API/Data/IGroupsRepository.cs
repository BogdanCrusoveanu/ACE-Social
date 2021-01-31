using Licenta.API.Models;
using System.Collections.Generic;
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
