using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Models;

namespace ForumAPI.Profiles
{
    // Implemented in the same way as UserProfile, go to that file for more information.
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            // Source --> Target
            CreateMap<Post, GetPostDto>();
            CreateMap<Post, UpdatePostDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}