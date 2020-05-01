using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
