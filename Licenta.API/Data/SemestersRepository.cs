using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class SemestersRepository : ISemestersRepository
    {
        private readonly DataContext _context;

        public SemestersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Semester> GetSemesterByDate()
        {
            return await _context.Semesters.FirstOrDefaultAsync(s => s.EndDate > DateTime.Now);
        }

        public async Task<Semester> GetSemesterById(int id)
        {
            return await _context.Semesters.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Semester>> GetSemesters()
        {
            return await _context.Semesters.ToListAsync();
        }
    }
}
