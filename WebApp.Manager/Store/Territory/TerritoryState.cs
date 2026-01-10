using Fluxor;
using WebApp.Domain.Models;

namespace WebApp.Manager.Store.Territory;

public record TerritoryState(
    bool Loading,
    List<TerritoryDto> Territories,
    string? SelectedCode
);

public class TerritoryFeature : Feature<TerritoryState>
{
    public override string GetName() => "Territory";
    protected override TerritoryState GetInitialState() => new(
        Loading: false,
        Territories: new(),
        SelectedCode: null
    );
}
