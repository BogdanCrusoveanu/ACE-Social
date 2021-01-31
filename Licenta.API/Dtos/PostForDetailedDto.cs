using System;
using System.Collections.Generic;

namespace Licenta.API.Dtos
{
    public class PostForDetailedDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public  string UserName { get; set; }
        public string MainPhotoUrl { get; set; }
        public  ICollection<CommentForPostDto> Comments { get; set; }
    }
}
