using Microsoft.AspNetCore.Components;
using WebApp.Domain.Models;

namespace WebApp.Manager.Components;

public partial class StateCard : ComponentBase
{
    [Parameter] public StateShapeDto? SelectedState {  get; set; }
}
