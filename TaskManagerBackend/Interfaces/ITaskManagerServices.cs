using TaskManagerBackend.Models;

namespace TaskManagerBackend.Interfaces
{
    public interface ITaskManagerServices{
        Task<IEnumerable<MyTask>> GetAllAsync();
        Task<MyTask> GetByIdAsync(Guid id);
        Task<MyTask> CreateTaskManagerAsync(CreateTaskManagerRequest request);
        Task<MyTask> UpdateTaskManagerAsync(Guid id, UpdateTaskManagerRequest request);
        Task DeleteTaskManagerAsync(Guid id);
    }
}
