using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Todos.Command.UpdateTodo;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Command.UpdateToDo;

public sealed class UpdateTodoCommandHandler : IRequestHandler<UpdateToDoCommand>
{
    private readonly IAppDbContext _appDbContext;
    public UpdateTodoCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _appDbContext.Todos.FindAsync(request.Id, cancellationToken);
        if (todo == null)
        {
            throw new NotFoundException(nameof(Todo), request.Id); ;
        }
        todo.Completed = request.Completed;
        todo.Title = request.Title;
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}