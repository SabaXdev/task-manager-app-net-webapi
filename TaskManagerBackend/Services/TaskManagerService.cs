using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Interfaces;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Services
{
    public class TaskManagerService : ITaskManagerServices
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TaskManagerService(TaskManagerDbContext context, ILogger<TaskManagerService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MyTask>> GetAllAsync()
        {
            var taskManagers = await _context.TaskManagers.ToListAsync() ?? throw new Exception("Sorry, no Task item were found.");
            return taskManagers;
        }

        public async Task<MyTask> GetByIdAsync(Guid id)
        {
            var taskItem = await _context.TaskManagers.FindAsync(id) ?? throw new KeyNotFoundException($"Sorry, Task item with given {id} were not found.");
            return taskItem;
        }

        public async Task<MyTask> CreateTaskManagerAsync(CreateTaskManagerRequest request)
        {
            try
            {
                var myTaskItem = _mapper.Map<MyTask>(request);     //Convert request to the MyTask json object
                _context.TaskManagers.Add(myTaskItem);             //Add this object to the Database
                await _context.SaveChangesAsync();
                return myTaskItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating a Task item.");
                throw new Exception("An error occured while creating a Task item.");
            }
        }

        public async Task<MyTask> UpdateTaskManagerAsync(Guid id, UpdateTaskManagerRequest request)
        {
            try
            {
                var taskItem = await _context.TaskManagers.FindAsync(id) ?? throw new Exception($"Task item with id {id} not found.");

                if (request.Name != null)
                {
                    taskItem.Name = request.Name;
                }

                if (request.IsComplete)
                {
                    taskItem.IsComplete = request.IsComplete;
                }
                await _context.SaveChangesAsync();
                return taskItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
                throw new Exception("An error occurred while updating the todo item with id {id}.");
            }
        }

        public async Task DeleteTaskManagerAsync(Guid id)
        {
            var taskItem = await _context.TaskManagers.FindAsync(id) ?? throw new KeyNotFoundException($"Item with given id {id} not found.");

            _context.TaskManagers.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
    }
}
