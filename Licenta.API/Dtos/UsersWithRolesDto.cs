using System.Collections.Generic;

namespace Licenta.API.Dtos
{
    public class UsersWithRolesDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
