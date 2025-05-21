# Task Manager Web App

A modern, responsive personal task manager web application built with ASP.NET Core MVC, Entity Framework Core, and SQL Server. Features user authentication, task CRUD, status management, due dates, reminders, search/filter, AJAX interactivity, and a dark blue theme with HTML5/CSS3 enhancements.

## Features
- User registration, login, and logout
- Create, edit, delete, and view personal tasks
- Task status: Pending, In Progress, Completed
- Due dates and reminders
- Search and filter tasks by title, status, and due date
- Responsive UI with Bootstrap 5 and custom dark blue theme
- AJAX for status updates
- HTML5 semantic elements and ARIA accessibility

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or compatible SQL Server)

### Setup
1. Clone the repository:
   ```sh
   git clone <your-repo-url>
   cd Task-Manage-App/TaskManager
   ```
2. Update the connection string in `appsettings.json` if needed.
3. Run database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```
5. Open your browser to the URL shown in the terminal (e.g., http://localhost:5195).

## Deployment (IIS 7/8)
- Install the .NET Hosting Bundle on your server
- Publish the app:
  ```sh
  dotnet publish -c Release -o C:\publish\TaskManager
  ```
- Set up a new IIS site pointing to the publish folder
- Use 'No Managed Code' for the app pool
- Ensure permissions for the IIS AppPool user

## Project Structure
- `Controllers/` - MVC controllers
- `Models/` - Entity and view models
- `Views/` - Razor views (HTML5/CSS3)
- `wwwroot/` - Static files (CSS, JS, images)
- `Data/` - EF Core DbContext

## License
MIT 