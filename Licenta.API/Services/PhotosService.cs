using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Licenta.API.Data;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;


namespace Licenta.API.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IPhotosRepository _photosRepo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IGenericsRepository _genericsRepo;

        public PhotosService(IPhotosRepository photosRepo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig, IGenericsRepository genericsRepo)
        {
            _photosRepo = photosRepo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _genericsRepo = genericsRepo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<Photo> SetMainPhoto(int id)
        {
            return await _photosRepo.GetMainPhotoForUser(id);
        }

        public async Task<bool> SaveChangesInContext()
        {
            return await _genericsRepo.SaveAll();
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _photosRepo.GetPhoto(id);
        }

        public PhotoForReturnDto MapPhotoForReturn(Photo photo)
        {
            return _mapper.Map<PhotoForReturnDto>(photo);
        }

        public Photo MapUploadedPhoto(PhotoForCreationDto photoForCreation, User user)
        {
            var photo = _mapper.Map<Photo>(photoForCreation);

            if (!user.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            user.Photos.Add(photo);

            return photo;
        }

        public void UploadPhotoToCloudinary(IFormFile file, PhotoForCreationDto photoForCreation)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = _cloudinary.Upload(uploadParams);
            }

            photoForCreation.Url = uploadResult.Uri.ToString();
            photoForCreation.PublicId = uploadResult.PublicId;
        }

        public void DeletePhoto(Photo photo)
        {
            if (photo.PublicID != null)
            {
                var deleteParams = new DeletionParams(photo.PublicID);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _genericsRepo.Delete(photo);
                }

                if (photo.PublicID == null)
                {
                    _genericsRepo.Delete(photo);
                }
            }
        }
    }
}
