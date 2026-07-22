using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Todos;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<TodoResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TodoResponse>>> GetAll(
        [FromQuery] bool? isCompleted,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<TodoResponse> todos =
            await _todoService.GetAllAsync(
                isCompleted,
                cancellationToken);
        return Ok(todos);
    }

    [HttpGet("{id:id}")]
    [ProducesResponseType(
        typeof(TodoResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        TodoResponse? todo = await _todoService.GetByIdAsync(
            id,
            cancellationToken);
        
        if (todo is null)
        {
            return NotFound(new
            {
                message = $"Todo with ID {id} was not found."
            });
        }

        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(TodoResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoResponse>> Create(
        CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        TodoResponse createdTodo = 
            await _todoService.CreateAsync(
                request,
                cancellationToken);
                
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTodo.Id},
            createdTodo);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(
        typeof(TodoResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponse>> Update(
        int id,
        UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        TodoResponse? updatedTodo =
            await _todoService.UpdateAsync(
                id,
                request,
                cancellationToken);
        
        if (updatedTodo is null)
        {
            return NotFound(new
            {
                message = $"Todo with ID {id} was not found."
            });
        }

        return Ok(updatedTodo);
    }

    [HttpPatch("{id:int}/complete")]
    [ProducesResponseType(
        typeof(TodoResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponse>> MarkComplete(
        int id,
        CancellationToken cancellationToken)
    {
        TodoResponse? completedTodo =
            await _todoService.MarkCompleteAsync(
                id,
                cancellationToken);
        
        if  (completedTodo is null)
        {
            return NotFound(new
            {
                message = $"Todo with ID {id} was not found."
            });
        }

        return Ok(completedTodo);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        bool deleted = await _todoService.DeleteAsync(
            id, 
            cancellationToken);
        
        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Todo with ID {id} was not found."
            });
        }

        return NoContent();
    }

}