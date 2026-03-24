using Api.Requests;
using Application.Todos.Command.CreateToDo;
using Application.Todos.Command.DeleteTodo;
using Application.Todos.Command.UpdateTodo;
using Application.Todos.Queries.GetTodoById;
using Application.Todos.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;
    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var result = await _mediator.Send(new GetTodosQuery());
        return Ok(result);

    }
    [HttpGet("{todoId:guid}")]
    public async Task<IActionResult> GetTodoById(Guid todoId)
    {
        var result = await _mediator.Send(new GetTodoByIdQuery(todoId));
        return result is null ? NotFound() : Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Post(CreateTodoRequest request)
    {
        var command = new CreateToDoCommand(request.Title);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> Put(Guid todoId, UpdateToRequest request)
    {
        var command = new UpdateToDoCommand(todoId, request.Title, request.Completed);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> Delete(Guid todoId)
    {
        var command = new DeleteToDoCommand(todoId);
        await _mediator.Send(command);
        return NoContent();
    }
}