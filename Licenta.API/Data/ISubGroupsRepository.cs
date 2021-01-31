using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ISubGroupsRepository
    {
        SubGroup GetSubGroupByName(string name);
        Task<SubGroup> GetSubGroupByUser(int userId);
        Task<List<SubGroup>> GetSubGroups();
        Task<SubGroup> GetSubGroupById(int id);
    }
}
