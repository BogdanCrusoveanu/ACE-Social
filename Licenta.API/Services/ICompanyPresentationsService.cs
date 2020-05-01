using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ICompanyPresentationsService
    {
        Task<List<CompanyPresentation>> GetCompanyPresentationsForUser();
        Task<bool> CompanyPresentationExists(CompanyPresentation companyPresentation);
        void AddCompanyPresentation(CompanyPresentation companyPresentation);
        List<PresentationForUpdateDto> MapPresentationsForAdmin(List<CompanyPresentation> presentations);
        Task<PresentationForUpdateDto> UpdatePresentation(CompanyPresentation presentation);
    }
}
