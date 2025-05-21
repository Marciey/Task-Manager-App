using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    /// <summary>
    /// Represents a task item belonging to a user.
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public TaskStatus Status { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Enum representing the status of a task.
    /// </summary>
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
} 