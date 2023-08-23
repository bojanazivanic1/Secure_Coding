using System.ComponentModel.DataAnnotations;

namespace InsecureCode.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool MessageVerified { get; set; } = false;
        public int ContributorId { get; set; }
        public User? Contributor { get; set; }
    }
}
