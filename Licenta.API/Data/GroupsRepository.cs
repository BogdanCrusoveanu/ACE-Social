using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly DataContext _context;

        public GroupsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public Group GetGroupByName(string name)
        {
            return _context.Groups.Where(d => d.Name == name).FirstOrDefault();
        }

        public async Task<Group> GetGroupByUser(int userId)
        {
            return await(from g in _context.Groups
                         join us in _context.UserSpecializations on userId equals us.UserId
                         select g).FirstOrDefaultAsync();
        }

        public async Task<List<Group>> GetGroups()
        {
            return await _context.Groups.OrderBy(g => g.Name).ToListAsync();
        }
    }
}
