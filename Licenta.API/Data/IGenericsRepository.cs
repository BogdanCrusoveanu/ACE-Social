using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IGenericsRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}
