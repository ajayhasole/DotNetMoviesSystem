using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Models;
using System;

namespace Movies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext")));

            builder.Services.AddControllersWithViews();

            // Add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Use authentication and authorization if using Identity
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseSession(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "Admin/Login",
                    defaults: new { controller = "Admin", action = "Login" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}



























//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Movies.Models;

//namespace Movies
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddDbContext<MoviesContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext")));

//            builder.Services.AddControllersWithViews();

//            // Configure Identity if needed
//            // builder.Services.AddIdentity<Admin, IdentityRole>()
//            //     .AddEntityFrameworkStores<MoviesContext>()
//            //     .AddDefaultTokenProviders();


//            public void ConfigureServices(IServiceCollection services)
//            {
//                services.AddControllersWithViews();

//                // Add session support
//                services.AddSession(options =>
//                {
//                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust session timeout as needed
//                    options.Cookie.HttpOnly = true;
//                    options.Cookie.IsEssential = true;
//                });
//            }


//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            // Use authentication and authorization if using Identity
//            // app.UseAuthentication();
//            // app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "admin",
//                    pattern: "Admin/Login",
//                    defaults: new { controller = "Admin", action = "Login" }
//                );

//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=User1}/{action=Login}/{id?}");
//            });

//            app.Run();
//        }
//    }
//}

