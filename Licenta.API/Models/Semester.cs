using System;

namespace Licenta.API.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
