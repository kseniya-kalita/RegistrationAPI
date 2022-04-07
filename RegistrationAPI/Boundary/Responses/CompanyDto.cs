using Domain.AggregatesModel.UserAggragate;

namespace RegistrationAPI.Boundary.Responses
{
    public record CompanyDto
    {
        public IEnumerable<UserDto> Users { get; init; }
        public Guid Id { get; init; }
        public string Name { get; init; }

        public static CompanyDto FromCompany(Company company)
        {
            return company == null ? null : new CompanyDto()
            {
                Users = company.Users.Select(u => new UserDto
                {
                    Email = u.GetUserEmail(),
                    Password = u.GetUserPassword(),
                    Id = u.Id
                }),
                Id = company.Id,
                Name = company.Name,
            };
        }
    }
}
