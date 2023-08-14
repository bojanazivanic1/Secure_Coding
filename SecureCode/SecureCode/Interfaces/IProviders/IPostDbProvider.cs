using SecureCode.Models;

namespace SecureCode.Interfaces.IProviders
{
    public interface IPostDbProvider
    {
        Task<bool> AddPostAsync(Post newPost);
        Task<List<Post>> GetAllPostsAsync(int contributorId);
        Task<List<Post>> GetAllPostsAsync();
        Task<bool> VerifyPostAsync(int postId);
        Task<List<Post>> GetVerifiedPostsAsync();
        Task<bool> SaveChanges();
    }
}
