using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class LaboratoriesRepository : ILaboratoriesRepository
    {
        private readonly DataContext _context;

        public LaboratoriesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Laboratory>> GetAll()
        {
            return await _context.Laboratories.ToListAsync();
        }

        public async Task<List<Laboratory>> GetLaboratoriesForUser(int userId)
        {
            var role = await(from r in _context.Roles
                             join ur in _context.UserRoles on r.Id equals ur.RoleId
                             join u in _context.Roles on ur.UserId equals userId
                             select r).FirstOrDefaultAsync();

            if (role.Name == "Admin")
            {
                return await _context.Laboratories.OrderBy(s => s.SubGroup.Name).ToListAsync();
            }
            else
                if (role.Name == "Profesor")
            {
                var laboratories = await _context.Laboratories.Where(a => a.TeacherId == userId).OrderBy(s => s.SubGroup.Name).ToListAsync();
                return laboratories;
            }
            else
            {
                var subGroupId = await(from sg in _context.SubGroups
                                      join usg in _context.UserSubGroups on sg.Id equals usg.SubGroupId
                                      join u in _context.Users on usg.UserId equals u.Id
                                      select sg.Id).FirstOrDefaultAsync();

                return await _context.Laboratories.Where(l => l.SubGroupId == subGroupId).OrderBy(s => s.SubGroup.Name).ToListAsync();
            }
        }

        public async Task<Laboratory> GetLaboratoryById(int laboratoryId)
        {
            return await _context.Laboratories.Where(l => l.Id == laboratoryId).FirstOrDefaultAsync();
        }
    }
}
