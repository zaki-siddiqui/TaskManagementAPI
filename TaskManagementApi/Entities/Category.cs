using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId {  get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
