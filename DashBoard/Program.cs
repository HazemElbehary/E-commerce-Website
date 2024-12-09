using LinkDev.Talabat.APIs.Controllers.Middlewares;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Core.Application.DepaendancyInjection;
using LinkDev.Talabat.Core.Domain;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LinkDev.Talabat.APIs.LoggedInUserServices;
using LinkDev.Talabat.Core.Application.Abstraction.LoggedInUserServices;
using LinkDev.Talabat.Core.Domain.Data;

namespace DashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
            });
           

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            builder.Services.AddPresistenceServices(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Middlewares
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
