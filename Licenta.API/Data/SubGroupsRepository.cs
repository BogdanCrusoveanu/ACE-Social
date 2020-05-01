using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class SubGroupsRepository : ISubGroupsRepository
    {
        private readonly DataContext _context;

        public SubGroupsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<SubGroup> GetSubGroupById(int id)
        {
            return await _context.SubGroups.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public SubGroup GetSubGroupByName(string name)
        {
            return _context.SubGroups.Where(d => d.Name == name).FirstOrDefault();
        }

        public async Task<SubGroup> GetSubGroupByUser(int userId)
        {
            return await(from sg in _context.SubGroups
                         join us in _context.UserSpecializations on userId equals us.UserId
                         select sg).FirstOrDefaultAsync();
        }

        public async Task<List<SubGroup>> GetSubGroups()
        {
            return await _context.SubGroups.OrderBy(sg => sg.Name).ToListAsync();
        }
    }
}
