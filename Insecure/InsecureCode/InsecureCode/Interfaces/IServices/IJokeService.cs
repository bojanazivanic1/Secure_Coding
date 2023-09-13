namespace InsecureCode.Interfaces.IServices
{
    public interface IJokeService
    {
        Task<string> GetRandomJokeAsync();
    }
}
