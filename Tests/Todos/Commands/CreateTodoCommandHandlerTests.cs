using Application.Todos.Command.CreateToDo;
using Tests.Helpers;

namespace Tests.Todos.Commands;

public class CreateTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_CreatesTodo_AndReturnsGuid()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_CreatesTodo_AndReturnsGuid));

        var handler = new CreateTodoCommandHandler(context);
        var result = await handler.Handle(new CreateToDoCommand("New Todo"), CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
        var todo = await context.Todos.FindAsync(result);
        Assert.NotNull(todo);
        Assert.Equal("New Todo", todo.Title);
        Assert.False(todo.Completed);
    }
}
