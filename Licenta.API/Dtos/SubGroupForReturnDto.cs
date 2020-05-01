using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Dtos
{
    public class SubGroupForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
