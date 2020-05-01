using Licenta.API.Services;
using Licenta.Dtos;
using Licenta.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _photosService;
        private readonly IUserService _usersService;

        public PhotosController(IPhotosService photosService, IUserService usersService)
        {
            _photosService = photosService;
            _usersService = usersService;
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _photosService.GetPhoto(id);

            var photo = _photosService.MapPhotoForReturn(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId,
            [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _usersService.GetUser(userId);

            _photosService.UploadPhotoToCloudinary(photoForCreationDto.File, photoForCreationDto);

            var photo = _photosService.MapUploadedPhoto(photoForCreationDto, userFromRepo);

            if (await _usersService.SaveChangesInContext())
            {
                var photoToReturn = _photosService.MapPhotoForReturn(photo);
                return CreatedAtRoute("GetPhoto", new { userId, id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await _usersService.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _photosService.GetPhoto(id);

            if (photoFromRepo.IsMain)
            {
                return BadRequest("This is already the main photo!");
            }

            var currentPhoto = await _photosService.SetMainPhoto(userId);

            currentPhoto.IsMain = false;

            photoFromRepo.IsMain = true;

            if (await _photosService.SaveChangesInContext())
            {
                return NoContent();
            }

            return BadRequest("Could not set photo to main!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _usersService.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _photosService.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("You can't delete your main photo!");

            _photosService.DeletePhoto(photoFromRepo);

            if (await _photosService.SaveChangesInContext())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the photo!");
        }
    }
}