using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;
using WebApp.Domain.Models;
using WebApp.Manager.Store.Territory;

namespace WebApp.Manager.Components.Admin;

public partial class TerritoryMap : ComponentBase
{
    [Parameter] public IEnumerable<TerritoryDto> Territories { get; set; } = Enumerable.Empty<TerritoryDto>();
    [Parameter] public EventCallback<string> OnStateClick { get; set; }
    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
    [Inject] private IState<TerritoryState> State { get; set; } = default!;

    protected string? SelectedCode { get; set; }
    protected List<StateShapeDto> States { get; set; } = new();
    protected bool Loading => State.Value.Loading;

    protected StateShapeDto? SelectedState => States.FirstOrDefault(s => s.Code == SelectedCode);
    protected override async Task OnInitializedAsync()
    {
        var http = HttpClientFactory.CreateClient("AssetsClient");
        var svg = await http.GetStringAsync("assets/us1.svg");

        var xml = XDocument.Parse(svg);

        States = [.. xml.Descendants()
            .Where(x => x.Name.LocalName == "path")
            .Select(x =>
            {
                var code = (string?)x.Attribute("id") ?? "";
                var path = (string?)x.Attribute("d") ?? "";
                var name = (string?)x.Attribute("data-name") ?? "";

                var textElement = xml.Descendants()
                    .FirstOrDefault(t => t.Name.LocalName == "text" &&
                                         ((string?)t.Value)?.Trim().EndsWith(code, StringComparison.OrdinalIgnoreCase) == true);

                var textValue = textElement?.Value ?? "";
                var textX = (string?)textElement?.Attribute("x") ?? "0";
                var textY = (string?)textElement?.Attribute("y") ?? "0";
                var fontSize = (string?)textElement?.Attribute("font-size") ?? "12";

                return new StateShapeDto
                {
                    Code = code,
                    Path = path,
                    Name = name,
                    Text = textValue,
                    TextX = textX,
                    TextY = textY,
                    FontSize = fontSize
                };
            }
        )];
    }

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
