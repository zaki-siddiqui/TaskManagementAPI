using TaskManagementApi.IServices;
using TaskManagementApi.Services;

namespace TaskManagementApi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDBContext _dbContext;

        public ITaskService Tasks { get; }

        public ICategoryService Categories { get; }

        public UnitOfWork(TaskDBContext dbContext)
        {
            _dbContext = dbContext;
            Tasks = new TaskService(dbContext);
            Categories = new CategoryService(dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
