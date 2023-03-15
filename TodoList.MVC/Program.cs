using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoList.BLL.Implementations;
using TodoList.BLL.Interfaces;
using TodoList.DAL.Entities;
using TodoList.DAL.Repository;
using TodoList.DAL.Seeds;

namespace TodoList.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ToDoListDbContext>(opts =>
            {
                // this will only work if there's a section called ConnectionStrings on the appSettings
                var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection");

                //var defaultConn = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

                opts.UseSqlServer(defaultConn);

            });



            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<ToDoListDbContext>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITodoListService, TodoListService>();

            builder.Services.AddAutoMapper(Assembly.Load("TodoList.BLL"));
            //builder.Services.AddAutoMapper(typeof(Program));


            var app = builder.Build();

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

            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllerRoute(
                    name: "delete",
                    pattern: "{controller}/{action}/{userId?}/{taskId?}");

                e.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            await SeedData.EnsurePopulatedAsync(app);

            await app.RunAsync();
        }
    }
}