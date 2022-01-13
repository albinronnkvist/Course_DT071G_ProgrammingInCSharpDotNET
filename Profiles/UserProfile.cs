using AutoMapper;
using ForumAPI.Dtos.User;
using ForumAPI.Models;

namespace ForumAPI.Profiles
{
    public class UserProfile : Profile
    {
        // Define AutoMapper profiles
        public UserProfile() {
            // Define each map from source to target
            CreateMap<User, GetFullUserDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}