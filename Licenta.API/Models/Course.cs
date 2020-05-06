using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public virtual User Teacher { get; set; }
        public int TeacherId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }
        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
        public virtual ICollection<Seminar> Seminars { get; set; }
        public virtual ICollection<Laboratory> Laboratories { get; set; }
    }
}
