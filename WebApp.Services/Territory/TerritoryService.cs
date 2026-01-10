using APIResponseWrapper;
using System.Net.Http.Json;
using System.Text.Json;
using WebApp.Domain.Models;

namespace WebApp.Services.Territory;

public class TerritoryService(HttpClient http) : ITerritoryService
{
    public async Task<IEnumerable<TerritoryDto>> GetTerritoriesAsync(string regionCode, CancellationToken ct = default)
    {
        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<object>>($"Territories/{regionCode}", ct);
            if (response?.Data is JsonElement json && json.ValueKind == JsonValueKind.Array)
            {
                var territories = json.Deserialize<IEnumerable<TerritoryDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); 
                return territories ?? new List<TerritoryDto>();
            }

            return new List<TerritoryDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return new List<TerritoryDto>();
        }
    }
}
