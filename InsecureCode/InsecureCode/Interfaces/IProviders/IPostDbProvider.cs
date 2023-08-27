using InsecureCode.Models;

namespace InsecureCode.Interfaces.IProviders
{
    public interface IPostDbProvider
    {
        Task<bool> AddPostAsync(Post newPost);
        Task<Post?> FindPostByIdAsync(int id);
        Task<List<Post>> GetAllPostsAsync(int contributorId);
        Task<List<Post>> GetAllPostsAsync();
        Task<bool> VerifyPostAsync(int postId);
        Task<List<Post>> GetVerifiedPostsAsync();
    }
}
