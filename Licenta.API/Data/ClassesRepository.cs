using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly DataContext _context;

        public ClassesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Class> GetClassById(int id)
        {
            return await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Class>> GetClasses()
        {
            return await _context.Classes.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
