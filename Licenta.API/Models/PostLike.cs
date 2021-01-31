using Licenta.Models;

namespace Licenta.API.Models
{
    public class PostLike
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
