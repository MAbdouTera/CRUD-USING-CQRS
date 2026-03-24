namespace Api.Requests;

public class UpdateToRequest
{
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; }
}