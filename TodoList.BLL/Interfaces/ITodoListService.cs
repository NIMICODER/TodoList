﻿using System;
using System.Collections.Generic;
using System.Text;
using TodoList.BLL.Models;
using TodoList.DAL.Entities;

namespace TodoList.BLL.Interfaces
{
    public interface ITodoListService
    {
        Task<(bool successful, string msg)> AddOrUpdateAsync(AddOrUpdateTaskVM model);
        Task<(bool successful, string msg)> DeleteAsync(int userId, int taskId);
        (bool Done, string msg) ToggleTaskStatus(int userId, int taskId);
        (Todo to, string msg) GetTask(int userId, int taskId);
        IEnumerable<Todo> GetTodoList();

    }
}
