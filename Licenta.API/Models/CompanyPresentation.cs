using System;

namespace Licenta.API.Models
{
    public class CompanyPresentation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }
    }
}
