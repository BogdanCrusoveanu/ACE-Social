using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.Data;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;
        private readonly ISpecializationsRepository _specializationsRepo;
        private readonly IGroupsRepository _groupsRepo;
        private readonly ISubGroupsRepository _subGroupsRepo;

        public UsersService(IUsersRepository repo, IMapper mapper, IGenericsRepository genericsRepo,
            ISpecializationsRepository specializationsRepo, IGroupsRepository groupsRepo, ISubGroupsRepository subGroupsRepo)
        {
            _usersRepo = repo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
            _specializationsRepo = specializationsRepo;
            _groupsRepo = groupsRepo;
            _subGroupsRepo = subGroupsRepo;
        }

        public IEnumerable<UserForDetailedDto> AddCategories(IEnumerable<UserForDetailedDto> users)
        {
            foreach (var user in users)
            {
                var specialization = _specializationsRepo.GetSpecializationByUser(user.Id).Result;

                if (specialization != null)
                {
                    user.Specialization = specialization.Name;

                    var group = _groupsRepo.GetGroupByUser(user.Id).Result;
                    user.Group = group.Name;

                    var subGroup = _subGroupsRepo.GetSubGroupByUser(user.Id).Result;
                    user.SubGroup = subGroup.Name;
                }
                user.Photos = null;
            }

            return users;
        }

        public void AddLike(int userId, int recipientId, Like like)
        {
            like = new Like
            {
                LikerId = userId,
                LikeeId = recipientId
            };

            _genericsRepo.Add(like);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _usersRepo.GetLike(userId, recipientId);
        }

        public async Task<User> GetUser(int id)
        {
            return await _usersRepo.GetUser(id);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            return await _usersRepo.GetUsers(userParams);
        }

        public async Task<PagedList<User>> GetUsersFromGroup(int groupId, UserParams userParams)
        {
            return await _usersRepo.GetUsersFromGroup(groupId, userParams);
        }

        public async Task<PagedList<User>> GetUsersFromSpecialization(int specialiationId, UserParams userParams)
        {
            return await _usersRepo.GetUsersFromSpecialization(specialiationId, userParams);
        }

        public async Task<PagedList<User>> GetUsersFromSubGroup(int subGroupId, UserParams userParams)
        {
            return await _usersRepo.GetUsersFromSubGroup(subGroupId, userParams);
        }

        public UserForDetailedDto MapUserForReturn(User user)
        {
            var userForDetailed = _mapper.Map<UserForDetailedDto>(user);

            var specialization = _specializationsRepo.GetSpecializationByUser(user.Id).Result;
            if (specialization != null)
            {
                userForDetailed.Specialization = specialization.Name;

                var group = _groupsRepo.GetGroupByUser(user.Id).Result;
                userForDetailed.Group = group.Name;

                var subGroup = _subGroupsRepo.GetSubGroupByUser(user.Id).Result;
                userForDetailed.SubGroup = subGroup.Name;
            }

            return userForDetailed;
        }

        public IEnumerable<UserFromCategoryDto> MapUsersFromCategory(PagedList<User> users)
        {
            return _mapper.Map<IEnumerable<UserFromCategoryDto>>(users);
        }

        public IEnumerable<UserForDetailedDto> MapUsersToReturn(PagedList<User> users)
        {
            return _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<UserForUpdateDto> UpdateUser(UserForUpdateDto userForUpdate, int id)
        {
            var user = await _usersRepo.GetUser(id);

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.DateOfBirth = userForUpdate.DateOfBirth;
            user.Year = userForUpdate.Year;
            user.UserName = userForUpdate.FirstName + userForUpdate.LastName + userForUpdate.DateOfBirth.Day;

            var mappedUser = _mapper.Map<UserForUpdateDto>(user);

            return mappedUser;
        }
    }
}
