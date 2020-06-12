using Licenta.API.Data;
using Licenta.API.Models;
using System;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class CommentsService: ICommentsService
    {
        private readonly ICommentsRepository _commentsRepo;
        private readonly IGenericsRepository _genericsRepo;

        public CommentsService(ICommentsRepository commentsRepo, IGenericsRepository genericsRepo)
        {
            _commentsRepo = commentsRepo;
            _genericsRepo = genericsRepo;
        }

        public void AddComment(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            _genericsRepo.Add(comment);
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            var commentToUpdate = await _commentsRepo.GetCommentById(comment.Id);
            commentToUpdate.Content = comment.Content;
            return commentToUpdate;
        }
    }
}
