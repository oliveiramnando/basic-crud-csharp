using TodoApi.DTOs.Todos;

namespace TodoApi.Services.Interfaces;

public interface ITodoService
{
    Task<IReadOnlyList<TodoResponse>> getAllAsync(
        bool? isCompleted,
        CancellationToken cancellationToken);

    Task<TodoResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<TodoResponse> CreateAsync(
        CreateTodoRequest request,
        CancellationToken cancellationToken);

    Task<TodoResponse?> UpdateAsync(
        int id,
        UpdateTodoRequest request,
        CancellationToken cancellationToken);

    Task<TodoResponse?> MarkCompleteAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}