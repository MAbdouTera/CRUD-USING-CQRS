using Application.Todos.Command.CreateToDo;

namespace Tests.Todos.Validators;

public class CreateTodoCommandValidatorTests
{
    private readonly CreateTodoCommandValidator _validator = new();

    [Fact]
    public void Validate_Passes_WhenTitleIsNotEmpty()
    {
        var result = _validator.Validate(new CreateToDoCommand("Valid Title"));
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_Fails_WhenTitleIsEmpty()
    {
        var result = _validator.Validate(new CreateToDoCommand(string.Empty));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Title should not be empty");
    }
}
