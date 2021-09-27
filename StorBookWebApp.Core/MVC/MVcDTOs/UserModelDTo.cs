using System.Collections.Generic;

namespace StorBookWebApp.Core.MVC
{
    public class UserModelDTo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePics { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
