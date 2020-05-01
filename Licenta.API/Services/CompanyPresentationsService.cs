using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class CompanyPresentationsService : ICompanyPresentationsService
    {
        private readonly ICompanyPresentationsRepository _companyPresentationsRepo;
        private readonly IGenericsRepository _genericsRepo;
        private readonly IMapper _mapper;

        public CompanyPresentationsService(ICompanyPresentationsRepository companyPresentationsRepo,
            IGenericsRepository genericsRepo, IMapper mapper)
        {
            _companyPresentationsRepo = companyPresentationsRepo;
            _genericsRepo = genericsRepo;
            _mapper = mapper;
        }

        public void AddCompanyPresentation(CompanyPresentation companyPresentation)
        {
            _genericsRepo.Add(companyPresentation);
        }

        public async Task<bool> CompanyPresentationExists(CompanyPresentation companyPresentation)
        {
            var presentations = await GetCompanyPresentationsForUser();

            foreach (var presentation in presentations)
            {
                if (companyPresentation.StartDate == presentation.StartDate && companyPresentation.ClassId == presentation.ClassId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<CompanyPresentation>> GetCompanyPresentationsForUser()
        {
            return await _companyPresentationsRepo.GetAll();
        }

        public List<PresentationForUpdateDto> MapPresentationsForAdmin(List<CompanyPresentation> presentations)
        {
            var presentationsToReturn = _mapper.Map<List<PresentationForUpdateDto>>(presentations);

            return presentationsToReturn;
        }

        public async Task<PresentationForUpdateDto> UpdatePresentation(CompanyPresentation presentation)
        {
            var currentPresentation = await _companyPresentationsRepo.GetPresentationById(presentation.Id);

            currentPresentation.Name = presentation.Name;
            currentPresentation.StartDate = presentation.StartDate;
            currentPresentation.EndDate = presentation.EndDate;
            currentPresentation.ClassId = presentation.ClassId;

            var mappedCourse = _mapper.Map<PresentationForUpdateDto>(currentPresentation);

            return mappedCourse;
        }
    }
}
