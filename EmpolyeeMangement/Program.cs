using EmpolyeeMangement.Models;
using EmpolyeeMangement.Models.Account;
using EmpolyeeMangement.Models.Admin;
using EmpolyeeMangement.Models.Employee;
using EmpolyeeMangement.Models.NewFolder;
using EmpolyeeMangement.Models.Report;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmpolyeeMangement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option=>{
                option.LoginPath = "/Account/Login";

            });
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DBContexts>(x => x.UseSqlServer(connection));
            builder.Services.AddTransient<IAdmin, ImpAdmin>();
            builder.Services.AddTransient<IEmployee, ImpEmployee>();
            builder.Services.AddTransient<IReport, ImpReport>();
         
            builder.Services.AddSession(x => x.IdleTimeout = TimeSpan.FromMinutes(30));
            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
         


            app.Run();
           
            
        }
    }
}
