using Microsoft.EntityFrameworkCore;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Models;
using TodoList.DAL.Entities;
using TodoList.DAL.Repository;

namespace TodoList.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();  
            
        }
        public void Create(CreateUserVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserWithTaskVM>> GetUsersWithTasksAsync()
        {

            return (await _userRepo.GetAllAsync(include: u => u.Include(t => t.TodoList))).Select(u => new UserWithTaskVM
            {
                Fullname = u.FullName,
                Tasks = u.TodoList.Select(t => new TaskVM
                {
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate.ToString("d"),
                    Priority = t.Priority.ToString(),
                    Status = t.IsDone ? "Done" : "Not Done"
                })
            });
        }
    }
}
