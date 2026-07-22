
namespace TodoApi.DTOs.Todos;

public class TodoResponse
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }
}