using static System.Net.Mime.MediaTypeNames;

namespace WebApp.Domain.Models;

public class StateShapeDto
{
    public string Code { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string TextX { get; set; } = string.Empty;
    public string TextY { get; set; } = string.Empty;
    public string FontSize { get; set; } = string.Empty;

    private const string DESHPREFIX = "-------";

    public string DisplayText(bool isSelected)
    {
        if (!isSelected) return Text;

        if (Text.StartsWith(DESHPREFIX))
        {
            Text = Text[2..];
            return Text.Trim();
        }
        return Text;
    }
}
