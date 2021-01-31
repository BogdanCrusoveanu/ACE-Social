using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ICompanyPresentationsRepository
    {
        Task<List<CompanyPresentation>> GetAll();
        Task<CompanyPresentation> GetPresentationById(int id);
    }
}
