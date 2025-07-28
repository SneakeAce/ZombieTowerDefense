using UnityEngine;
using Zenject;

public class SpawnersInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerUnitSpawner();
    }

    private void BindPlayerUnitSpawner()
    {
        Container.Bind<IUnitHealth>() // Убрать эо безобразие!!!
            .To<UnitHealth>()
            .AsTransient();

        Container.Bind<IPlayerUnitsFactory>()
            .To<PlayerUnitFactory>()
            .AsSingle();

        Container.Bind<IPlayerUnitSpawner>()
            .To<PlayerUnitSpawner>()
            .AsSingle();

        Container.Bind<IPlayerUnitSpawnerManager>()
            .To<PlayerUnitSpawnerManager>()
            .AsSingle();
    }
}
