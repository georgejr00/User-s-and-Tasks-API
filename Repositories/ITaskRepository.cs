using TaskApi.Models;

namespace TaskApi.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);

        // --- NEW FILTERING METHODS ---
        Task<List<TaskItem>> GetExpiredTasksAsync();
        Task<List<TaskItem>> GetActiveTasksAsync();
        Task<List<TaskItem>> GetTasksByDateAsync(DateTime date);
        Task<List<TaskItem>> GetTasksByUserAsync(int userId);
    }
}