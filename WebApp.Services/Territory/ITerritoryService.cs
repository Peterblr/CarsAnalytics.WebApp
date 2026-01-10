using WebApp.Domain.Models;

namespace WebApp.Services.Territory;

public interface ITerritoryService
{
    Task<IEnumerable<TerritoryDto>> GetTerritoriesAsync(string regionCode, CancellationToken ct = default);
}
