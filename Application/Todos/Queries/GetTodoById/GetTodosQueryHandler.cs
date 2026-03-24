using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodoById;

public sealed class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    private readonly IAppDbContext _appDBContext;
    public GetTodoByIdQueryHandler(IAppDbContext appDbContext)
    {
        _appDBContext = appDbContext;
    }
    public async Task<Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _appDBContext.Todos.FindAsync(request.Id, cancellationToken);
    }
}