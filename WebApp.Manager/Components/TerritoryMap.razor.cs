using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WebApp.Domain.Models;
using static System.Net.WebRequestMethods;

namespace WebApp.Manager.Components;

public partial class TerritoryMap : ComponentBase
{
    [Parameter] public IEnumerable<TerritoryDto> Territories { get; set; } = Enumerable.Empty<TerritoryDto>();
    [Parameter] public EventCallback<string> OnStateClick { get; set; }
    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
    protected string? SelectedCode { get; set; }
    protected List<StateShapeDto> States { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var http = HttpClientFactory.CreateClient("AssetsClient");
        var svg = await http.GetStringAsync("assets/us.svg");

        var xml = XDocument.Parse(svg);

        States = xml.Descendants()
            .Where(x => x.Name.LocalName == "path")
            .Select(x =>
            {
                var code = (string?)x.Attribute("id") ?? "";
                var path = (string?)x.Attribute("d") ?? "";
                var name = (string?)x.Attribute("data-name") ?? "";

                var (cx, cy) = GetPathCenter(path);

                return new StateShapeDto
                {
                    Code = code,
                    Path = path,
                    Name = name,
                    X = cx,
                    Y = cy
                };
            })
            .ToList();

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

    private (string X, string Y) GetPathCenter(string d)
    {
        var matches = Regex.Matches(d, @"-?\d+(\.\d+)?");

        var numbers = matches.Select(m => double.Parse(m.Value, System.Globalization.CultureInfo.InvariantCulture)).ToList();

        if (numbers.Count < 2) return ("0", "0");

        var xs = numbers.Where((n, i) => i % 2 == 0).ToList();
        var ys = numbers.Where((n, i) => i % 2 == 1).ToList();

        var minX = xs.Min();
        var maxX = xs.Max();
        var minY = ys.Min();
        var maxY = ys.Max();

        var centerX = (minX + maxX) / 2;
        var centerY = (minY + maxY) / 2;

        return (centerX.ToString("F0"), centerY.ToString("F0"));
    }

    public class StateShapeDto
    {
        public string Code { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? X { get; set; }
        public string? Y { get; set; }
    }
}
