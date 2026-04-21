using IKEA.DAL.Models.Auth;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace IKEA.DAL.Contexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
}