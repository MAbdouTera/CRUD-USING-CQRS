using Application.Todos.Queries.GetTodos;
using Domain.Todos;
using Tests.Helpers;

namespace Tests.Todos.Queries;

public class GetTodosQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllTodos()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ReturnsAllTodos));
        context.Todos.AddRange(
            new Todo { Id = Guid.NewGuid(), Title = "First", Completed = false },
            new Todo { Id = Guid.NewGuid(), Title = "Second", Completed = true }
        );
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetTodoQueryHandler(context);
        var result = await handler.Handle(new GetTodosQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoTodos()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ReturnsEmptyList_WhenNoTodos));

        var handler = new GetTodoQueryHandler(context);
        var result = await handler.Handle(new GetTodosQuery(), CancellationToken.None);

        Assert.Empty(result);
    }
}
