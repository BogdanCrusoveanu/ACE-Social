using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Models
{
    public class SubGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<UserSubGroup> UserSubGroups { get; set; }
        public virtual ICollection<Laboratory> Laboratories { get; set; }
    }
}
