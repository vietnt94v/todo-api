using System.ComponentModel.DataAnnotations;

namespace TodoApi.Application.DTOs;

public class UpdateTodoItemDto
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
}
