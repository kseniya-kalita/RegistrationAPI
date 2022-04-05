namespace Registration.Abstractions.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
