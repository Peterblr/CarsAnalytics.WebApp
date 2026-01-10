using Fluxor;

namespace WebApp.Manager.Store.Territory;

public static class TerritoryReducers
{
    [ReducerMethod]
    public static TerritoryState ReduceLoad(TerritoryState state, LoadTerritoriesAction action) =>
        state with { Loading = true };

    [ReducerMethod]
    public static TerritoryState ReduceLoadResult(TerritoryState state, LoadTerritoriesResultAction action) =>
        state with { Loading = false, Territories = action.Territories };

    [ReducerMethod]
    public static TerritoryState ReduceSelect(TerritoryState state, SelectTerritoryAction action) =>
        state with { SelectedCode = action.Code };
}
