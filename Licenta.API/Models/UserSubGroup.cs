using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
