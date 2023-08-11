﻿using InsecureCode.Interfaces.IProviders;
using InsecureCode.Models;
using Microsoft.EntityFrameworkCore;

namespace InsecureCode.Infrastructure.Providers
{
    public class UserDbProvider : IUserDbProvider
    {
        private readonly InsecureDbContext _dbContext;

        public UserDbProvider(InsecureDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddUserAsync(User newUser)
        {
            await _dbContext.Users.AddAsync(newUser);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }           
        }
        public async Task<bool> UpdateUser(User? user)
        {
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> FindUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
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
