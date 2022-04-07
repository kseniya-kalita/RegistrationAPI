using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggragate
{
    public class User : Entity
    {
        private string _email;
        private string _password;

        public User(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            _email = email;
            _password = password;
        }

        public string GetUserEmail()
        {
            return _email;
        }

        public string GetUserPassword()
        {
            // ToDo
            // Add hashing to not show the original password
            return _password;
        }
    }
}
