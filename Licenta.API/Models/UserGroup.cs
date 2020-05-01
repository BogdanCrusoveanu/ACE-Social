using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Models
{
    public class UserGroup
    {
        public virtual Group Group { get; set; }
        public int GroupId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
