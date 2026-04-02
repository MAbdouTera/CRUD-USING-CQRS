using Application.Common.Exceptions;
using Application.Todos.Command.DeleteToDo;
using Application.Todos.Command.DeleteTodo;
using Domain.Todos;
using Tests.Helpers;

namespace Tests.Todos.Commands;

public class DeleteTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeletesTodo_WhenFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_DeletesTodo_WhenFound));
        var id = Guid.NewGuid();
        context.Todos.Add(new Todo { Id = id, Title = "To Delete", Completed = false });
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new DeleteTodoCommandHandler(context);
        await handler.Handle(new DeleteToDoCommand(id), CancellationToken.None);

        var deleted = await context.Todos.FindAsync(id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenTodoNotFound()
    {
        using var context = TestDbContextFactory.Create(nameof(Handle_ThrowsNotFoundException_WhenTodoNotFound));

        var handler = new DeleteTodoCommandHandler(context);
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteToDoCommand(Guid.NewGuid()), CancellationToken.None));
    }
}
