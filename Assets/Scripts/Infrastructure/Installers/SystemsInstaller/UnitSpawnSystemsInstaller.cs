using Zenject;

public class UnitSpawnSystemsInstaller : Installer
{
    public override void InstallBindings()
    {
        BindUnitFactory();

        Container.Install<PlayerUnitSpawnSystemInstaller>();
        Container.Install<EnemyUnitSpawnSystemInstaller>();
    }

    private void BindUnitFactory() => 
        Container.Bind<IUnitsFactory>()
            .To<UnitFactory>()
            .AsSingle();

}
