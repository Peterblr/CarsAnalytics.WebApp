using Microsoft.AspNetCore.Components;
using WebApp.Domain.Models;

namespace WebApp.Manager.Components.Admin;

public partial class StateCard : ComponentBase
{
    [Parameter] public StateShapeDto? SelectedState {  get; set; }
}
