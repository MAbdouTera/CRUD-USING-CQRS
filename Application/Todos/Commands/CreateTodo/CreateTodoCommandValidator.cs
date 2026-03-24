using FluentValidation;

namespace Application.Todos.Command.CreateToDo;

public class CreateTodoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty()
        .WithMessage("Title should not be empty");
    }
}