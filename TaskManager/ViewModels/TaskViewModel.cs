using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for creating and editing tasks.
    /// </summary>
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        public TaskManager.Models.TaskStatus Status { get; set; }
    }
} 