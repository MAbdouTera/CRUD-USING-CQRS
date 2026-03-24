using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Command.CreateToDo;

public sealed class CreateTodoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
{
    private readonly IAppDbContext _appDbContext;
    public CreateTodoCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Guid> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Id = Guid.CreateVersion7(),
            Title = request.Title
        };
        await _appDbContext.Todos.AddAsync(todo);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return todo.Id;
    }
}