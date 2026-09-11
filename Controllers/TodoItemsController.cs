using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoItemsController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAllTodos(CancellationToken cancellationToken)
    {
        var todos = await _todoService.GetAllAsync(cancellationToken);
        return Ok(todos);
    }

    [HttpGet("complete")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetCompleteTodos(CancellationToken cancellationToken)
    {
        var todos = await _todoService.GetCompleteAsync(cancellationToken);
        return Ok(todos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoItemDto>> GetTodo(int id, CancellationToken cancellationToken)
    {
        var todo = await _todoService.GetByIdAsync(id, cancellationToken);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> CreateTodo(CreateTodoItemDto dto, CancellationToken cancellationToken)
    {
        var created = await _todoService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetTodo), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodo(int id, UpdateTodoItemDto dto, CancellationToken cancellationToken)
    {
        var updated = await _todoService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodo(int id, CancellationToken cancellationToken)
    {
        var deleted = await _todoService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
