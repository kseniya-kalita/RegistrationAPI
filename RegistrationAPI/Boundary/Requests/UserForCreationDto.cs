namespace RegistrationAPI.Boundary.Requests
{
    public class UserForCreationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public CompanyForCreationDto Company { get; set; }
    }
}
