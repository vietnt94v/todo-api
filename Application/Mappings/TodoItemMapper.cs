using TodoApi.Application.DTOs;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Mappings;

public static class TodoItemMapper
{
    public static TodoItemDto ToDto(this TodoItem entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        IsComplete = entity.IsComplete
    };

    public static TodoItem ToEntity(this CreateTodoItemDto dto) => new()
    {
        Name = dto.Name,
        IsComplete = dto.IsComplete
    };

    public static void Apply(this TodoItem entity, UpdateTodoItemDto dto)
    {
        entity.Name = dto.Name;
        entity.IsComplete = dto.IsComplete;
    }
}
