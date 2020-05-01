using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.Dtos;
using Licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly ISpecializationsRepository _specializationsRepo;
        private readonly IGenericsRepository _genericsRepo;
        private readonly IGroupsRepository _groupsRepo;
        private readonly ISubGroupsRepository _subGroupsRepo;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration config,
            SignInManager<User> signInManager, ISpecializationsRepository specializationsRepo,
            IGenericsRepository genericsRepo, IGroupsRepository groupsRepo, ISubGroupsRepository subGroupsRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _specializationsRepo = specializationsRepo;
            _genericsRepo = genericsRepo;
            _groupsRepo = groupsRepo;
            _subGroupsRepo = subGroupsRepo;
        }

        public User FindUser(string username)
        {
            return _userManager.FindByNameAsync(username).Result;
        }

        public User MapRegisterInformations(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.FirstName + userForRegisterDto.LastName + userForRegisterDto.DateOfBirth.Day;

            return _mapper.Map<User>(userForRegisterDto);
        }

        public UserForDetailedDto MapRegisteredUser(User user)
        {
            return _mapper.Map<UserForDetailedDto>(user);
        }

        public UserForListDto MapUserForLogin(User user)
        {
            return _mapper.Map<UserForListDto>(user);
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (user.Year == 0)
                await _userManager.AddToRoleAsync(user, "Profesor");
            else
                await _userManager.AddToRoleAsync(user, "Student");

            return result;
        }

        public async Task<SignInResult> SignInUser(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void AddUserToDivisions(UserForRegisterDto userForRegister, User user, UserForDetailedDto userForDetailed)
        {
            if (userForRegister.Year != 0)
            {

                var specialization = _specializationsRepo.GetSpecializationByName(userForRegister.Specialization);

                var group = _groupsRepo.GetGroupByName(userForRegister.Group);

                var subGroup = _subGroupsRepo.GetSubGroupByName(userForRegister.SubGroup);

                UserSpecialization userSpecialization = new UserSpecialization
                {
                    UserId = user.Id,
                    SpecializationId = specialization.Id
                };

                _genericsRepo.Add(userSpecialization);

                UserGroup userGroup = new UserGroup
                {
                    UserId = user.Id,
                    GroupId = group.Id
                };

                _genericsRepo.Add(userGroup);

                UserSubGroup userSubGroup = new UserSubGroup
                {
                    UserId = user.Id,
                    SubGroupId = subGroup.Id
                };

                _genericsRepo.Add(userSubGroup);

                userForDetailed.Specialization = specialization.Name;
                userForDetailed.Group = group.Name;
                userForDetailed.SubGroup = subGroup.Name;

                _genericsRepo.SaveAll();
            }
        }
    }
}
