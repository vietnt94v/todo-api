using TodoApi.Application.DTOs;

namespace TodoApi.Application.Interfaces;

public interface ITodoService
{
    Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodoItemDto>> GetCompleteAsync(CancellationToken cancellationToken = default);
    Task<TodoItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, UpdateTodoItemDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
