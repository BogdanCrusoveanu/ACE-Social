using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using Licenta.API.Services;
using Licenta.Data;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IAdminService _adminService;
        private readonly IGenericsRepository _genericsRepo;

        public AdminController(DataContext context, UserManager<User> userManager, IAdminService adminService, IGenericsRepository genericsRepo)
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
            _genericsRepo = genericsRepo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("pagedRoles")]
        public async Task<IActionResult> GetUsersWithRolesPaged([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userParams.UserId = currentUserId;

            var userList = _context.Users
                .OrderBy(x => x.UserName)
                .Select(user => new UsersWithRolesDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = (from userRole in user.UserRoles
                             join role in _context.Roles
                             on userRole.RoleId
                             equals role.Id
                             select role.Name).ToList()
                }).AsQueryable();

            var users = await PagedList<UsersWithRolesDto>.CreateAsync(userList,
                    userParams.PageNumber, userParams.PageSize);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles ??= new string[] {};
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpGet("GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var teachers = await _adminService.GetTeachers();

            var teachersToReturn = _adminService.MapTeachersForReturn(teachers);

            return Ok(teachersToReturn);
        }

        [HttpGet("GetSemesters")]
        public async Task<IActionResult> GetSemesters()
        {
            var semesters = await _adminService.GetSemesters();
            return Ok(semesters);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("updateSemesterDate")]
        public async Task<IActionResult> UpdateSemesterDate(Semester updatedSemester)
        {
            var semester = await _adminService.UpdateSemester(updatedSemester);

            if(await _adminService.SaveChangesInContext())
            {
                return Ok(semester);
            }
            return BadRequest("Update Failed or the same dates were sent");

        }

        [HttpGet("GetClasses")]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _adminService.GetClasses();

            return Ok(classes);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("UpdateClass")]
        public async Task<IActionResult> UpdateClass(Class classToUpdate)
        {
            var currentClasss =  await _adminService.GetClassById(classToUpdate.Id);

            if (currentClasss.Name != classToUpdate.Name)
            {
                currentClasss.Name = classToUpdate.Name;
            }

            if(await _adminService.SaveChangesInContext())
            {
                return Ok(currentClasss);
            }

            return BadRequest("You entered the same class name or something went wrong!");
        }

        [HttpGet("{id}", Name ="GetClass")]
        public async Task<IActionResult> GetClass(int id)
        {
            var selectedClass = await _adminService.GetClassById(id);

            return Ok(selectedClass);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass(ClassForCreateDto newClass)
        { 
            if( _adminService.ClassExists(newClass))
            {
                return BadRequest("The class you entered already exists!");
            }

            var addedClass = _adminService.AddClass(newClass);

            if(await _adminService.SaveChangesInContext())
            {
                return CreatedAtRoute("GetClass", new {  id = addedClass.Id }, addedClass);
            }
            return BadRequest("Something went wrong!");
        }

        [HttpDelete("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            _adminService.DeleteClass(id);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Error deleting the class!");
        }
    }
}