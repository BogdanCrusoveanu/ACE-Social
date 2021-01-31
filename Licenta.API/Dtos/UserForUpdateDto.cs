using System;

namespace Licenta.API.Dtos
{
    public class UserForUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interests { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Year { get; set; }
    }
}
