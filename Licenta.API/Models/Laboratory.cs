using Licenta.Models;
using System;

namespace Licenta.API.Models
{
    public class Laboratory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public virtual User Teacher { get; set; }
        public int TeacherId { get; set; }
        public virtual SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
    }
}
