using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.Todos;

public class UpdateTodoRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public required string Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}