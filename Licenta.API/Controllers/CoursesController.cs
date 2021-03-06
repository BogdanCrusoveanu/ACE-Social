﻿using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.API.Services;
using Licenta.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        private readonly IGenericsRepository _genericsRepo;

        public CoursesController(ICoursesService coursesService, IGenericsRepository genericsRepo)
        {
            _coursesService = coursesService;
            _genericsRepo = genericsRepo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCourse(Course course)
        {
            int adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _coursesService.CourseExists(course, adminId))
            {
                return BadRequest("The course you entered already exists!");
            }

            _coursesService.AddCourse(course);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCoursesForAdmin(int id)
        {
            var courses = await _coursesService.GetCoursesForUser(id);

            var coursesForAdmin = _coursesService.MapCoursesForAdmin(courses);

            return Ok(coursesForAdmin);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCourse(Course course)
        {
            await _coursesService.UpdateCourse(course);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("You entered the same course name or something went wrong!");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCourse(Course course)
        {
            _genericsRepo.Delete(course);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Delete Failed!");
        }
    }
}