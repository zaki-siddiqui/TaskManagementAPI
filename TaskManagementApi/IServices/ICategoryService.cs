using TaskManagementApi.Entities;

namespace TaskManagementApi.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> getAllCategoriesAsync();
    }
}
