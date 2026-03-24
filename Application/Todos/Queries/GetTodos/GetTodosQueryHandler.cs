using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodos;

public sealed class GetTodoQueryHandler : IRequestHandler<GetTodosQuery, List<Todo>>
{
    private readonly IAppDbContext _appDBContext;
    public GetTodoQueryHandler(IAppDbContext appDbContext)
    {
        _appDBContext = appDbContext;
    }
    public async Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await _appDBContext.Todos.ToListAsync(cancellationToken);
    }
}