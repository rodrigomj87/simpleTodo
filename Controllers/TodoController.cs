using System.Threading.Tasks;
using SimpleTodo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTodo.ViewModels;
using SimpleTodo.Models;
using System;

namespace SimpleTodo.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("getListaTodo")]
        public async Task<IActionResult> GetListaTodoAsync(
            [FromServices] AppDbContext context)
        {
            var todos = await context.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("getTodoById/{id}")]
        public async Task<IActionResult> GetbyIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(_ => _.todoId == id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost("postTodos")]
        public async Task<IActionResult> PostTodoAsync([FromServices] AppDbContext context,
        [FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                todoDate = DateTime.Now,
                todoDone = false,
                todoTitle = model.Title
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created(uri: $"todo/getTodoById/{todo.todoId}", todo);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpPut("putTodo/{id}")]
        public async Task<IActionResult> PutTodoAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTodoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(_ => _.todoId == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.todoTitle = model.Title;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("deleteTodo/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(_ => _.todoId == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}