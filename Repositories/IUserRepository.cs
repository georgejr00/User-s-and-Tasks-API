using TaskApi.Models;

namespace TaskApi.Repositories
{
    public interface IUserRepository
    {
        // The contract: "These are the things we must be able to do"
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}