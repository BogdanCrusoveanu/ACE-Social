using AutoMapper;
using Licenta.API.Dtos;
using Licenta.API.Models;
using Licenta.Dtos;
using Licenta.Models;
using System.Linq;

namespace Licenta.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt =>
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt =>
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.FullName, opt =>
                    opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.GroupName, opt =>
                    opt.MapFrom(src => src.UserGroups.Where(ug => ug.UserId == src.Id).FirstOrDefault().Group.Name))
                .ForMember(dest => dest.IsFriend, opt =>
                    opt.MapFrom(src => src.Likers.Where(ug => ug.LikerId == src.Id).FirstOrDefault().LikerId))
                .ForMember(dest => dest.Friends, opt =>
                    opt.MapFrom(src => src.Likers));

            CreateMap<Course, ActivityForReturnDto>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src =>
                    src.Teacher.FirstName + " " + src.Teacher.LastName))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                    src.EndDate.Hour - src.StartDate.Hour))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.Specialization.Name));

            CreateMap<Seminar, ActivityForReturnDto>()
               .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src =>
                   src.Teacher.FirstName + " " + src.Teacher.LastName))
               .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                   src.EndDate.Hour - src.StartDate.Hour))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.Group.Name));

            CreateMap<Laboratory, ActivityForReturnDto>()
               .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src =>
                   src.Teacher.FirstName + " " + src.Teacher.LastName))
               .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                   src.EndDate.Hour - src.StartDate.Hour))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.SubGroup.Name));

            CreateMap<CompanyPresentation, ActivityForReturnDto>()
               .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                   src.EndDate.Hour - src.StartDate.Hour));

            CreateMap<Course, CourseForUpdateDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                   src.Teacher.FirstName + " " + src.Teacher.LastName))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src =>
                   src.Specialization.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
                   src.Class.Name));

            CreateMap<Seminar, SeminarForUpdateDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                   src.Teacher.FirstName + " " + src.Teacher.LastName))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src =>
                   src.Group.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
                   src.Class.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src =>
                   src.Course.Name));

            CreateMap<Laboratory, LaboratoryForUpdateDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                    src.Teacher.FirstName + " " + src.Teacher.LastName))
                .ForMember(dest => dest.SubGroupName, opt => opt.MapFrom(src =>
                    src.SubGroup.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
                    src.Class.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src =>
                   src.Course.Name));

            CreateMap<CompanyPresentation, PresentationForUpdateDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
                    src.Class.Name));

            CreateMap<Group, GroupForReturnDto>()
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src =>
                   src.Specialization.Name));

            CreateMap<SubGroup, SubGroupForReturnDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src =>
                   src.Group.Name));

            CreateMap<Comment, CommentForPostDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                   src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src =>
                  src.User.Photos.Where(p => p.IsMain).FirstOrDefault().Url))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src =>
                  src.User.Id));

            CreateMap<Post, PostForDetailedDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                  src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src =>
                  src.User.Photos.Where(p => p.IsMain).FirstOrDefault().Url))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src =>
                  src.User.Id));

            //CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<Post, PostToAddDto>();
            CreateMap<Like, LikeDto>();
            CreateMap<User, TeacherDto>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<User, UserFromCategoryDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<ClassForCreateDto, Class>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Specialization, SpecializationForReturnDto>();
            CreateMap<Group, SpecializationForReturnDto>();
            CreateMap<SubGroup, SpecializationForReturnDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageForReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.SenderUserName, opt => opt
                    .MapFrom(u => u.Sender.FirstName + " " + u.Sender.LastName))
                .ForMember(m => m.RecipientUserName, opt => opt
                    .MapFrom(u => u.Recipient.FirstName + " " + u.Recipient.LastName));

        }
    }
}
