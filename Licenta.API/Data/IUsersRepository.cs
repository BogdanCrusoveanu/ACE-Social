using Licenta.Helpers;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Data
{
    public interface IUsersRepository
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<List<User>> GetTeachers();
        Task<PagedList<User>> GetUsersFromSpecialization(int specializationId, UserParams userParams);
        Task<PagedList<User>> GetUsersFromGroup(int groupId, UserParams userParams);
        Task<PagedList<User>> GetUsersFromSubGroup(int subGroupId, UserParams userParams);
    }
}
