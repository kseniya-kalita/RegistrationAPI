namespace RegistrationAPI.Boundary.Responses
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
