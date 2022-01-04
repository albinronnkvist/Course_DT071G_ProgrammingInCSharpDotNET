using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Models;

namespace ForumAPI.Profiles
{
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