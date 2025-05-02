using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Entities;
using TaskManagementApi.IServices;

namespace TaskManagementApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TaskDBContext _dbContext;
        public CategoryService(TaskDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Category>> getAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
