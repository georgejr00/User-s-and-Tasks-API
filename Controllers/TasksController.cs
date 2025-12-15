using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Repositories;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // --- STANDARD CRUD ---

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return Ok(await _taskRepository.GetAllTasksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
        {
            await _taskRepository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            if (id != task.Id) return BadRequest();
            await _taskRepository.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }

        // --- NEW FILTERING ENDPOINTS ---

        [HttpGet("expired")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetExpiredTasks()
        {
            return Ok(await _taskRepository.GetExpiredTasksAsync());
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetActiveTasks()
        {
            return Ok(await _taskRepository.GetActiveTasksAsync());
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasksByDate(DateTime date)
        {
            return Ok(await _taskRepository.GetTasksByDateAsync(date));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasksByUser(int userId)
        {
            return Ok(await _taskRepository.GetTasksByUserAsync(userId));
        }
    }
}