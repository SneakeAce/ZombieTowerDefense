using Zenject;

public class PlayerUnitSpawnSystemInstaller : Installer
{
    public override void InstallBindings()
    {
        BindWeaponFactory();

        BindUnitBuilder();

        BindPlayerUnitSpawnSystem();
    }

    private void BindPlayerUnitSpawnSystem()
    {
        Container.Bind<IPlayerUnitSpawner>()
            .To<PlayerUnitSpawner>()
            .AsSingle();

        Container.Bind<IPlayerUnitSpawnerManager>()
            .To<PlayerUnitSpawnerManager>()
            .AsSingle();
    }

    private void BindWeaponFactory()
    {
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
    }

    private void BindUnitBuilder()
    {
        Container.Bind<IUnitBuilder>().To<PlayerUnitBuilder>().AsSingle();
    }
}
