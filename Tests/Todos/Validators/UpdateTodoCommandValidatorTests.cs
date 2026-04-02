using Application.Todos.Command.UpdateTodo;
using Application.Todos.Command.UpdateToDo;

namespace Tests.Todos.Validators;

public class UpdateTodoCommandValidatorTests
{
    private readonly UpdateTodoCommandValidator _validator = new();

    [Fact]
    public void Validate_Passes_WhenTitleIsNotEmpty()
    {
        var result = _validator.Validate(new UpdateToDoCommand(Guid.NewGuid(), "Valid Title", false));
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_Fails_WhenTitleIsEmpty()
    {
        var result = _validator.Validate(new UpdateToDoCommand(Guid.NewGuid(), string.Empty, false));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Title should not be empty");
    }
}
