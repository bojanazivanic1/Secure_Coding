using Microsoft.EntityFrameworkCore;
using SecureCode.DTO;
using SecureCode.Interfaces.IProviders;
using SecureCode.Models;
using System;
using System.Data;
using static QRCoder.PayloadGenerator.ShadowSocksConfig;

namespace SecureCode.Infrastructure.Providers
{
    public class UserDbProvider : IUserDbProvider
    {
        private readonly SecureDbContext _dbContext;

        public UserDbProvider(SecureDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddUserAsync(User newUser)
        {
            await _dbContext.Users.AddAsync(newUser);

            return await SaveChanges();
        }
        public async Task<bool> UpdateUser(User? user)
        {
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Update(user);

            return await SaveChanges();
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> FindUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);

            return await SaveChanges();
        }

        public async Task<bool> VerifyModeratorAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.ModeratorVerifiedAt = DateTime.Now;

            _dbContext.Users.Update(user);

            return await SaveChanges();
        }

        public async Task<List<User>> GetUnverifiedModeratorsAsync()
        {
            return await _dbContext.Users
                .Where(user => user.UserRole == EUserRole.MODERATOR && user.ModeratorVerifiedAt == null)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
