using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly DataContext _context;

        public CoursesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetCoursesForUser(int userId)
        {
            var role = await (from r in _context.Roles
                              join ur in _context.UserRoles on r.Id equals ur.RoleId
                              join u in _context.Roles on ur.UserId equals userId
                              select r).FirstOrDefaultAsync();

            if (role.Name == "Admin")
            {
                return await _context.Courses.OrderBy(c => c.Specialization.Name).ToListAsync();
            }
            else
                if (role.Name == "Profesor")
            {
                var activities = await _context.Courses.Where(a => a.TeacherId == userId).OrderBy(c => c.Specialization.Name).ToListAsync();
                return activities;
            }
            else
            {
                var specializationId = await (from s in _context.Specializations
                                        join us in _context.UserSpecializations on s.Id equals us.SpecializationId
                                        join u in _context.Users on us.UserId equals u.Id
                                        select s.Id).FirstOrDefaultAsync();

                return await _context.Courses.Where(c => c.SpecializationId == specializationId).OrderBy(c => c.Specialization.Name).ToListAsync();
            }

        }

        public async Task<List<Course>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
