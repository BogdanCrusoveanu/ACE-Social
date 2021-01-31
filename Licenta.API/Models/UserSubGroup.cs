using Licenta.Models;

namespace Licenta.API.Models
{
    public class UserSubGroup
    {
        public virtual SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
