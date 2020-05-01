using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ISeminarsRepository
    {
        Task<List<Seminar>> GetSeminarsForUser(int userId);
        Task<List<Seminar>> GetAll();
        Task<Seminar> GetSeminarById(int id);
    }
}
