using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class PostsRepository : IPostsRepository
    {
        private readonly DataContext _context;

        public PostsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsByUser(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<List<Post>> GetPostsUserCommented(int userId)
        {
            return await _context.Posts.Where(p => p.Comments.Any(c => c.UserId == userId)).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}
