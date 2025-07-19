using UnityEngine;
using Zenject;

public class HireUnitSystemInstaller : MonoInstaller
{
    [SerializeField] private HireUnitButtonConfigsLibrary _configsLibrary;

    public override void InstallBindings()
    {
        BindSpawnUnitSystem();

        BindHireUnitSystem();
    }

    private void BindSpawnUnitSystem()
    {
        Container.Bind<IPlayerControlledUnitsFactory>()
            .To<PlayerControlledUnitsFactory>()
            .AsSingle();

        Container.Bind<IPlayerUnitSpawner>()
            .To<PlayerControlledUnitSpawner>()
            .AsSingle();

        Container.Bind<IPlayerUnitSpawnerManager>()
            .To<PlayerUnitSpawnerManager>()
            .AsSingle();
    }

    private void BindHireUnitSystem()
    {
        Container.Bind<HireUnitButtonConfigsLibrary>()
            .FromInstance(_configsLibrary)
            .AsSingle();

        Container.Bind<IHiringUnitButtonsController>()
            .To<HiringUnitButtonsController>()
            .AsSingle();
    }
}
