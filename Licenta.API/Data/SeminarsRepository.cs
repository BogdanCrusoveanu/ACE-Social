using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class SeminarsRepository : ISeminarsRepository
    {
        private readonly DataContext _context;

        public SeminarsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Seminar>> GetAll()
        {
            return await _context.Seminars.ToListAsync();
        }

        public async Task<Seminar> GetSeminarById(int id)
        {
            return await _context.Seminars.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Seminar>> GetSeminarsForUser(int userId)
        {
            var role = await(from r in _context.Roles
                             join ur in _context.UserRoles on userId equals ur.UserId
                             select ur.Role).FirstOrDefaultAsync();

            if (role.Name == "Admin")
            {
                return await _context.Seminars.ToListAsync();
            }
            else
                if (role.Name == "Profesor")
            {
                var seminars = await _context.Seminars.Where(a => a.TeacherId == userId).ToListAsync();
                return seminars;
            }
            else
            {
                var groupId = await(from g in _context.Groups
                                       join us in _context.UserGroups on g.Id equals us.GroupId
                                       join u in _context.Users on us.UserId equals u.Id
                                       select g.Id).FirstOrDefaultAsync();

                return await _context.Seminars.Where(s => s.GroupId == groupId).ToListAsync();
            }
        }
    }
}
