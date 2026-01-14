namespace WebApp.Domain.Models;

public class StateShapeDto
{
    public string Code { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? X { get; set; }
    public string? Y { get; set; }
}
