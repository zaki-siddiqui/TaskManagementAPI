using TaskManagementApi.IServices;

namespace TaskManagementApi.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskService Tasks { get; }
        ICategoryService Categories { get; }
        Task<int> CompleteAsync();
    }
}
