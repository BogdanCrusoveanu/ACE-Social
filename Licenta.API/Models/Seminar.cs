using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Models
{
    public class Seminar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public virtual User Teacher { get; set; }
        public int TeacherId { get; set; }
        public virtual Group Group { get; set; }
        public int GroupId { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
    }
}
