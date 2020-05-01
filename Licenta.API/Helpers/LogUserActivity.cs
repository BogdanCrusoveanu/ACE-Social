using Licenta.API.Data;
using Licenta.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.
                FindFirst(ClaimTypes.NameIdentifier).Value);
            var genericsRepo = resultContext.HttpContext.RequestServices.GetService<IGenericsRepository>();
            var usersRepo = resultContext.HttpContext.RequestServices.GetService<IUsersRepository>();
            var coursesRepo = resultContext.HttpContext.RequestServices.GetService<ICoursesRepository>();
            var seminarsReop = resultContext.HttpContext.RequestServices.GetService<ISeminarsRepository>();
            var laboratoriesRepo = resultContext.HttpContext.RequestServices.GetService<ILaboratoriesRepository>();
            var semestersRepo = resultContext.HttpContext.RequestServices.GetService<ISemestersRepository>();
            var user = await usersRepo.GetUser(userId);
            user.LastActive = DateTime.Now;

            var courses = await coursesRepo.GetAll();
            var semester = await semestersRepo.GetSemesterByDate();
            foreach (var activity in courses)
            {
                if(activity.EndDate < DateTime.Now && activity.SemesterId == semester.Id && semester != null)
                {
                    activity.EndDate = activity.EndDate.AddDays(7);
                    activity.StartDate = activity.StartDate.AddDays(7);
                }
            }

            var seminars = await seminarsReop.GetAll();
            foreach (var seminar in seminars)
            {
                if (seminar.EndDate < DateTime.Now && seminar.SemesterId == semester.Id && semester != null)
                {
                    seminar.EndDate = seminar.EndDate.AddDays(7);
                    seminar.StartDate = seminar.StartDate.AddDays(7);
                }
            }

            var laboratories = await laboratoriesRepo.GetAll();
            foreach (var laboratory in laboratories)
            {
                if (laboratory.EndDate < DateTime.Now && laboratory.SemesterId == semester.Id && semester != null)
                {
                    laboratory.EndDate = laboratory.EndDate.AddDays(7);
                    laboratory.StartDate = laboratory.StartDate.AddDays(7);
                }
            }

            await genericsRepo.SaveAll();
        }
    }
}
