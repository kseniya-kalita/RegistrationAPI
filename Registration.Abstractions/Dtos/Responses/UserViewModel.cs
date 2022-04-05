namespace Registration.Abstractions.Dtos.Responses
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
