using Microsoft.AspNetCore.Components;
using WebApp.Domain.Models;

namespace WebApp.Manager.Components;

public partial class StateShape : ComponentBase
{
    [Parameter] public StateShapeDto State { get; set; } = default!;
    [Parameter] public bool IsSelected { get; set; }
    [Parameter] public string FillColor { get; set; } = "#f9f9f9";
    [Parameter] public EventCallback<string> OnClick { get; set; }
}
