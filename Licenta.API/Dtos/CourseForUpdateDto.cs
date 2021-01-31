using System;

namespace Licenta.API.Dtos
{
    public class CourseForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SemesterId { get; set; }
    }
}
