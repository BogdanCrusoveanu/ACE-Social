using System;

namespace Licenta.API.Dtos
{
    public class UserFromCategoryDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Year { get; set; }
    }
}
