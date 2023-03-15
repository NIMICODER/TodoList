using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Models;

namespace TodoList.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    // [Route("[controller]/[action]/{id?}")]
    public class TodoListController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITodoListService _todoListService;

        public TodoListController(IUserService userService, ITodoListService todoListService)
        {
            _userService = userService;
            _todoListService = todoListService;

        }

        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetUsersWithTasksAsync();

            return View(model);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddOrUpdateTaskVM model)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await  _todoListService.AddOrUpdateAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                // TempData["ErrMsg"] = msg; for both views and redirect to actions

                ViewBag.ErrMsg = msg;

                return View("New");

            }

            return View("New");
        }
    }
}
