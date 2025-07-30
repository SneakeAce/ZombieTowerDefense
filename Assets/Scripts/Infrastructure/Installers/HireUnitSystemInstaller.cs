using Zenject;

public class HireUnitSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUnitComponents();

        BindSpawnUnitSystem();

        BindUnitHireSystem();
    }

    private void BindSpawnUnitSystem()
    {
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

    // Temporary method!
    private void BindUnitComponents() 
    {
        Container.Bind<IUnitHealth>()
            .To<UnitHealth>()
            .AsTransient();
    }

    private void BindUnitHireSystem()
    {
        Container.Bind<IUnitHiringButtonsController>()
            .To<UnitHiringButtonsController>()
            .AsSingle();

        Container.Bind<IUnitHiringController>()
            .To<UnitHiringController>()
            .AsSingle();
    }
}
