using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedTodo.Data;
using AdvancedTodo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTodoWebAPI.Controllers {
[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase {
    private ITodosService todosService;

    public TodosController(ITodosService todosService) {
        this.todosService = todosService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Todo>>> GetTodos() {
        try {
            IList<Todo> todos = await todosService.GetTodosAsync();
            return Ok(todos);
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteTodo(int id) {
        try {
            await todosService.RemoveTodoAsync(id);
            return Ok();
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo) {
        try {
            Todo added = await todosService.AddTodoAsync(todo);
            return Ok(added); // return newly added to-do, to get the auto generated id
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("{id:int}")]
    public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo) {
        try {
            await todosService.UpdateAsync(todo);
            return Ok(); 
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
}
}