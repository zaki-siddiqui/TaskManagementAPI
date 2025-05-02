using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskManagementApi.Data.UnitOfWork;
using TaskManagementApi.Entities;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<ActionResult<List<Category>>> getCategories()
        {
            var catRes = await _unitOfWork.Categories.getAllCategoriesAsync();
            return Ok(catRes);
        }
    }
}
