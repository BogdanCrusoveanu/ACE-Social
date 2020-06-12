using Licenta.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Licenta.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Year { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Like> Likers { get; set; }
        public virtual ICollection<Like> Likees { get; set; }
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserSpecialization> UserSpecialization { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserSubGroup> UserSubGroups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }

}
