using Licenta.API.Dtos;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IUserService
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<List<User>> GetRecommendedUsers(int userid);
        IEnumerable<UserForDetailedDto> MapUsersToReturn(PagedList<User> users, int currentUserId);
        IEnumerable<UserForRecommendationDto> MapUsersForRecommendation(List<User> users, int currentUserId);
        Task<User> GetUser(int id);
        Task<PagedList<User>> GetUsersFromSpecialization(int specialiationId ,UserParams userParams);
        Task<PagedList<User>> GetUsersFromGroup(int groupId, UserParams userParams);
        Task<PagedList<User>> GetUsersFromSubGroup(int subGroupId, UserParams userParams);
        UserForDetailedDto MapUserForReturn(User user);
        Task<bool> SaveChangesInContext();
        Task<Like> GetLike(int userId, int recipientId);
        void AddLike(int userId, int recipientId, Like like);
        IEnumerable<UserForDetailedDto> AddCategories(IEnumerable<UserForDetailedDto> users);
        IEnumerable<UserFromCategoryDto> MapUsersFromCategory(PagedList<User> users);
        Task<UserForUpdateDto> UpdateUser(UserForUpdateDto userForUpdate, int id);
    }
}
