using System;

namespace Licenta.API.Dtos
{
    public class PresentationForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }
    }
}
