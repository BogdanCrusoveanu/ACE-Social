using System.Collections.Generic;

namespace Licenta.API.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserSpecialization> UserSpecializations { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
