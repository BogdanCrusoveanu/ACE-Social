using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class PostsService: IPostsService
    {
        private readonly IPostsRepository _postsRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public PostsService(IPostsRepository postsRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _postsRepo = postsRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void AddPost(Post post)
        {
            post.CreatedAt = DateTime.Now;
            _genericsRepo.Add(post);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _postsRepo.GetAllPosts();
        }

        public async Task<List<Post>> GetPostsUserCommented(int userId)
        {
            return await _postsRepo.GetPostsUserCommented(userId);
        }

        public async Task<List<Post>> GetTeachersPosts()
        {
            return await _postsRepo.GetTeachersPosts();
        }

        public async Task<List<Post>> GetStudentsPosts()
        {
            return await _postsRepo.GetStudentsPosts();
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            return await _postsRepo.GetPostsByUser(userId);
        }

        public List<PostForDetailedDto> MapPosts(List<Post> posts)
        {
            var postsForReturn = _mapper.Map<List<PostForDetailedDto>>(posts);

            return postsForReturn;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            var postToUpdate = await _postsRepo.GetPostById(post.Id);
            postToUpdate.Content = post.Content;
            return postToUpdate;
        }
    }
}
