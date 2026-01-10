using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebApp.Manager.Store.Territory;

namespace WebApp.Manager.Components.Features.Territory;

public partial class TerritoryMap : ComponentBase
{
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (firstRender)
        //{
        //    await JS.InvokeVoidAsync("territoryMap.init", "/assets/us-states.geojson", DotNetObjectReference.Create(this));
        //}
        if (firstRender)
        {
            await JS.InvokeVoidAsync("initMap");
        }
    }

    [JSInvokable("OnTerritorySelected")]
    public void OnTerritorySelected(string code)
    {
        Dispatcher.Dispatch(new SelectTerritoryAction(code));
    }
}
