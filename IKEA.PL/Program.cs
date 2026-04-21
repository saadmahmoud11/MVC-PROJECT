using IKEA.BLL.Common.MappingProfiles;
using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.Services.DepartmentService;
using IKEA.BLL.Services.EmployeeService;
using IKEA.DAL.Contexts;
using IKEA.DAL.Models.Auth;
using IKEA.DAL.Reposatories.DepartmentRepo;
using IKEA.DAL.Reposatories.EmployeeRepo;
using IKEA.DAL.UOW;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAttachmentServices,AttachmentServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.ExpireTimeSpan = TimeSpan.FromHours(2);
            //});

            builder.Services.AddAutoMapper(cfg => { }, typeof(ProjectMapperProfile));

            var app = builder.Build();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
                

            app.Run();
        }
    }
}
