using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TaskManager.Models
{
    /// <summary>
    /// Represents an application user with a collection of tasks.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TaskItem> Tasks { get; set; }
    }
} 