namespace Domain.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string companyName, string userEmail)
            : base($"Company with name [{companyName}] already contains a user with email [{userEmail}]!")
        { }
    }
}
