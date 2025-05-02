using TaskManagementApi.Entities;

namespace TaskManagementApi.IServices
{
    public interface ITaskService
    {
        Task<TaskItem> addTaskAsync(TaskItem request);
        Task<TaskItem> updateTaskAsync(TaskItem request);
        Task<IEnumerable<TaskItem>> getAllTasksAsync();
        Task<TaskItem?> getByTaskIdAsync(int id);
        Task deletaTaskAsync(int taskId);
    }
}
