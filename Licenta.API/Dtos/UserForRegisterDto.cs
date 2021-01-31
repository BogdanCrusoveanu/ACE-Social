using System;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Dtos
{
    public class UserForRegisterDto
    {
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage = "You must specify passwords between 4 and 8 caracters")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
            Username = FirstName + LastName + DateOfBirth.Day;
        }
    }
}
