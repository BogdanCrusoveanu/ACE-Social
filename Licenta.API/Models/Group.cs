using System.Collections.Generic;

namespace Licenta.API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<Seminar> Seminars { get; set; }
    }
}
