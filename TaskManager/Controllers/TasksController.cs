using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    /// <summary>
    /// Handles CRUD operations and status updates for user tasks.
    /// </summary>
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Lists tasks for the current user, with search and filter options.
        /// </summary>
        public async Task<IActionResult> Index(string searchString, TaskManager.Models.TaskStatus? status, DateTime? dueDate)
        {
            var user = await _userManager.GetUserAsync(User);
            var tasks = _context.Tasks.Where(t => t.UserId == user.Id);

            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString) || t.Description.Contains(searchString));
            }

            if (status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == status.Value);
            }

            if (dueDate.HasValue)
            {
                tasks = tasks.Where(t => t.DueDate.HasValue && t.DueDate.Value.Date == dueDate.Value.Date);
            }

            return View(await tasks.OrderByDescending(t => t.CreatedAt).ToListAsync());
        }

        /// <summary>
        /// Shows the create task form.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles task creation.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var task = new TaskItem
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    UserId = user.Id
                };

                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        /// <summary>
        /// Shows the edit task form.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

            if (task == null)
            {
                return NotFound();
            }

            var model = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status
            };

            return View(model);
        }

        /// <summary>
        /// Handles task editing.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

                if (task == null)
                {
                    return NotFound();
                }

                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = model.Status;
                task.UpdatedAt = DateTime.UtcNow;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        /// <summary>
        /// Handles task deletion.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Handles AJAX status updates for tasks.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, TaskManager.Models.TaskStatus status)
        {
            var user = await _userManager.GetUserAsync(User);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

            if (task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// Checks if a task exists by ID.
        /// </summary>
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
} 