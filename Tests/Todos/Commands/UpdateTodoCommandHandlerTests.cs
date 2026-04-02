using Application.Common.Exceptions;
using Application.Todos.Command.UpdateToDo;
using Application.Todos.Command.UpdateTodo;
using Domain.Todos;
using Tests.Helpers;

namespace Tests.Todos.Commands;

public class UpdateTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_UpdatesTodo_WhenFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_UpdatesTodo_WhenFound));
        var id = Guid.NewGuid();
        context.Todos.Add(new Todo { Id = id, Title = "Old Title", Completed = false });
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new UpdateTodoCommandHandler(context);
        await handler.Handle(new UpdateToDoCommand(id, "New Title", true), CancellationToken.None);

        var updated = await context.Todos.FindAsync(id);
        Assert.NotNull(updated);
        Assert.Equal("New Title", updated.Title);
        Assert.True(updated.Completed);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenTodoNotFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ThrowsNotFoundException_WhenTodoNotFound));

        var handler = new UpdateTodoCommandHandler(context);
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateToDoCommand(Guid.NewGuid(), "Title", false), CancellationToken.None));
    }
}
