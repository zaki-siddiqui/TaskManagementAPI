using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Validators
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        private readonly TaskDBContext _dbcontext;

        public TaskDtoValidator(TaskDBContext dBContext)
        {
            _dbcontext = dBContext;

            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Task title is required")
                .MaximumLength(100).WithMessage("Task title must be 100 characters or less");

            RuleFor(t => t.Description)
                .MaximumLength(500).WithMessage("Description must be 500 characters or less")
                .When(t => t.Description != null);

            RuleFor(t => t.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Due date cannot be from the past")
                .When(t => t.DueDate.HasValue);

            RuleFor(t => t.CategoryId)
                .Must(id => _dbcontext.Categories.Any(c => c.CategoryId == id))
                .WithMessage("Invalid category id")
                .When(t => t.CategoryId != 0);
        }
    }
}
