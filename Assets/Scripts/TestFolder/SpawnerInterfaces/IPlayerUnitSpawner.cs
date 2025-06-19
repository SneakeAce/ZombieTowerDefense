using Cysharp.Threading.Tasks;

public interface IPlayerUnitSpawner : IUnitSpawner
{
    void CreateUnit(UnitType unitType);
}
