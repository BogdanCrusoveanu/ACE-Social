using Licenta.Models;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IPhotosRepository
    {
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
    }
}
