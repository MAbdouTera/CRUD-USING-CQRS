using Application.Todos.Command.DeleteTodo;
using FluentValidation;

namespace Application.Todos.Command.DeleteToDo;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Id should not be empty");
    }
}