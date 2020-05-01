using Licenta.Data;
using Licenta.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class PhotosRepository: IPhotosRepository
    {
        private readonly DataContext _context;

        public PhotosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }
    }
}
