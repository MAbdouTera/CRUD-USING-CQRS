using MediatR;

namespace Application.Todos.Command.UpdateTodo;

public sealed record UpdateToDoCommand(Guid Id, string Title, bool Completed) : IRequest;