using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Todos.Command.DeleteTodo;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Command.DeleteToDo;

public sealed class DeleteTodoCommandHandler : IRequestHandler<DeleteToDoCommand>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteTodoCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _appDbContext.Todos.FindAsync(request.Id, cancellationToken);
        if (todo == null)
        {
            throw new NotFoundException(nameof(Todo), request.Id);
        }
        _appDbContext.Todos.Remove(todo);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}