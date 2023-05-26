namespace Contracts;

public class AuthDto
{
    public bool IsSucceeded { get; set; }
    public IEnumerable<string> Errors { get; set; }
}