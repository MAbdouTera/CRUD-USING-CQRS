using MediatR;

namespace Application.Todos.Command.CreateToDo;
public sealed record CreateToDoCommand(string Title):IRequest<Guid>;