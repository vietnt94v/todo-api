using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_api.Data;
using todo_api.Models;

namespace todo_api.Controllers;

[ApiController]
[Route("todo")]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _db;

    public TodoItemsController(TodoContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllTodos()
    {
        return await _db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync();
    }

    [HttpGet("complete")]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetCompleteTodos()
    {
        return await _db.Todos.Where(t => t.IsComplete).Select(x => new TodoItemDTO(x)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodo(int id)
    {
        var todo = await _db.Todos.FindAsync(id);

        return todo is null ? NotFound() : new TodoItemDTO(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> CreateTodo(TodoItemDTO todoItemDTO)
    {
        var todoItem = new Todo
        {
            IsComplete = todoItemDTO.IsComplete,
            Name = todoItemDTO.Name
        };

        _db.Todos.Add(todoItem);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodo), new { id = todoItem.Id }, new TodoItemDTO(todoItem));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoItemDTO todoItemDTO)
    {
        var todo = await _db.Todos.FindAsync(id);

        if (todo is null) return NotFound();

        todo.Name = todoItemDTO.Name;
        todo.IsComplete = todoItemDTO.IsComplete;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _db.Todos.FindAsync(id);

        if (todo is null) return NotFound();

        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
