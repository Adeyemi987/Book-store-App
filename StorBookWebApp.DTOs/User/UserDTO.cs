using System.Collections.Generic;

namespace StorBookWebApp.DTOs.API
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoURL { get; set; }
        public string token { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
