using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Infrastructure;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
        }
        //GET /
        public async Task<ActionResult> Index()
        {
            IQueryable<TodoList> items = from i in context.ToDolist orderby i.Id select i;

            List<TodoList> todoList = await items.ToListAsync();

            return View(todoList);
        }

        //GET /todo/create
        public IActionResult Create() => View();


        //Post /todo / createe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been added!";

                return RedirectToAction("Index");
            }
            return View(item);
        }


        //GET / todo/edit
        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await context.ToDolist.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        //Post /todo / edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //GET / todo/delete
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await context.ToDolist.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.ToDolist.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted!";
            }
            return RedirectToAction("Index");

        }
    }
}
