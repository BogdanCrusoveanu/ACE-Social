using Licenta.Models;

namespace Licenta.API.Models
{
    public class UserSpecialization
    {
        public virtual Specialization Specializations { get; set; }
        public int SpecializationId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
