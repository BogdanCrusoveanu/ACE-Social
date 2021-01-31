using Licenta.API.Models;
using Licenta.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly DataContext _context;

        public CommentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _context.Comments.Where(c => c.Id == commentId).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetComments(int postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).OrderByDescending(c => c.CreatedAt).ToListAsync();
        }
    }
}
