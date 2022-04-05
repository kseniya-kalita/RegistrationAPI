using Microsoft.EntityFrameworkCore;
using Registration.Abstractions.Interfaces;
using Registration.Abstractions.Models;

namespace Registration.DAL.Repositories
{
    /// <summary>
    /// Repository to manage user entity
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to create a user in the database
        /// </summary>
        /// <param name="user">User to be created</param>
        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get all users including their companies
        /// </summary>
        public async Task<List<User>> GetAllWithCompaniesAsync()
        {
            return await _context.Users
                .Include(u => u.Company)
                .ToListAsync();
        }
    }
}
