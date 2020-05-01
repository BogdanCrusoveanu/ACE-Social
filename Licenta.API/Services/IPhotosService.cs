using Licenta.Dtos;
using Licenta.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IPhotosService
    {
        Task<Photo> GetPhoto(int id);
        PhotoForReturnDto MapPhotoForReturn(Photo photo);
        void UploadPhotoToCloudinary(IFormFile file, PhotoForCreationDto photoForCreation);
        Photo MapUploadedPhoto(PhotoForCreationDto photoForCreation, User user);
        Task<Photo> SetMainPhoto(int id);
        Task<bool> SaveChangesInContext();
        void DeletePhoto(Photo photo);
    }
}
