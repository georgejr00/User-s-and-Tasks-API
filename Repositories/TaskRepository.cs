using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.Include(t => t.Assignee).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        // --- FILTERING METHODS (Now properly inside the class) ---

        public async Task<List<TaskItem>> GetExpiredTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetActiveTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetTasksByDateAsync(DateTime date)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetTasksByUserAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.AssigneeId == userId)
                .ToListAsync();
        }
    }
}