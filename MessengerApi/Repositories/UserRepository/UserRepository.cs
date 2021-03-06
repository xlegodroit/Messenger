using MessengerApi.Database;
using MessengerApi.Database.Models;
using MessengerApi.Dtos.Auth;
using MessengerApi.Dtos.Search;
using MessengerApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MessengerApi.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName, 
                Birthdate = (DateTime)registerDto.Birthdate,
                Email = registerDto.Email, 
                IsVerified = false,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.PlainPassword)
            };
            
            try {
                await _context.Users.AddAsync(user);
                return await _context.SaveChangesAsync() > 0 ? user : null;            }
            catch (DbUpdateException) {
                return null;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task SetUserRefreshTokenAsync(int idUser, RefreshToken refreshToken = null)
        {
            var user = new User()
            {
                Id = idUser,
                RefreshToken = refreshToken?.Value,
                RefreshTokenExpiration = refreshToken?.ExpirationDate
            };

            _context.Entry(user).Property(u => u.RefreshToken).IsModified = true;
            _context.Entry(user).Property(u => u.RefreshTokenExpiration).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task SetUserEmailVerificationTokenAsync(int idUser, string emailVerificaionToken)
        {
            var user = new User()
            {
                Id = idUser,
                EmailVerificationToken = emailVerificaionToken
            };

            _context.Entry(user).Property(u => u.EmailVerificationToken).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task MarkUserAsVerifiedAsync(int idUser)
        {
            var user = new User()
            {
                Id = idUser,
                IsVerified = true,
                EmailVerificationToken = null
            }; 

            _context.Entry(user).Property(u => u.IsVerified).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<SingleUserDto>> GetUsersForUserAsync(int idUser, int skipCount, int takeCount, string searchPhraze)
        {
            return await _context.Users
                .Where(u => u.Id != idUser)
                .Where(u => (u.FirstName + u.LastName).Contains(searchPhraze))
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Skip(skipCount)
                .Take(takeCount)
                .Select(u => new SingleUserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                }).ToListAsync();
        }
    }
}