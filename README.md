# IKEA Sample MVC Project

A layered ASP.NET Core web application implementing an administrative portal for managing departments, employees and user accounts. The solution follows a Presentation → Business → Data Access separation and uses ASP.NET Core MVC with Razor views for the UI.

## Key Features
- Authentication and account management (register, login, reset/forgot password).
- CRUD for Departments and Employees with server-side validation and ViewModels/DTOs.
- Layered architecture (Presentation Layer, Business Logic Layer, Data Access Layer).
- Environment-aware error handling and structured logging via `ILogger`.
- Email configuration support (SMTP/email settings present).
- Uses dependency injection for services and environment access (`IWebHostEnvironment`).

## Architecture
- IKEA.PL (Presentation Layer) — ASP.NET Core MVC controllers and Razor views.
- IKEA.BLL (Business Logic Layer) — DTOs and service interfaces/implementations.
- IKEA.DAL (Data Access Layer) — models and persistence (adaptable to EF Core or other ORMs).
- DTOs and ViewModels decouple the UI from domain models and persistence.
- Services are injected using the built-in DI container.

## Technology stack
- .NET 9
- C# 13
- ASP.NET Core MVC with Razor views
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- Logging (Microsoft.Extensions.Logging)
- Data annotations for model validation
- SMTP/email configuration (helper class present)
- Git, Visual Studio 2022 (recommended)

## How to run (local, typical)
1. Clone the repository:
   - git clone https://github.com/saadmahmoud11/MVC-PROJECT
2. Open solution in Visual Studio 2022.
3. Configure connection string and email settings in __appsettings.json__ or environment secrets.
4. Restore NuGet packages, build the solution.
5. Run the project (F5) — it starts the web host using the configured environment.
6. Use seeded/admin accounts or register new users via the Account pages.

Notes:
- If the project uses Entity Framework Core, run database migrations or ensure the database exists before running.
- Configure SMTP/email credentials before using password reset or notification features.

## Developer notes
- Controllers use ViewModels (e.g., `DepartmentViewModel`) and call BLL services via interfaces (e.g., `IDepartmentService`).
- Controller actions use pattern: Validate ModelState → map ViewModel to DTO → call service → handle success/failure and exceptions.
- Environment checks (via `IWebHostEnvironment.IsDevelopment()`) are used to control error logging and user feedback.
- Use `TempData`, `ViewData`, and `ViewBag` for transient UI messages and view data.
- Prefer returning appropriate HTTP responses: `BadRequest()`, `NotFound()`, `RedirectToAction(...)`, or `View(...)`.

## Contributing
- Follow existing layered patterns and naming conventions.
- Add unit tests for services and controllers where feasible.
- Keep UI concerns in PL, business logic in BLL, and persistence in DAL.


