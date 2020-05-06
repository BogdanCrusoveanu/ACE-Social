using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Dtos
{
    public class ActivityForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Duration { get; set; }
        public string Teacher { get; set; }
        public string CategoryName { get; set; }
        public string ClassName { get; set; }
        public int SemesterId { get; set; }
    }
}
