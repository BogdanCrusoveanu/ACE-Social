using System;
using System.Collections.Generic;

namespace Licenta.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailedDto> Photos { get; set; }
    }
}
