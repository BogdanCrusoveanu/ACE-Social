using Licenta.Dtos;
using System;
using System.Collections.Generic;

namespace Licenta.API.Dtos
{
    public class UserForRecommendationDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public int IsFriend { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public string Interests { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailedDto> Photos { get; set; }
        public ICollection<LikeDto> Friends { get; set; }
        public int Distance { get; set; }
    }
}
