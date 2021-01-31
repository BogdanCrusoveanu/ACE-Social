using System;

namespace Licenta.API.Dtos
{
    public class PostToAddDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }

        public PostToAddDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
