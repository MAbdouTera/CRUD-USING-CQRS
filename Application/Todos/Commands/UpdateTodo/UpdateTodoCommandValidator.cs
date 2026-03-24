using Application.Todos.Command.UpdateTodo;
using FluentValidation;

namespace Application.Todos.Command.UpdateToDo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateToDoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty()
        .WithMessage("Title should not be empty");
    }
}