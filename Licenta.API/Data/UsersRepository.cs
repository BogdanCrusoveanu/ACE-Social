using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.API.Data;
using Licenta.Helpers;
using Licenta.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(
                u => u.LikerId == userId && u.LikeeId == recipientId);
        }

        public async Task<List<User>> GetTeachers()
        {
            return await _context.Users.OrderBy(u => u.FirstName).Where(u => u.Year == 0).Where(u => u.UserName != "Admin").ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users,
                userParams.PageNumber, userParams.PageSize);
        }

        //public async Task<PagedList<User>> GetUsersWithRoles(UserParams userParams)
        //{
        //    var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

        //    users = users.Where(u => u.Id != userParams.UserId);

        //    return await PagedList<User>.CreateAsync(users,
        //        userParams.PageNumber, userParams.PageSize);
        //}

        public async Task<PagedList<User>> GetUsersFromGroup(int groupId, UserParams userParams)
        {
            var users = _context.Users.Where(x => _context.UserGroups.Any(y => y.GroupId == groupId && y.UserId == x.Id));

            return await PagedList<User>.CreateAsync(users,
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<User>> GetUsersFromSpecialization(int specializationId, UserParams userParams)
        {
            var users = _context.Users.Where(x => _context.UserSpecializations.Any(y => y.SpecializationId == specializationId && y.UserId == x.Id));

            return await PagedList<User>.CreateAsync(users,
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<User>> GetUsersFromSubGroup(int subGroupId, UserParams userParams)
        {
            var users = _context.Users.Where(x => _context.UserSubGroups.Any(y => y.SubGroupId == subGroupId && y.UserId == x.Id));

            return await PagedList<User>.CreateAsync(users,
                userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }
    }
}
