using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Dtos
{
    public class GroupForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
