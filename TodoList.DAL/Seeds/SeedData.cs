using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TodoList.DAL.Entities;
using TodoList.DAL.Enums;

namespace TodoList.DAL.Seeds
{
    public class SeedData
    {
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            ToDoListDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<ToDoListDbContext>();

            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(UsersWithToDos());
                await context.SaveChangesAsync();
            }


        }





        private static IEnumerable<User> UsersWithToDos()
        {
            return new List<User>()
            {
                new User() {
                    
                    FullName = "David Ukpoju",
                    Email = "david@gmail.com",
                    Password= "password1",
                    TodoList = new List<Todo>()
                    {
                        new Todo()
                        {
                            
                            Title = "Build Project",
                            Description = "Building a project",
                            Priority = Priority.High,
                            DueDate = DateTime.Now.AddDays(3)

                        },
                        new Todo()
                        {
                           
                            Title = "Build Project2",
                            Description = "Building a project",
                            Priority = Priority.Low,
                            DueDate = DateTime.Now,
                            IsDone = true

                        },
                        new Todo()
                        {

                            Title = "Build Project3",
                            Description = "Building a project3",

                            DueDate = DateTime.Now,


                        }
                    }

                },
                new User() {
                    
                    FullName = "Jonathan Ukpoju",
                    Email = "jona@gmail.com",
                    Password= "password2",
                    TodoList = new List<Todo>()
                    {
                        new Todo()
                        {
                            
                            Title = "Start Project1",
                            Description = "Starting a project",
                            DueDate = DateTime.Now.AddDays(3)

                        },
                        new Todo()
                        {
                            
                            Title = "Plan Project2",
                            Description = "Planning a project",
                            Priority = Priority.High,
                            DueDate = DateTime.Now.AddDays(9),


                        },
                        new Todo()
                        {
                           
                            Title = "Deliver Project3",
                            Description = "Delivering a project3",
                            DueDate = DateTime.Now,
                            IsDone = true

                        }
                    }

                }

            };
        }
    }
}
