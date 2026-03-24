using MediatR;

namespace Application.Todos.Command.DeleteTodo;

public sealed record DeleteToDoCommand(Guid Id) : IRequest;