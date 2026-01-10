using Fluxor;
using WebApp.Services.Territory;

namespace WebApp.Manager.Store.Territory;

public class TerritoryEffects(ITerritoryService service)
{
    [EffectMethod]
    public async Task HandleLoad(LoadTerritoriesAction action, IDispatcher dispatcher)
    {
        var items = await service.GetTerritoriesAsync(action.RegionCode);
        await Task.Delay(2000);
        dispatcher.Dispatch(new LoadTerritoriesResultAction([.. items]));
    }
}
