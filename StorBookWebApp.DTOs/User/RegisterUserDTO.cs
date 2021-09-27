namespace StorBookWebApp.DTOs.API
{
    public class RegisterUserDto : LoginUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
