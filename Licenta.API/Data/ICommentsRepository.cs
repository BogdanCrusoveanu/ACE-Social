using Licenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface ICommentsRepository
    {
        public Task<List<Comment>> GetComments(int postId);
        public Task<Comment> GetCommentById(int commentId);
    }
}
