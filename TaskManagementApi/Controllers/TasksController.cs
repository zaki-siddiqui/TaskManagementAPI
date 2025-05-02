using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using TaskManagementApi.Data.UnitOfWork;
using TaskManagementApi.Entities;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody]TaskDto request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                CategoryId = request.CategoryId,
            };

            await _unitOfWork.Tasks.addTaskAsync(task);
            await _unitOfWork.CompleteAsync();

            request.TaskId = task.TaskId;
            request.CategoryName = (await _unitOfWork.Categories.getAllCategoriesAsync()).FirstOrDefault(x => x.CategoryId == task.CategoryId)?.CategoryName;

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> updateTask(int id, [FromBody] TaskDto request)
        {
            if(id != request.TaskId)
                return BadRequest("Task Id not found");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskRes = await _unitOfWork.Tasks.getByTaskIdAsync(id);

            if (taskRes is null)
                return NotFound("Task not found");

            taskRes.Title = request.Title;
            taskRes.Description = request.Description;
            taskRes.DueDate = request.DueDate;
            taskRes.IsCompleted = request.IsCompleted;
            taskRes.CategoryId = request.CategoryId;

            await _unitOfWork.Tasks.updateTaskAsync(taskRes);
            await _unitOfWork.CompleteAsync();

            request.CategoryName = (await _unitOfWork.Categories.getAllCategoriesAsync()).FirstOrDefault(x => x.CategoryId == taskRes.CategoryId)?.CategoryName;
            return Ok(request);
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult<TaskDto>> toggleIsComplete(int id)
        {
            var myTask = await _unitOfWork.Tasks.getByTaskIdAsync(id);
            if (myTask is null)
                return BadRequest("Task not found");

            myTask.IsCompleted = !myTask.IsCompleted;
            await _unitOfWork.Tasks.updateTaskAsync(myTask);
            await _unitOfWork.CompleteAsync();

            var taskDto = new TaskDto
            {
                TaskId = myTask.TaskId,
                Title = myTask.Title,
                Description = myTask.Description,
                DueDate = myTask.DueDate,
                IsCompleted = myTask.IsCompleted,
                CategoryId = myTask.CategoryId,
                CategoryName = myTask.Category?.CategoryName
            };

            return Ok(taskDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTask(int id)
        {
            var task = await _unitOfWork.Tasks.getByTaskIdAsync(id);
            if (task is null)
                return NotFound("Task not found");

            await _unitOfWork.Tasks.deletaTaskAsync(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        
        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDelete([FromBody] int[] ids)
        {
            foreach (var id in ids)
            {
                var task = await _unitOfWork.Tasks.getByTaskIdAsync(id);
                if (task != null)
                {
                    await _unitOfWork.Tasks.deletaTaskAsync(id);
                }
            }
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _unitOfWork.Tasks.getAllTasksAsync();

            var taskDtos = tasks.Select(t => new TaskDto
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted,
                CategoryId = t.CategoryId,
                CategoryName = t.Category?.CategoryName
            }).ToList();

            return Ok(taskDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _unitOfWork.Tasks.getByTaskIdAsync(id);
            if(task == null)
                return NotFound();

            var taskResult = new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.CategoryName
            };

            return Ok(taskResult);
        }


        
        [HttpGet("ByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(string status)
        {
            var tasks = await _unitOfWork.Tasks.getAllTasksAsync();
            if (status.ToLower() != "all")
            {
                tasks = tasks.Where(t => status.ToLower() == "completed" ? t.IsCompleted : !t.IsCompleted);
            }
            var taskDtos = tasks.Select(t => new TaskDto
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted,
                CategoryId = t.CategoryId,
                CategoryName = t.Category?.CategoryName
            }).ToList();
            return Ok(taskDtos);
        }

    }
}
