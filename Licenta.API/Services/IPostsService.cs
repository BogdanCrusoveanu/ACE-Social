using Licenta.API.Dtos;
using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IPostsService
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetUserPosts(int userId);
        Task<List<Post>> GetPostsUserCommented(int userId);
        List<PostForDetailedDto> MapPosts(List<Post> posts);
        void AddPost(Post post);
        Task<Post> UpdatePost(Post post);
    }
}
