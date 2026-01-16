using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApp.Manager.Components;

public partial class TempMap : ComponentBase
{
    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    private string SvgContent { get; set; } = string.Empty;
    private bool svgLoaded = false;
    protected override async Task OnInitializedAsync()
    {
        var http = HttpClientFactory.CreateClient("AssetsClient");
        SvgContent = await http.GetStringAsync("assets/us.svg");
        await Task.Delay(200);
        svgLoaded = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && svgLoaded)
        {
            await JS.InvokeVoidAsync("addStateLabels", "#us-map-container svg");
        }
    }

    private async Task CallLabels()
    {
        await JS.InvokeVoidAsync("addStateLabels", "#us-map-container svg");
    }

    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        await Task.Delay(100);
    //        await JS.InvokeVoidAsync("addStateLabels", "#us-map-container svg");
    //    }
    //}
}
