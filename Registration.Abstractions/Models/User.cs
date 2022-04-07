namespace Registration.Abstractions.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Company Company { get; set; }
        //ToDo K: do we need companyId?
        public User(Guid id, string email, string password, Guid companyId, string companyName)
        {
            Id = id;
            Email = email;
            Password = password;
            Company = new Company(companyId, companyName);
        }

        //ToDo K: set company???
    }
}
