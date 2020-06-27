using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IPostsRepository
    {
        public Task<List<Post>> GetAllPosts();
        public Task<List<Post>> GetPostsByUser(int userId);
        public Task<List<Post>> GetPostsUserCommented(int userId);
        public Task<List<Post>> GetTeachersPosts();
        public Task<List<Post>> GetStudentsPosts();
        public Task<Post> GetPostById(int postId);
    }
}
