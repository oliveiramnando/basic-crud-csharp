using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs.Todos;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services;

public class TodoService: ITodoService
{
    // Constructor dependency injection
    private readonly AppDbContext _dbContext;
    public TodoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<TodoResponse>> GetAllAsync(
        bool? isCompleted,
        CancellationToken cancellationToken)
    {
        IQueryable<TodoItem> query = _dbContext.TodoItems
            .AsNoTracking();
        
        if (isCompleted.HasValue)
        {
            query = query.Where(todo =>
                todo.IsCompleted == isCompleted.Value);
        }

        return await query 
            .OrderByDescending(todo => todo.CreatedAtUtc)
            .Select(todo => ToResponse(todo))
            .ToListAsync(cancellationToken);
    }

    public async Task<TodoResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.TodoItems
            .AsNoTracking()
            .Where(todo => todo.Id == id)
            .Select(todo => ToResponse(todo))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TodoResponse> CreateAsync(
        CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Title = request.Title.Trim(),
            Description = NormalizeDescription(request.Description),
            IsCompleted = false,
            CreatedAtUtc = DateTime.UtcNow,
            CompletedAtUtc = null
        };

        _dbContext.TodoItems.Add(todo);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(todo);
    }

    public async Task<TodoResponse?> UpdateAsync(
        int id,
        UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        TodoItem? todo = await _dbContext.TodoItems
            .FirstOrDefaultAsync(
                todo => todo.Id == id,
                cancellationToken);
        
        if (todo is null)
        {
            return null;
        }

        bool wasCompleted = todo.IsCompleted;

        todo.Title = request.Title.Trim();
        todo.Description = NormalizeDescription(request.Description);
        todo.IsCompleted = request.IsCompleted;

        if (!wasCompleted && request.IsCompleted)
        {
            todo.CompletedAtUtc = DateTime.UtcNow;
        }
        else if (wasCompleted && !request.IsCompleted)
        {
            todo.CompletedAtUtc = null;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(todo);
    }

    public async Task<TodoResponse?> MarkCompleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        TodoItem? todo = await _dbContext.TodoItems
            .FirstOrDefaultAsync(
                todo => todo.Id == id,
                cancellationToken);
        
        if (todo is null)
        {
            return null;
        }

        if (!todo.IsCompleted)
        {
            todo.IsCompleted = true;
            todo.CompletedAtUtc = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return ToResponse(todo);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        TodoItem? todo = await _dbContext.TodoItems
            .FirstOrDefaultAsync(
                todo => todo.Id == id,
                cancellationToken);
        
        if (todo is null)
        {
            return false;
        }

        _dbContext.TodoItems.Remove(todo);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static TodoResponse ToResponse(TodoItem todo)
    {
        return new TodoResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAtUtc = todo.CreatedAtUtc,
            CompletedAtUtc = todo.CompletedAtUtc
        };
    }

    private static string? NormalizeDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return null;
        }

        return description.Trim();
    }
}
