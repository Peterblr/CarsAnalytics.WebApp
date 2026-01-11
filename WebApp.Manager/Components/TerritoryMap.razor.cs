using Microsoft.AspNetCore.Components;
using WebApp.Domain.Models;

namespace WebApp.Manager.Components;

public partial class TerritoryMap : ComponentBase
{
    [Parameter] public IEnumerable<TerritoryDto> Territories { get; set; } = Enumerable.Empty<TerritoryDto>();
    [Parameter] public EventCallback<string> OnStateClick { get; set; }

    protected string? SelectedCode { get; set; }

    protected void OnClick(string id)
    {
        SelectedCode = id;
        OnStateClick.InvokeAsync(id);
    }

    protected string GetFill(string code)
    {
        var t = Territories.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        if (t is null) return "#f9f9f9"; 

        return t.RegionCode.ToLowerInvariant() switch
        {
            "west" => "#6cc070",
            "south" => "#f7c24f",
            "midwest" => "#4f8ef7",
            "northeast" => "#b36cf0",
            _ => "#f9f9f9"
        };
    }
}
