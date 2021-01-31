using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class SpecializationsRepository : ISpecializationsRepository
    {
        private readonly DataContext _context;

        public SpecializationsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Specialization> GetSpecializationById(int id)
        {
            return await _context.Specializations.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public Specialization GetSpecializationByName(string name)
        {
            return _context.Specializations.Where(d => d.Name == name).FirstOrDefault();
        }

        public async Task<Specialization> GetSpecializationByUser(int userId)
        {
            return await (from s in _context.Specializations
                          join us in _context.UserSpecializations on userId equals us.UserId
                          select s).FirstOrDefaultAsync();
        }

        public async Task<List<Specialization>> GetSpecializations()
        {
            return await _context.Specializations.OrderBy(s => s.Name).ToListAsync();
        }
    }
}
