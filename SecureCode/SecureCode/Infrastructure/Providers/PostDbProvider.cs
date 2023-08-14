using Microsoft.EntityFrameworkCore;
using SecureCode.Interfaces.IProviders;
using SecureCode.Models;
using System;

namespace SecureCode.Infrastructure.Providers
{
    public class PostDbProvider : IPostDbProvider
    {
        private readonly SecureDbContext _dbContext;

        public PostDbProvider(SecureDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddPostAsync(Post newPost)
        {
            await _dbContext.Posts.AddAsync(newPost);

            return await SaveChanges();
        }

        public async Task<List<Post>> GetAllPostsAsync(int contributorId)
        {
            List<Post> posts = await _dbContext.Posts
                .Where(p => p.ContributorId == contributorId)
                .ToListAsync();

            return posts;
        }

        public async Task<List<Post>> GetVerifiedPostsAsync()
        {
            List<Post> verifiedPosts = await _dbContext.Posts
                .Where(post => post.MessageVerified)
                .ToListAsync();

            return verifiedPosts;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<bool> VerifyPostAsync(int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return false;
            }

            post.MessageVerified = true;

            return await SaveChanges();
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
