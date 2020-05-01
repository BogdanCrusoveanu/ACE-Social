using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ISeminarsService
    {
        Task<List<Seminar>> GetSeminarsForUser(int userId);
        Task<bool> SeminarExists(Seminar seminar, int id);
        void AddSeminar(Seminar seminar);
        List<SeminarForUpdateDto> MapSeminarsForAdmin(List<Seminar> seminars);
        Task<SeminarForUpdateDto> UpdateSeminar(Seminar seminar);
    }
}
