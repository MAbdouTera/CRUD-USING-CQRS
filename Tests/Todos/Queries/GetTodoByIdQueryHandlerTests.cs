using Application.Todos.Queries.GetTodoById;
using Domain.Todos;
using Tests.Helpers;

namespace Tests.Todos.Queries;

public class GetTodoByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsTodo_WhenFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ReturnsTodo_WhenFound));
        var id = Guid.NewGuid();
        context.Todos.Add(new Todo { Id = id, Title = "Test Todo", Completed = false });
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetTodoByIdQueryHandler(context);
        var result = await handler.Handle(new GetTodoByIdQuery(id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Test Todo", result.Title);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenNotFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ReturnsNull_WhenNotFound));

        var handler = new GetTodoByIdQueryHandler(context);
        var result = await handler.Handle(new GetTodoByIdQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.Null(result);
    }
}
