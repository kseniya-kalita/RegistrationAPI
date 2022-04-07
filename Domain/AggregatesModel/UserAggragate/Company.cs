using Domain.Exceptions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggragate
{
    public class Company : Entity, IAggregateRoot
    {
        public Company(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            _users = new List<User>();
        }

        public string Name { get; private set; }

        private readonly List<User> _users;
        public IReadOnlyCollection<User> Users => _users;

        public void AddUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
          

            var existingUser = Users.FirstOrDefault(u => u.GetUserEmail() == email);
            if (existingUser == null)
            {
                _users.Add(new User(email, password));
            }
            else
            {
                throw new UserAlreadyExistsException(Name, email);
            }
        }
    }
}
