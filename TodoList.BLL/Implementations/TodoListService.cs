using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Models;
using TodoList.DAL.Entities;
using TodoList.DAL.Repository;

namespace TodoList.BLL.Implementations
{

    public class TodoListService : ITodoListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Todo> _taskRepo;

        public TodoListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _taskRepo = _unitOfWork.GetRepository<Todo>();
            _userRepo = _unitOfWork.GetRepository<User>();
            
        }

        public async Task<(bool successful, string msg)> AddOrUpdateAsync(AddOrUpdateTaskVM model)
        {

            // _taskRepo.GetSingleByAsync(t => t.UserId == model.UserId && t.Id == model.TaskId);

            User user = await _userRepo.GetSingleByAsync(u => u.Id == model.UserId, include: u => u.Include(x => x.TodoList), tracking: true);

            if (user == null)
            {
                return (false, $"User with id:{model.UserId} wasn't found");
            }

            Todo task = user.TodoList.SingleOrDefault(t => t.Id == model.TaskId);


            if (task != null)
            {

                _mapper.Map(model, task);

                //
                // task.Title = model.Title;
                // task.Description = model.Description;
                // task.Priority = (model.Priority ?? Priority.Normal);
                // task.DueDate = model.DueDate;

                await _unitOfWork.SaveChangesAsync();
                return (true, "Update Successful!");
            }

            // var newTask = _mapper.Map<AddOrUpdateTaskVM,Todo>(model);
            var newTask = _mapper.Map<Todo>(model);

            // var newTask = new Todo
            // {
            //  
            //     Title = model.Title,
            //     Description = model.Description,
            //     Priority = model.Priority ?? Priority.Normal,
            //     DueDate = model.DueDate,
            //
            // };
            user.TodoList.Add(newTask);

            var rowChanges = await _unitOfWork.SaveChangesAsync();

            return rowChanges > 0 ? (true, $"Task: {model.Title} was successfully created!") : (false, "Failed To save changes!");



        }

        public async Task<(bool successful, string msg)> DeleteAsync(int userId, int taskId)
        {
            // User user = ToDoListDbContext.GetUsersWithToDos().SingleOrDefault(u => u.Id == model.UserId);
            User user = await _userRepo.GetSingleByAsync(u => u.Id == userId,
                include: u => u.Include(x => x.TodoList.Where(u => u.UserId == userId)), tracking: true);

            if (user == null)
            {
                return (false, $"User with id:{userId} wasn't found");
            }

            Todo userTask = user.TodoList.FirstOrDefault(t => t.Id == taskId);
            if (userTask != null)
            {
                user.TodoList.Remove(userTask);

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Task with Title:{userTask.Title} was deleted") : (false, $"Delete Failed");
            }
            return (false, $"Task with id:{taskId} was not found");
        }


        public (bool Done, string msg) ToggleTaskStatus(int userId, int taskId)
        {
            // User user = ToDoListDbContext.GetUsersWithToDos().SingleOrDefault(u => u.Id == model.UserId);
            User user = null;

            if (user != null)
            {
                var todo = user.TodoList.SingleOrDefault(t => t.Id == taskId);
                if (todo != null)
                {
                    todo.IsDone = !todo.IsDone;
                    return (true, $"task status with the id {taskId} updated");
                }
            }

            return (false, $"user with the id {userId} not found");
        }

        public (Todo to, string msg) GetTask(int userId, int taskId)
        {
            // User user = ToDoListDbContext.GetUsersWithToDos().SingleOrDefault(u => u.Id == model.UserId);
            User user = null;

            if (user == null)
            {
                return (null, "User not found");
            }

            Todo todo = user.TodoList.FirstOrDefault(t => t.Id == taskId);

            if (todo == null)
            {
                return (null, "Task not found");
            }

            return (todo, "");

        }

        public IEnumerable<Todo> GetTodoList()
        {
            List<Todo> todoList = new List<Todo>();


            // var todos = ToDoListDbContext.GetUsersWithToDos().SelectMany(t => t.TodoList).ToList();
            var todos = todoList;


            return todos;
        }
    }
}
