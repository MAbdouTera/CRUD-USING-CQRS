using Application.Todos.Command.DeleteTodo;
using Application.Todos.Command.DeleteToDo;

namespace Tests.Todos.Validators;

public class DeleteTodoCommandValidatorTests
{
    private readonly DeleteTodoCommandValidator _validator = new();

    [Fact]
    public void Validate_Passes_WhenIdIsNotEmpty()
    {
        var result = _validator.Validate(new DeleteToDoCommand(Guid.NewGuid()));
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_Fails_WhenIdIsEmpty()
    {
        var result = _validator.Validate(new DeleteToDoCommand(Guid.Empty));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Id should not be empty");
    }
}
