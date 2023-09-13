namespace SecureCode.Interfaces.IServices
{
    public interface IJokeService
    {
        Task<string> GetRandomJokeAsync();
    }
}
