using WebApp.Domain.Models;

namespace WebApp.Manager.Store.Territory;

public record LoadTerritoriesAction(string RegionCode); 
public record LoadTerritoriesResultAction(List<TerritoryDto> Territories);
public record SelectTerritoryAction(string Code);
