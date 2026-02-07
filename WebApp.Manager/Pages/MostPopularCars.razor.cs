using Fluxor;
using Microsoft.AspNetCore.Components;
using WebApp.Domain.Models;
using WebApp.Manager.Store.Territory;

namespace WebApp.Manager.Pages;

public partial class MostPopularCars
{
    [Inject] private IState<TerritoryState> State { get; set; } = default!;
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;

    protected bool Loading => State.Value.Loading;
    protected List<TerritoryDto> TerritoriesModel => State.Value.Territories;

    protected override async Task OnInitializedAsync()
    {
        State.StateChanged += OnStateChanged;
        Dispatcher.Dispatch(new LoadTerritoriesAction("us"));
        await base.OnInitializedAsync();
    }

    private void HandleStateClick(string code)
    {
        Console.WriteLine($"Clicked state: {code}");
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        State.StateChanged -= OnStateChanged;
    }
}
