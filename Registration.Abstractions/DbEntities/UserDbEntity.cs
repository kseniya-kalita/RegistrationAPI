namespace Registration.Abstractions.DbEntities
{
    public class UserDbEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyDbEntity Company { get; set; }
    }
}
