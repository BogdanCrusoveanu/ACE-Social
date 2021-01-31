using System;

namespace Licenta.API.Dtos
{
    public class CommentForPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}
