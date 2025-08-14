using Zenject;

public class PlayerUnitSpawnSystemInstaller : Installer
{
    public override void InstallBindings()
    {
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
}
