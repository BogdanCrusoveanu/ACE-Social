using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class CompanyPresentationsRepository : ICompanyPresentationsRepository
    {
        private readonly DataContext _context;

        public CompanyPresentationsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<CompanyPresentation>> GetAll()
        {
            return await _context.CompaniesPresentations.ToListAsync();
        }

        public async Task<CompanyPresentation> GetPresentationById(int id)
        {
            return await _context.CompaniesPresentations.Where(cp => cp.Id == id).FirstOrDefaultAsync();
        }
    }
}
