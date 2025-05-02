using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Entities;
using TaskManagementApi.IServices;

namespace TaskManagementApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDBContext _dbContext;
        public TaskService(TaskDBContext dbContext)
        {
            _dbContext = dbContext;       
        }

        public async Task<TaskItem> addTaskAsync(TaskItem request)
        {
            _dbContext.Tasks.Add(request);
            await _dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<TaskItem> updateTaskAsync(TaskItem request)
        {
            _dbContext.Tasks.Update(request);
            await _dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<IEnumerable<TaskItem>> getAllTasksAsync()
        {
            var allTasks = await _dbContext.Tasks.Include(c => c.Category).ToListAsync();
            return allTasks;
        }

        public async Task<TaskItem?> getByTaskIdAsync(int id)
        {
            var myTask = await _dbContext.Tasks.Include(c => c.Category).FirstOrDefaultAsync(t => t.TaskId == id);
            return myTask;
        }

        public async Task deletaTaskAsync(int TaskId)
        {
            var task = await _dbContext.Tasks.FindAsync(TaskId);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
