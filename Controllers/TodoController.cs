using System.Threading.Tasks;
using SimpleTodo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace SimpleTodo.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("getListaTodo")]
        public async Task<IActionResult> Get(
            [FromServices] AppDbContext context)
        {
            var todos = await context.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }
    }
}