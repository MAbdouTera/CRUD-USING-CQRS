namespace Domain.Todos;
public class Todo
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public bool Completed { get; set; }
}