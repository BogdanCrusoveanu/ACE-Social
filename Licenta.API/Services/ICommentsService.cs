using Licenta.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface ICommentsService
    {
        void AddComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
    }
}
