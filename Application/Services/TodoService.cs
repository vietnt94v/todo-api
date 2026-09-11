using Microsoft.EntityFrameworkCore;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Mappings;
using TodoApi.Infrastructure.Data;

namespace TodoApi.Application.Services;

public class TodoService : ITodoService
{
    private readonly TodoContext _db;

    public TodoService(TodoContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _db.TodoItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return items.Select(x => x.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<TodoItemDto>> GetCompleteAsync(CancellationToken cancellationToken = default)
    {
        var items = await _db.TodoItems
            .AsNoTracking()
            .Where(t => t.IsComplete)
            .ToListAsync(cancellationToken);

        return items.Select(x => x.ToDto()).ToList();
    }

    public async Task<TodoItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var todo = await _db.TodoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return todo?.ToDto();
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        _db.TodoItems.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, UpdateTodoItemDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _db.TodoItems.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Apply(dto);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.TodoItems.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _db.TodoItems.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
